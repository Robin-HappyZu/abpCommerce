using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    public interface IETicketAppService : IApplicationService
    {
        Task CreateETicketAsync(int ticketId, int ticketOrderId, int ticketOrderItemId, string description);

        Task<bool> CheckInAsync(int serialNo, string hash, int checkerId);

        Task<bool> IsValidAsync(int serialNo, string hash);

        Task<IList<ETicketDto>> GetETicketsByTicketOrderIdAsync(int ticketOrderId);

        Task<ETicketDto> GetETicketByIdAsync(int eTicketId);
    }
}
