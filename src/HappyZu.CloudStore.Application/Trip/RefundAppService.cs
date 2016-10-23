using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.Entities;

namespace HappyZu.CloudStore.Trip
{
    public class RefundAppService : IRefundAppService
    {
        private readonly TicketOrderManager _ticketOrderManager;
        private readonly ETicketManager _eTicketManager;
        private readonly IRepository<RefundRecord> _refundRecordRepository; 

        public RefundAppService(TicketOrderManager ticketOrderManager, ETicketManager eTicketManager, IRepository<RefundRecord> refundRecordRepository)
        {
            _ticketOrderManager = ticketOrderManager;   
            _eTicketManager = eTicketManager;
            _refundRecordRepository = refundRecordRepository;
        }

        public async Task<ResultOutputDto> SubmitRefundRequestAsync(int ticketOrderId)
        {
            try
            {
                // 是否已经提交过申请
                var request = await _refundRecordRepository.FirstOrDefaultAsync(o => o.TicketOrderId == ticketOrderId);
                if (request != null)
                {
                    return ResultOutputDto.Fail(0, "不允许重复提交申请");
                }

                // 订单
                var order = await _ticketOrderManager.GetTicketOrderByIdAsync(ticketOrderId);
                if (order.Status != OrderStatus.Paid)
                {
                    return ResultOutputDto.Fail(0, "当前订单状态不支持退款");
                }

                // 获取订单详情
                var orderItems = await _ticketOrderManager.GetTicketOrderDetailsByTicketOrderIdAsync(ticketOrderId);
                if (!orderItems.Any())
                {
                    return ResultOutputDto.Fail(0, "订单信息异常");
                }

                if (orderItems.Any(item => item.Date <= DateTime.UtcNow))
                {
                    return ResultOutputDto.Fail(0, "当日或者过期门票不能退款");
                }

                // 电子票详情
                var eTickets = await _eTicketManager.GetETicketsByTicketOrderIdAsync(ticketOrderId);
                if (eTickets.Any(eTicket => eTicket.IsChecked))
                {
                    return ResultOutputDto.Fail(0, "门票已使用不能退款");
                }

                // 添加退款申请
                var record = new RefundRecord()
                {
                    TicketOrderId = ticketOrderId,
                    Amount = order.TotalAmount,
                    ApplyStatus = ApplyStatus.Applying,
                    RefundStatus = RefundStatus.None
                };

                await _refundRecordRepository.InsertAsync(record);

                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> ApproveRefundRequestAsync(int ticketOrderId)
        {
            try
            {
                var refund = await _refundRecordRepository.FirstOrDefaultAsync(o => o.TicketOrderId == ticketOrderId);
                if (refund == null)
                {
                    return ResultOutputDto.Fail(0, "退款申请不存在");
                }

                if (refund.ApplyStatus != ApplyStatus.Applying)
                {
                    return ResultOutputDto.Fail(0, "退款申请状态异常");
                }

                refund.ApplyStatus = ApplyStatus.Approved;
                refund.RefundStatus = RefundStatus.Processing;
                await _refundRecordRepository.UpdateAsync(refund);

                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> CloseRefundRequestAsync(int ticketOrderId)
        {
            try
            {
                var refund = await _refundRecordRepository.FirstOrDefaultAsync(o => o.TicketOrderId == ticketOrderId);
                if (refund == null)
                {
                    return ResultOutputDto.Fail(0, "退款申请不存在");
                }

                if (refund.ApplyStatus != ApplyStatus.Applying)
                {
                    return ResultOutputDto.Fail(0, "退款申请状态异常");
                }

                refund.ApplyStatus = ApplyStatus.Denied;
                await _refundRecordRepository.UpdateAsync(refund);

                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> CompleteRefundRequestAsync(int ticketOrderId)
        {
            try
            {
                var refund = await _refundRecordRepository.FirstOrDefaultAsync(o => o.TicketOrderId == ticketOrderId);

                if (refund.ApplyStatus != ApplyStatus.Approved)
                {
                    return ResultOutputDto.Fail(0, "退款申请状态异常");
                }

                refund.RefundStatus = RefundStatus.Completed;
                await _refundRecordRepository.UpdateAsync(refund);

                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }
    }
}
