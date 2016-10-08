using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using HappyZu.CloudStore.Entities;

namespace HappyZu.CloudStore.Trip.Events
{
    public class PaymentHandler : IEventHandler<OrderPaidEventData>, ITransientDependency
    {
        private readonly TicketOrderManager _ticketOrderManager;
        private readonly PaymentRecordManager _paymentRecordManager;
        private readonly ETicketManager _eTicketManager;
        private readonly ETicketAppService _eTicketAppService;

        public PaymentHandler(TicketOrderManager ticketOrderManager, PaymentRecordManager paymentRecordManagere,
            ETicketManager eTicketManager, ETicketAppService eTicketAppService)
        {
            _ticketOrderManager = ticketOrderManager;
            _paymentRecordManager = paymentRecordManagere;
            _eTicketManager = eTicketManager;
            _eTicketAppService = eTicketAppService;
        }

        /// <summary>
        /// 订单付款成功
        /// </summary>
        /// <param name="eventData"></param>
        public async void HandleEvent(OrderPaidEventData eventData)
        {
            // 修改订单状态
            int orderId;
            var result = int.TryParse(eventData.TradeNo, out orderId);
            if (!result)
            {
                // 添加日志记录错误的单号,以便人工对账处理
                return;
            }

            var order = await _ticketOrderManager.GetTicketOrderByIdAsync(orderId);
            if (order.Status == OrderStatus.Paying || order.Status == OrderStatus.Pending)
            {
                order.Status = OrderStatus.Paid;
                order.PaidAmount = eventData.Amount;
                await _ticketOrderManager.UpdateTicketOrderAsync(order);
            }

            // 添加付款记录
            var paymentRecord = new PaymentRecord()
            {
                OrderId = orderId,
            };
            await _paymentRecordManager.AddPaymentRecordAsync(paymentRecord);
            
            // 生成电子票据
            var count = await _eTicketManager.GetETicketsCountByTicketOrderIdAsync(orderId);
            if (count != 0) return;

            // 获取订单详情
            var items = await _ticketOrderManager.GetTicketOrderDetailsByTicketOrderIdAsync(orderId);
            foreach (var item in items)
            {
                for (var i = 0; i < item.Quantity; i++)
                {
                    await _eTicketAppService.CreateETicketAsync(item.TicketId, item.TicketOrderId, item.Id, "test");
                }
            }
        }
    }
}
