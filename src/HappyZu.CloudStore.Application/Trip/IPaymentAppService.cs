using System.Threading.Tasks;
using Abp.Application.Services;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    public interface IPaymentAppService : IApplicationService
    {   
        Task OrderPaidAsync(OrderPaidInput input);
    }
}
