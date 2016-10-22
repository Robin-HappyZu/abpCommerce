using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    public interface IPaymentAppService : IApplicationService
    {   
        //Task AddPaymentRecordAsync(AddPaymentRecordInput input);

        //Task<TicketOrder> GetTicketOrderByPaymentIdAsync(int paymentId);

        Task OrderPaidAsync(string tradeNo, string transactionNo, decimal amount);
    }
}
