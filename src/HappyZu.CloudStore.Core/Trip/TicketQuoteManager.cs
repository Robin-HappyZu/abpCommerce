using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

namespace HappyZu.CloudStore.Trip
{
    public class TicketQuoteManager : IDomainService
    {
        private readonly IRepository<TicketQuote> _ticketQuoteRepository;

        public TicketQuoteManager(IRepository<TicketQuote> ticketQuoteRepository)
        {
            _ticketQuoteRepository = ticketQuoteRepository;
        }

        #region 门票报价

        public async Task AddTicketQuoteAsync(TicketQuote ticketQuote)
        {
            await _ticketQuoteRepository.InsertAsync(ticketQuote);
        }

        public async Task UpdateTicketQuoteAsync(TicketQuote ticketQuote)
        {
            await _ticketQuoteRepository.UpdateAsync(ticketQuote);
        }

        public async Task RemoveTicketQuoteAsync(int ticketQuoteId)
        {
            await _ticketQuoteRepository.DeleteAsync(ticketQuoteId);
        }

        public async Task<TicketQuote> GetTicketQuoteByIdAsync(int ticketQuoteId)
        {
            return await _ticketQuoteRepository.GetAsync(ticketQuoteId);
        }

        public async Task<IList<TicketQuote>> GetTicketQuotesByTicketIdAsync(int ticketId)
        {
            return await _ticketQuoteRepository.GetAllListAsync(ticketQuote => ticketQuote.TicketId == ticketId);
        } 
        #endregion
    }
}
