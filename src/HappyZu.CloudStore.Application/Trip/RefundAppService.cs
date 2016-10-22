using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.Entities;

namespace HappyZu.CloudStore.Trip
{
    public class RefundAppService : IRefundAppService
    {
        private readonly TicketOrderManager _ticketOrderManager;
        private readonly ETicketManager _eTicketManager;

        public RefundAppService(TicketOrderManager ticketOrderManager, ETicketManager eTicketManager)
        {
            _ticketOrderManager = ticketOrderManager;
            _eTicketManager = eTicketManager;
        }

        public async Task<ResultOutputDto> SubmitRefundRequestAsync(int ticketOrderId)
        {
            try
            {
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
                //TODO:

                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }
    }
}
