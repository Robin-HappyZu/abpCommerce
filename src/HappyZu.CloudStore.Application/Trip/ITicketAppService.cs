using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    public interface ITicketAppService : IApplicationService
    {
        #region 门票

        Task<ResultOutputDto> AddTicketAsync(AddTicketInput input);

        Task<ResultOutputDto> UpdateTicketAsync(UpdateTicketInput input);
        Task<ResultOutputDto> UpdateTicketQuoteTypeAsync(UpdateTicketInput input);

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

        Task<IPagedResult<TicketQuoteDto>> GetTicketQuotesByTicketId(int ticketId);

        #endregion

        #region 门票订单

        Task<ResultOutputDto> AddTicketOrderAsync(AddTicketOrderInput input);

        Task<ResultOutputDto> UpdateTicketOrderAsync(UpdateTicketOrderInput input);

        Task<ResultOutputDto> RemoveTicketOrderAsync(int ticketOrderId);

        Task<TicketOrderDto> GetTicketOrderByIdAsync(int ticektOrderId);

        Task<IPagedResult<TicketOrderDto>> GetPagedTicketOrdersByTicektId(GetPagedTicketOrdersInput input);

        bool CanCancelTicketOrder(TicketOrder order);

        Task<ResultOutputDto> CancelTicketOrderAsync(TicketOrder order, bool notifyCustomer);

        bool CanMarkTicketOrderAsPaid(TicketOrder order);

        Task<ResultOutputDto> MarkTicketOrderAsPaidAsync(TicketOrder order);

        bool CanRefundTicketOrder(TicketOrder order);

        Task<ResultOutputDto> RefundTicketOrderAsync(TicketOrder order);

        #endregion

        #region 门票标签

        Task<ResultOutputDto> AttachTicketAttributeRecordAsync(AttachTicketAttributeRecordInput input);

        Task<ResultOutputDto> DetachTicketAttributeRecordAsync(DetachTicketAttributeRecordInput input);

        Task<List<TicketAttributeRecordDto>> GetAllAttributeRecordByTicketIdAsync(int ticketId);

        #endregion

        #region 门票类型

        Task<IPagedResult<TicketTypeDto>> GetTicketTypeListAsync(int destId);

        Task<TicketTypeDto> GetTicketTypeAsync(int id);

        Task<ResultOutputDto> AddTicketTypeAsync(AddTicketTypeInput input);

        Task<ResultOutputDto> RemoveTicketTypeAsync(int id);
        #endregion
    }
}
