using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HappyZu.CloudStore.Common.Dto;
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

        public async Task<ResultOutputDto> AddTicketAsync(AddTicketInput input)
        {
            try
            {
                var ticket = input.Ticket.MapTo<Ticket>();
                await _ticketManager.AddTicketAsync(ticket);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> UpdateTicketAsync(UpdateTicketInput input)
        {
            try
            {
                var ticket = input.Ticket.MapTo<Ticket>();
                await _ticketManager.UpdateTicketAysnc(ticket);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> RemoveTicketAsync(int ticketId)
        {
            try
            {
                await _ticketManager.RemoveTicketAsync(ticketId);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<TicketDto> GetTicketByIdAsync(int ticketId)
        {
            try
            {
                var ticket = await _ticketManager.GetTicketByIdAsync(ticketId);
                return ticket.MapTo<TicketDto>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IPagedResult<TicketDto>> GetPagedTicketsAsync(GetPagedTicketsInput input)
        {
            try
            {
                var count = await _ticketManager.GetDestTicketsCountAsync(input.DestId);
                var tickets =  _ticketManager.GetPagedTickets(input.DestId, input);

                return new PagedResultOutput<TicketDto>()
                {
                    TotalCount = count,
                    Items = tickets.MapTo<List<TicketDto>>()
                };
            }
            catch (Exception)
            {
                return new PagedResultOutput<TicketDto>()
                {
                    TotalCount = 0,
                    Items = new List<TicketDto>()
                };
            }
        }

        public async Task<ResultOutputDto> AddTicketQuoteAsync(AddTicketQuoteInput input)
        {
            try
            {
                var quote = input.TicketQuote.MapTo<TicketQuote>();
                await _ticketQuoteManager.AddTicketQuoteAsync(quote);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> UpdateTicketQuoteAsync(UpdateTicketQuoteInput input)
        {
            try
            {
                var quote = input.TicketQuote.MapTo<TicketQuote>();
                await _ticketQuoteManager.UpdateTicketQuoteAsync(quote);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> RemoveTicketQuoteAsync(int ticketQuoteId)
        {
            try
            {
                await _ticketQuoteManager.RemoveTicketQuoteAsync(ticketQuoteId);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<TicketQuoteDto> GetTicketQuoteByIdAsync(int ticektQuoteId)
        {
            try
            {
                var quote = await _ticketQuoteManager.GetTicketQuoteByIdAsync(ticektQuoteId);
                return quote.MapTo<TicketQuoteDto>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IPagedResult<TicketQuoteDto>> GetPagedTicketQuotesByTicektId(GetPagedTicketQuotesInput input)
        {
            try
            {
                var count = await _ticketQuoteManager.GetTicketQuotesCountAsync(input.TicketId);
                var quotes = _ticketQuoteManager.GetPagedTicketQuotesByTicketId(input.TicketId, input);

                return new PagedResultOutput<TicketQuoteDto>()
                {
                    TotalCount = count,
                    Items = quotes.MapTo<List<TicketQuoteDto>>()
                };
            }
            catch (Exception)
            {
                return new PagedResultDto<TicketQuoteDto>()
                {
                    TotalCount = 0,
                    Items = new List<TicketQuoteDto>()
                };
            }
        }

        public async Task<ResultOutputDto> AddTicketOrderAsync(AddTicketOrderInput input)
        {
            try
            {
                var order = input.TicketOrder.MapTo<TicketOrder>();
                var orderItems = input.TicketOrderItems.MapTo<List<TicketOrderItem>>();
                await _ticketOrderManager.AddTicketOrderAsync(order);
                await _ticketOrderManager.AddTicketOrderDetailsAsync(orderItems);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> UpdateTicketOrderAsync(UpdateTicketOrderInput input)
        {
            try
            {
                var order = input.TicketOrder.MapTo<TicketOrder>();
                await _ticketOrderManager.UpdateTicketOrderAsync(order);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> RemoveTicketOrderAsync(int ticketOrderId)
        {
            try
            {
                await _ticketOrderManager.RemoveTicketOrderAsync(ticketOrderId);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<TicketOrderDto> GetTicketOrderByIdAsync(int ticektOrderId)
        {
            try
            {
                var order = await _ticketOrderManager.GetTicketOrderByIdAsync(ticektOrderId);
                return order.MapTo<TicketOrderDto>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IPagedResult<TicketOrderDto>> GetPagedTicketOrdersByTicektId(GetPagedTicketOrdersInput input)
        {
            try
            {
                var count = await _ticketOrderManager.GetTicketOrdersCountAsync();
                var orders = _ticketOrderManager.GetPagedTicketOrders(input);

                return new PagedResultOutput<TicketOrderDto>()
                {
                    TotalCount = count,
                    Items = orders.MapTo<List<TicketOrderDto>>()
                };
            }
            catch (Exception)
            {
                return new PagedResultOutput<TicketOrderDto>()
                {
                    TotalCount = 0,
                    Items = new List<TicketOrderDto>()
                };
            }
        }

        public async Task<ResultOutputDto> AttachTicketAttributeRecordAsync(AttachTicketAttributeRecordInput input)
        {
            try
            {
                var records = input.TicketAttributeRecords.MapTo<List<TicketAttributeRecord>>();
                await _ticketManager.AttachTicketAttributeRecordAsync(records);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> DetachTicketAttributeRecordAsync(DetachTicketAttributeRecordInput input)
        {
            try
            {
                var records = input.TicketAttributeRecords.MapTo<List<TicketAttributeRecord>>();
                await _ticketManager.DetachTicketAttributeRecordAsync(records);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<List<TicketAttributeRecordDto>> GetAllAttributeRecordByTicketIdAsync(int ticketId)
        {
            try
            {
                var records = await _ticketManager.GetAttributeRecordsByTicketIdAsync(ticketId);
                return records.MapTo<List<TicketAttributeRecordDto>>();
            }
            catch (Exception)
            {
                return new List<TicketAttributeRecordDto>();
            }
        }
    }
}
