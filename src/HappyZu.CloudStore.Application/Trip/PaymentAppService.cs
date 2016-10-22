using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Events.Bus;
using HappyZu.CloudStore.Trip.Dto;
using HappyZu.CloudStore.Trip.Events;

namespace HappyZu.CloudStore.Trip
{
    public class PaymentAppService : IPaymentAppService
    {
        public IEventBus EventBus { get; set; }

        public PaymentAppService()
        {
            EventBus = NullEventBus.Instance;
        }

        public Task AddPaymentRecordAsync(AddPaymentRecordInput input)
        {
            return null;
        }

        public Task<TicketOrder> GetTicketOrderByPaymentIdAsync(int paymentId)
        {
            return null;
        }

        public async Task OrderPaidAsync(string tradeNo, string transactionNo, decimal amount)
        {
            await EventBus.TriggerAsync(new OrderPaidEventData()
            {
                TradeNo = tradeNo,
                TransactionNo = transactionNo,
                Amount = amount
            });
        }
    }
}
