using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    interface ITicketAppService : IApplicationService
    {
        #region 门票

        Task<ResultOutputDto> AddTicketAsync(AddTicketInput input);

        Task<ResultOutputDto> UpdateTicketAsync(UpdateTicketInput input);

        Task<ResultOutputDto> RemoveTicketAsync(int ticketId);

        Task<TicketDto> GetTicketByIdAsync(int ticketId);

        Task<IPagedResult<TicketDto>> GetPagedTicketsAsync(GetPagedTicketsInput input);

        #endregion

        #region 门票报价

        Task<ResultOutputDto> AddTicketQuoteAsync(AddTicketQuoteInput input);

        Task<ResultOutputDto> UpdateTicketQuoteAsync(UpdateTicketQuoteInput input);

        Task<ResultOutputDto> RemoveTicketQuoteAsync(int ticketQuoteId);

        Task<TicketQuoteDto> GetTicketQuoteByIdAsync(int ticektQuoteId);

        Task<IPagedResult<TicketQuoteDto>> GetPagedTicketQuotesByTicektId(GetPagedTicketQuotesInput input);

        #endregion

        #region 门票订单

        Task<ResultOutputDto> AddTicketOrderAsync(AddTicketOrderInput input);

        Task<ResultOutputDto> UpdateTicketOrderAsync(UpdateTicketOrderInput input);

        Task<ResultOutputDto> RemoveTicketOrderAsync(int ticketOrderId);

        Task<TicketOrderDto> GetTicketOrderByIdAsync(int ticektOrderId);

        Task<IPagedResult<TicketOrderDto>> GetPagedTicketOrdersByTicektId(GetPagedTicketOrdersInput input);
        #endregion
    }
}
