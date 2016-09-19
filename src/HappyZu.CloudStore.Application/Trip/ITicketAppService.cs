using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    interface ITicketAppService : IApplicationService
    {
        #region 门票

        Task AddTicketAsync(AddTicketInput input);

        Task UpdateTicketAsync(UpdateTicketInput input);

        Task RemoveTicketAsync(int ticketId);

        Task<TicketDto> GetTicketByIdAsync(int ticketId);

        Task<IPagedResult<TicketDto>> GetTicketsAsync(GetPagedTicketsInput input);

        #endregion

        #region 门票报价

        Task AddTicketQuoteAsync(AddTicketQuoteInput input);

        Task UpdateTicketQuoteAsync(UpdateTicketQuoteInput input);

        Task RemoveTicketQuoteAsync(int ticketQuoteId);

        Task GetTicketQuoteByIdAsync(int ticektQuoteId);

        Task<IPagedResult<TicketQuoteDto>> GetTicketQuotesByTicektId(GetPagedTicketQuotesInput input);

        #endregion

        #region 门票订单

        Task AddTicketOrderAsync(AddTicketOrderInput input);

        Task UpdateTicketOrderAsync(UpdateTicketOrderInput input);

        Task RemoveTicketOrderAsync(int ticketOrderId);

        Task<TicketOrderDto> GetTicketOrderByIdAsync(int ticektOrderId);

        Task<IPagedResult<TicketOrderDto>> GetTicketOrdersByTicektId(GetPagedTicketOrdersInput input);
        #endregion
    }
}
