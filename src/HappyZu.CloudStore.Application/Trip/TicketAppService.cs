using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    public class TicketAppService : ITicketAppService
    {
        public Task AddTicketAsync(AddTicketInput input)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTicketAsync(UpdateTicketInput input)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTicketAsync(int ticketId)
        {
            throw new NotImplementedException();
        }

        public Task<TicketDto> GetTicketByIdAsync(int ticketId)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<TicketDto>> GetTicketsAsync(GetPagedTicketsInput input)
        {
            throw new NotImplementedException();
        }

        public Task AddTicketQuoteAsync(AddTicketQuoteInput input)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTicketQuoteAsync(UpdateTicketQuoteInput input)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTicketQuoteAsync(int ticketQuoteId)
        {
            throw new NotImplementedException();
        }

        public Task GetTicketQuoteByIdAsync(int ticektQuoteId)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<TicketQuoteDto>> GetTicketQuotesByTicektId(GetPagedTicketQuotesInput input)
        {
            throw new NotImplementedException();
        }

        public Task AddTicketOrderAsync(AddTicketOrderInput input)
        {
            throw new NotImplementedException();
        }

        public Task UpdateTicketOrderAsync(UpdateTicketOrderInput input)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTicketOrderAsync(int ticketOrderId)
        {
            throw new NotImplementedException();
        }

        public Task<TicketOrderDto> GetTicketOrderByIdAsync(int ticektOrderId)
        {
            throw new NotImplementedException();
        }

        public Task<IPagedResult<TicketOrderDto>> GetTicketOrdersByTicektId(GetPagedTicketOrdersInput input)
        {
            throw new NotImplementedException();
        }
    }
}
