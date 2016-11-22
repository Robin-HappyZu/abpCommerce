using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.Entities;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    public class TicketAppService : CloudStoreAppServiceBase, ITicketAppService
    {
        private readonly TicketManager _ticketManager;
        private readonly TicketQuoteManager _ticketQuoteManager;
        private readonly TicketOrderManager _ticketOrderManager;
        private readonly TicketTypeManager _ticketTypeManager;
        private readonly ETicketManager _eTicketManager;
        private readonly DestMananger _destManager;
        private readonly IUniqueIdManager _uniqueIdManager;

        public TicketAppService(TicketManager ticketManager, TicketQuoteManager ticketQuoteManager,
            TicketOrderManager ticketOrderManager, TicketTypeManager ticketTypeManager, ETicketManager eTicketManager, 
            DestMananger destManager, IUniqueIdManager uniqueIdManager)
        {
            _ticketManager = ticketManager;
            _ticketOrderManager = ticketOrderManager;
            _ticketTypeManager = ticketTypeManager;
            _ticketQuoteManager = ticketQuoteManager;
            _eTicketManager = eTicketManager;
            _destManager = destManager;
            _uniqueIdManager = uniqueIdManager;
        }

        public async Task<ResultOutputDto> AddTicketAsync(AddTicketInput input)
        {
            try
            {
                var ticket = input.Ticket.MapTo<Ticket>();
                var id = await _ticketManager.AddTicketAndGetIdAsync(ticket);
                return ResultOutputDto.Success(id);
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
                var ticket = await _ticketManager.GetTicketByIdAsync(input.Ticket.Id);

                ticket.AdvanceBookingDays = input.Ticket.AdvanceBookingDays;
                ticket.AgentPrice = input.Ticket.AgentPrice;
                ticket.CanPayFrontMoney = input.Ticket.CanPayFrontMoney;
                ticket.CanUsePoint = input.Ticket.CanUsePoint;
                ticket.CostPrice = input.Ticket.CostPrice;
                ticket.Description = input.Ticket.Description;
                ticket.EndTime = input.Ticket.EndTime;
                ticket.EndDate = input.Ticket.EndDate;
                ticket.FrontMoneyPrice = input.Ticket.FrontMoneyPrice;
                ticket.Inventory = input.Ticket.Inventory;
                ticket.MarketPrice = input.Ticket.MarketPrice;
                ticket.MustAdvance = input.Ticket.MustAdvance;
                ticket.Name = input.Ticket.Name;
                ticket.Points = input.Ticket.Points;
                ticket.Price = input.Ticket.Price;
                ticket.TypeId = input.Ticket.TypeId;
                ticket.UsePoints = input.Ticket.UsePoints;

                await _ticketManager.UpdateTicketAysnc(ticket);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> UpdateTicketQuoteTypeAsync(UpdateTicketInput input)
        {
            try
            {
                var ticket = await _ticketManager.GetTicketByIdAsync(input.Ticket.Id);

                ticket.QuotesType = input.Ticket.QuotesType;

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
                
                var tickets = await _ticketManager.QuerysListAsync(m=>m.Where(x=>x.DestId==input.DestId).OrderBy(x=>x.Id), input);

                return new PagedResultDto<TicketDto>()
                {
                    TotalCount = count,
                    Items = tickets.MapTo<List<TicketDto>>()
                };
            }
            catch (Exception ex)
            {
                return new PagedResultDto<TicketDto>()
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
                var quote =await _ticketQuoteManager.GetTicketQuoteByIdAsync(input.TicketQuote.Id);
                quote.IsDisplay = input.TicketQuote.IsDisplay;
                quote.Quote = input.TicketQuote.Quote;
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
                Func<IQueryable<TicketQuote>, IQueryable<TicketQuote>> query = null;
               
                var now = DateTime.Now;
                if (input.MaxDate!=null && input.MinDate != null)
                {
                    query = q => q.Where(x => x.IsDisplay == input.IsDisplay && 
                                     x.TicketId == input.TicketId && x.DateTime>=input.MinDate && x.DateTime<=input.MaxDate)
                                     .OrderBy(x => x.DateTime);
                }
                else if (input.MaxDate != null)
                {
                    query = q => q.Where(x => x.IsDisplay == input.IsDisplay &&
                                     x.TicketId == input.TicketId && x.DateTime >= DateTime.Now.AddDays(-1) && x.DateTime <= input.MaxDate)
                                     .OrderBy(x => x.DateTime);
                }
                else if (input.MinDate != null)
                {
                    query = q => q.Where(x => x.IsDisplay == input.IsDisplay &&
                                     x.TicketId == input.TicketId && x.DateTime >= input.MinDate)
                                     .OrderBy(x => x.DateTime);
                }
                else
                {
                    query = q => q.Where(x => x.IsDisplay == input.IsDisplay &&
                                     x.TicketId == input.TicketId)
                                     .OrderBy(x => x.DateTime);
                }
                var count = await _ticketQuoteManager.QueryCountAsync(query);
                var quotes =await _ticketQuoteManager.QuerysListAsync(query, input);

                return new PagedResultDto<TicketQuoteDto>()
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

        public async Task<IPagedResult<TicketQuoteDto>> GetTicketQuotesByTicketId(int ticketId)
        {
            try
            {
                Func<IQueryable<TicketQuote>, IQueryable<TicketQuote>> query =  q => q.Where(x => x.IsDisplay  &&
                                    x.TicketId == ticketId)
                                    .OrderBy(x => x.DateTime); 
                var count = await _ticketQuoteManager.QueryCountAsync(query);
                var quotes = await _ticketQuoteManager.QuerysListAsync(query,new PagedResultRequestDto());

                return new PagedResultDto<TicketQuoteDto>()
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
                var order = new TicketOrder()
                {
                    OrderNo = _uniqueIdManager.CreateId().ToString(),
                    Contact = input.Contact,
                    Remark = input.Remark,
                    Mobile = input.Mobile,
                    AgentId = input.AgentId
                };

                if (AbpSession.UserId != null) order.CustomerId = AbpSession.UserId.Value;
                order.Count = input.Tickets.Sum(item => item.Quantity);
                
                

                var orderItems = new List<TicketOrderItem>();

                // 订单明细
                foreach (var item in input.Tickets)
                {
                    var ticket = await _ticketManager.GetTicketByIdAsync(item.TicketId);
                    var dest = await _destManager.GetDestByIdAsync(ticket.DestId);
                    var quote = await _ticketQuoteManager.GetTicketQuoteByIdAsync(item.TicketQuoteId);
                    order.DestName = dest.Title;

                    orderItems.Add(new TicketOrderItem()
                    {
                        TicketOrderItemNo = _uniqueIdManager.CreateId().ToString(),
                        TicketId = ticket.Id,
                        TicketName = ticket.Name,
                        Quantity = item.Quantity,
                        AgentPrice=quote.Quote.AgentPrice,
                        UnitPrice = quote.Quote.Price,
                        Price = item.Quantity * quote.Quote.Price,
                        Date = quote.DateTime
                    });
                }

                order.TotalAmount = orderItems.Sum(item => item.Price);

                // 添加订单
                var orderId = await _ticketOrderManager.AddTicketOrderAndGetIdAsync(order);

                foreach (var orderItem in orderItems)
                {
                    orderItem.TicketOrderId = orderId;
                }

                // 添加订单明细
                await _ticketOrderManager.AddTicketOrderDetailsAsync(orderItems);

                order.Status=OrderStatus.Paying;
                return ResultOutputDto.Success(orderId);
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

        public async Task<TicketOrderDto> GetTicketOrderByIdAsync(int ticektOrderId, long userId)
        {
            try
            {
                var order = await _ticketOrderManager.GetTicketOrdersAsync(q=>q.Where(x=>x.Id== ticektOrderId && x.CustomerId==userId).OrderBy(x=>x.Id),new PagedResultRequestDto
                {
                    MaxResultCount=1
                });
                if (order.Count<=0)
                {
                    return null;
                }
                return order.FirstOrDefault().MapTo<TicketOrderDto>();
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
                //TODO : need to be fixed
                var count = await _ticketOrderManager.GetTicketOrdersCountAsync(null);
                var orders = _ticketOrderManager.GetTicketOrdersAsync(null, input);

                return new PagedResultDto<TicketOrderDto>()
                {
                    TotalCount = count,
                    Items = orders.MapTo<List<TicketOrderDto>>()
                };
            }
            catch (Exception)
            {
                return new PagedResultDto<TicketOrderDto>()
                {
                    TotalCount = 0,
                    Items = new List<TicketOrderDto>()
                };
            }
        }

        public async Task<IPagedResult<TicketOrderDto>> GetTicketOrdersAsync(GetPagedTicketOrdersInput input)
        {
            try
            {
                Func<IQueryable<TicketOrder>, IQueryable<TicketOrder>> query=null;
                if (input.UserId>0)
                {
                    if (input.OrderStatus != null)
                    {
                        query = q =>q.Where(x => x.CustomerId == input.UserId && x.Status==input.OrderStatus)
                                                     .OrderByDescending(x => x.CreationTime)
                                                     .ThenByDescending(x => x.Id);
                    }
                    else
                    {
                        query = q => q.Where(x => x.CustomerId == input.UserId)
                             .OrderByDescending(x => x.CreationTime)
                             .ThenByDescending(x => x.Id);
                    }
                }
                else
                {
                    if (input.OrderStatus != null)
                    {
                        query = q => q.Where(x => x.Status == input.OrderStatus)
                                                     .OrderByDescending(x => x.CreationTime)
                                                     .ThenByDescending(x => x.Id);
                    }
                    else
                    {
                        query = q => q.OrderByDescending(x => x.CreationTime)
                             .ThenByDescending(x => x.Id);
                    }
                }
                
                var count = await _ticketOrderManager.GetTicketOrdersCountAsync(query);
                var orders = await _ticketOrderManager.GetTicketOrdersAsync(query, input);
                return new PagedResultDto<TicketOrderDto>()
                {
                    TotalCount = count,
                    Items = orders.MapTo<List<TicketOrderDto>>()
                };
            }
            catch (Exception)
            {
                return new PagedResultDto<TicketOrderDto>()
                {
                    TotalCount = 0,
                    Items = new List<TicketOrderDto>()
                };
            }
        }

        private async Task SetTicketOrderStatusAsync(TicketOrder order, OrderStatus os, bool notifyCustomer = false)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            OrderStatus preOrderStatus = order.Status;
            if (preOrderStatus == os) return;

            order.Status = os;
            await _ticketOrderManager.UpdateTicketOrderAsync(order);

            // 通知订单状态
            if (notifyCustomer)
            {
                switch (os)
                {
                    case OrderStatus.Completed:
                        break;
                }
            }
        }

        public bool CanCancelTicketOrder(TicketOrder order)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            return order.Status == OrderStatus.Pending || order.Status == OrderStatus.Paying;
        }

        public async Task<ResultOutputDto> CancelTicketOrderAsync(TicketOrder order, bool notifyCustomer)
        {
            try
            {
                if (order == null)
                    return ResultOutputDto.Exception(new ArgumentNullException("order"));

                if (!CanCancelTicketOrder(order))
                    return ResultOutputDto.Fail(200, $"{order.Id}");

                await SetTicketOrderStatusAsync(order, OrderStatus.Closed, notifyCustomer);
                // 如果有扣库存 这里就需要调整库存
                // 发布订单取消事件
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public bool CanMarkTicketOrderAsPaid(TicketOrder order)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            return order.Status == OrderStatus.Paying;
        }

        public async Task<ResultOutputDto> MarkTicketOrderAsPaidAsync(TicketOrder order)
        {
            try
            {
                if (order == null)
                    return ResultOutputDto.Exception(new ArgumentNullException("order"));

                if (!CanMarkTicketOrderAsPaid(order))
                    return ResultOutputDto.Fail(200, $"TicketOrder: {order.Id} can not mark as paid");
            
                await SetTicketOrderStatusAsync(order, OrderStatus.Paid, true);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public bool CanRefundTicketOrder(TicketOrder order)
        {
            if (order == null)
                throw new ArgumentNullException("order");

            return order.Status == OrderStatus.Paid;
        }

        public Task<ResultOutputDto> RefundTicketOrderAsync(TicketOrder order)
        {
            throw new NotImplementedException();
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

        public async Task<IPagedResult<TicketTypeDto>> GetTicketTypeListAsync(int destId)
        {
            try
            {
               var list = await _ticketTypeManager.GetListAsync(destId);
                return new PagedResultDto<TicketTypeDto>(list.Count, list.MapTo<List<TicketTypeDto>>());
            }
            catch (Exception)
            {
                return new PagedResultDto<TicketTypeDto>(0,new List<TicketTypeDto>());
            }
           
        }

        public async Task<TicketTypeDto> GetTicketTypeAsync(int id)
        {
            try
            {
                var entity=await _ticketTypeManager.GetAsync(new EntityDto(id));
                return entity.MapTo<TicketTypeDto>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResultOutputDto> AddTicketTypeAsync(AddTicketTypeInput input)
        {
            try
            {
                var ticketType=new TicketType()
                {
                    DestId = input.DestId,
                    Name = input.Name,
                    DisplayOrder = input.DisplayOrder
                };

                await _ticketTypeManager.AddAsync(ticketType);

                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> RemoveTicketTypeAsync(int id)
        {
            try
            {
                await _ticketTypeManager.RemoveAsync(new EntityDto(id));
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<IList<TicketOrderItemDto>> GetTicketOrderItemsByTicketOrderIdAsync(int ticketOrderId)
        {
            var items = await _ticketOrderManager.GetTicketOrderDetailsByTicketOrderIdAsync(ticketOrderId);

            return items.MapTo<List<TicketOrderItemDto>>();
        }

        public async Task<IPagedResult<ETicketDto>> GetETicketsAsync(GetPagedETicketsInput input)
        {
            try
            {
                Func<IQueryable<ETicket>, IQueryable<ETicket>> query = null;
                if (input.SerialNo > 0 && input.TicketOrderId >  0 )
                {                    
                    query = t =>
                    {
                        return t.Where(o => o.SerialNo == input.SerialNo && o.TicketOrderId == input.TicketOrderId).OrderBy(o=>o.Id);
                    };
                }
                else
                {
                    if (input.SerialNo > 0)
                    {
                        query = t =>
                        {
                            return t.Where(o => o.SerialNo == input.SerialNo).OrderBy(o => o.Id);
                        };
                    }

                    if (input.TicketOrderId > 0)
                    {
                        query = t =>
                        {
                            return t.Where(o => o.TicketOrderId == input.TicketOrderId).OrderBy(o => o.Id);
                        };
                    }
                }

                var count = await _eTicketManager.GetETicketsCountAsync(query);
                var items = await _eTicketManager.GetETicketsAsync(query, input);

                return new PagedResultDto<ETicketDto>()
                {
                    TotalCount = count,
                    Items = items.MapTo<List<ETicketDto>>()
                };
            }
            catch (Exception)
            {
                return new PagedResultDto<ETicketDto>()
                {
                    TotalCount = 0,
                    Items = new List<ETicketDto>()
                };
            }
        }
    }
}
