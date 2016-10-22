using System.Threading.Tasks;
using Abp.Application.Services;
using HappyZu.CloudStore.Common.Dto;

namespace HappyZu.CloudStore.Trip
{
    interface IRefundAppService : IApplicationService
    {
        Task<ResultOutputDto> SubmitRefundRequestAsync(int ticketOrderId);
    }
}
