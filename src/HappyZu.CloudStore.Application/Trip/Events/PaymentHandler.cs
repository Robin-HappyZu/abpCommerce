using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Events.Bus;
using Abp.Events.Bus.Handlers;
using HappyZu.CloudStore.Entities;
using HappyZu.CloudStore.StatisticalAnalysis.Events;

namespace HappyZu.CloudStore.Trip.Events
{
    public class PaymentHandler : IEventHandler<OrderPaidEventData>, ITransientDependency
    {
        private readonly TicketOrderManager _ticketOrderManager;
        private readonly PaymentRecordManager _paymentRecordManager;
        private readonly ETicketManager _eTicketManager;
        private readonly ETicketAppService _eTicketAppService;
        public IEventBus EventBus { get; set; }

        public PaymentHandler(TicketOrderManager ticketOrderManager, PaymentRecordManager paymentRecordManagere,
            ETicketManager eTicketManager, ETicketAppService eTicketAppService)
        {
            _ticketOrderManager = ticketOrderManager;
            _paymentRecordManager = paymentRecordManagere;
            _eTicketManager = eTicketManager;
            _eTicketAppService = eTicketAppService;

            EventBus = NullEventBus.Instance;
        }

        /// <summary>
        /// 订单付款成功
        /// </summary>
        /// <param name="eventData"></param>
        public async void HandleEvent(OrderPaidEventData eventData)
        {
            var payResult = eventData.WechatPayResult;
            var order = await _ticketOrderManager.GetTicketOrderByOrderNoAsync(payResult.OrderNo);
            if (order==null)
            {
                // 添加日志记录错误的单号,以便人工对账处理
                return;
            }
            if (order.Status == OrderStatus.Paying || order.Status == OrderStatus.Pending)
            {
                order.Status = OrderStatus.Paid;
                order.PaidAmount = payResult.Amount;
                await _ticketOrderManager.UpdateTicketOrderAsync(order);
            }
            else
            {
                //订单已支付
                return;
            }

            // 添加付款记录
            var paymentRecord = new PaymentRecord()
            {
                OrderId = order.Id,
            };
            await _paymentRecordManager.AddPaymentRecordAsync(paymentRecord);
            
            // 生成电子票据
            var count = await _eTicketManager.GetETicketsCountByTicketOrderIdAsync(order.Id);
            if (count != 0) return;

            // 获取订单详情
            var items = await _ticketOrderManager.GetTicketOrderDetailsByTicketOrderIdAsync(order.Id);
            foreach (var item in items)
            {
                for (var i = 0; i < item.Quantity; i++)
                {
                    await _eTicketAppService.CreateETicketAsync(item.TicketId, item.TicketOrderId, item.Id, "test");
                }
            }

            //通知统计消息事件
            await EventBus.TriggerAsync(new StatisticsSalesEventData()
            {
                OrderId= order.Id,
                Total=order.TotalAmount,
                PaidAmount=payResult.Amount,
                AgentId=order.AgentId
            });
        }
    }
}
