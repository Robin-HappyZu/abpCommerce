using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Events.Bus;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    public class PaymentAppSerivce : IPaymentAppService
    {
        public IEventBus EventBus;
  
        public PaymentAppSerivce()
        {
            EventBus = NullEventBus.Instance;
        }

        public Task AddPaymentRecordAsync(AddPaymentRecordInput input)
        {
            throw new NotImplementedException();
        }

        public Task<TicketOrder> GetTicketOrderByPaymentIdAsync(int paymentId)
        {
            throw new NotImplementedException();
        }

        public async Task OrderPaidAsync(string tradeNo, string transactionNo, decimal amount)
        {
            await EventBus.TriggerAsync();
        }
    }
}
