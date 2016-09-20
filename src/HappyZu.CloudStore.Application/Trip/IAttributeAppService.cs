using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    public interface IAttributeAppService : IApplicationService
    {
        #region DestAttribute

        Task<ResultOutputDto> AddDestAttributeAsync(AddDestAttributeInput input);

        Task<ResultOutputDto> UpdateDestAttributeAsync(UpdateDestAttributeInput input);

        Task<ResultOutputDto> RemoveDestAttributeAsync(int destAttributeId);

        Task<DestAttributeDto> GetDestAttributeByIdAsync(int destAttributeId);

        Task<IList<DestAttributeDto>> GetSubDestAttributesByParentIdAsync(int parentId);

        #endregion

        #region TicketAttribute

        Task<ResultOutputDto> AddTicketAttributeAsync(AddTicketAttributeInput input);

        Task<ResultOutputDto> UpdateTicketAttributeAsync(UpdateTicketAttributeInput input);

        Task<ResultOutputDto> RemoveTicketAttributeAsync(int ticketAttributeId);

        Task<TicketAttributeDto> GetTicketAttributeByIdAsync(int ticketAttributeId);

        Task<IList<TicketAttributeDto>> GetSubTicketAttributesByParentIdAsync(int parentId);

        #endregion
    }
}
