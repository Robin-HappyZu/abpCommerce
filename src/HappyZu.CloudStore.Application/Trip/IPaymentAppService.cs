using System.Threading.Tasks;
using Abp.Application.Services;

namespace HappyZu.CloudStore.Trip
{
    public interface IPaymentAppService : IApplicationService
    {   
        Task OrderPaidAsync(string tradeNo, string transactionNo, decimal amount);
    }
}
