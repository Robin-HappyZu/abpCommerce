using System.Threading.Tasks;
using Abp.Application.Services;
using HappyZu.CloudStore.Common.Dto;

namespace HappyZu.CloudStore.Trip
{
    public interface IRefundAppService : IApplicationService
    {
        Task<ResultOutputDto> SubmitRefundRequestAsync(int ticketOrderId);

        Task<ResultOutputDto> ApproveRefundRequestAsync(int ticketOrderId);

        Task<ResultOutputDto> CloseRefundRequestAsync(int ticketOrderId);

        Task<ResultOutputDto> CompleteRefundRequestAsync(int ticketOrderId);
    }
}
