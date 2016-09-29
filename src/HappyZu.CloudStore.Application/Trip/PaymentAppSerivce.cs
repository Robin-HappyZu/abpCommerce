using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    public class PaymentAppSerivce : IPaymentAppService
    {
        public Task AddPaymentRecordAsync(AddPaymentRecordInput input)
        {
            throw new NotImplementedException();
        }

        public Task<TicketOrder> GetTicketOrderByPaymentIdAsync(int paymentId)
        {
            throw new NotImplementedException();
        }
    }
}
