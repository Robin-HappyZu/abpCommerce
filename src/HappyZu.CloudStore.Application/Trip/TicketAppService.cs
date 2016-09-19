using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    public class TicketAppService : ITicketAppService
    {
        private readonly TicketManager _ticketManager;
        private readonly TicketQuoteManager _ticketQuoteManager;
        private readonly TicketOrderManager _ticketOrderManager;

        public TicketAppService(TicketManager ticketManager, TicketQuoteManager ticketQuoteManager,
            TicketOrderManager ticketOrderManager)
        {
            _ticketManager = ticketManager;
            _ticketOrderManager = ticketOrderManager;
            _ticketQuoteManager = ticketQuoteManager;
        }

        public async Task AddTicketAsync(AddTicketInput input)
        {
            var ticket = input.Ticket.MapTo<Ticket>();
            await _ticketManager.AddTicketAsync(ticket);
        }

        public async Task UpdateTicketAsync(UpdateTicketInput input)
        {
            var ticket = input.Ticket.MapTo<Ticket>();
            await _ticketManager.UpdateTicketAysnc(ticket);
        }

        public async Task RemoveTicketAsync(int ticketId)
        {
            await _ticketManager.RemoveTicketAsync(ticketId);
        }

        public async Task<TicketDto> GetTicketByIdAsync(int ticketId)
        {
            var ticket = await _ticketManager.GetTicketByIdAsync(ticketId);
            return ticket.MapTo<TicketDto>();
        }

        public async Task<IPagedResult<TicketDto>> GetPagedTicketsAsync(GetPagedTicketsInput input)
        {
            var count = await _ticketManager.GetDestTicketsCountAsync(input.DestId);
            var tickets =  _ticketManager.GetPagedTickets(input.DestId, input);

            return new PagedResultOutput<TicketDto>()
            {
                TotalCount = count,
                Items = tickets.MapTo<List<TicketDto>>()
            };
        }

        public async Task AddTicketQuoteAsync(AddTicketQuoteInput input)
        {
            var quote = input.TicketQuote.MapTo<TicketQuote>();
            await _ticketQuoteManager.AddTicketQuoteAsync(quote);
        }

        public async Task UpdateTicketQuoteAsync(UpdateTicketQuoteInput input)
        {
            var quote = input.TicketQuote.MapTo<TicketQuote>();
            await _ticketQuoteManager.UpdateTicketQuoteAsync(quote);
        }

        public async Task RemoveTicketQuoteAsync(int ticketQuoteId)
        {
            await _ticketQuoteManager.RemoveTicketQuoteAsync(ticketQuoteId);
        }

        public async Task<TicketQuoteDto> GetTicketQuoteByIdAsync(int ticektQuoteId)
        {
            var quote = await _ticketQuoteManager.GetTicketQuoteByIdAsync(ticektQuoteId);
            return quote.MapTo<TicketQuoteDto>();
        }

        public async Task<IPagedResult<TicketQuoteDto>> GetPagedTicketQuotesByTicektId(GetPagedTicketQuotesInput input)
        {
            var count = await _ticketQuoteManager.GetTicketQuotesCountAsync(input.TicketId);
            var quotes = _ticketQuoteManager.GetPagedTicketQuotesByTicketId(input.TicketId, input);

            return new PagedResultOutput<TicketQuoteDto>()
            {
                TotalCount = count,
                Items = quotes.MapTo<List<TicketQuoteDto>>()
            };
        }

        public async Task AddTicketOrderAsync(AddTicketOrderInput input)
        {
            var order = input.TicketOrder.MapTo<TicketOrder>();
            await _ticketOrderManager.AddTicketOrderAsync(order);
        }

        public async Task UpdateTicketOrderAsync(UpdateTicketOrderInput input)
        {
            var order = input.TicketOrder.MapTo<TicketOrder>();
            await _ticketOrderManager.UpdateTicketOrderAsync(order);
        }

        public async Task RemoveTicketOrderAsync(int ticketOrderId)
        {
            await _ticketOrderManager.RemoveTicketOrderAsync(ticketOrderId);
        }

        public async Task<TicketOrderDto> GetTicketOrderByIdAsync(int ticektOrderId)
        {
            var order = await _ticketOrderManager.GetTicketOrderByIdAsync(ticektOrderId);
            return order.MapTo<TicketOrderDto>();
        }

        public async Task<IPagedResult<TicketOrderDto>> GetPagedTicketOrdersByTicektId(GetPagedTicketOrdersInput input)
        {
            var count = await _ticketOrderManager.GetTicketOrdersCountAsync();
            var orders = _ticketOrderManager.GetPagedTicketOrders(input);

            return new PagedResultOutput<TicketOrderDto>()
            {
                TotalCount = count,
                Items = orders.MapTo<List<TicketOrderDto>>()
            };
        }
    }
}
