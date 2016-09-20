using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    public class AttributeAppService : IAttributeAppService
    {
        private readonly DestAttributeManager _destAttributeManager;
        private readonly TicketAttributeManager _ticketAttributeManager;

        public AttributeAppService(DestAttributeManager destAttributeManager,
            TicketAttributeManager ticketAttributeManager)
        {
            _destAttributeManager = destAttributeManager;
            _ticketAttributeManager = ticketAttributeManager;
        }

        public async Task<ResultOutputDto> AddDestAttributeAsync(AddDestAttributeInput input)
        {
            try
            {
                var destAttribute = input.DestAttribute.MapTo<DestAttribute>();
                await _destAttributeManager.AddDestAttributeAsync(destAttribute);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> UpdateDestAttributeAsync(UpdateDestAttributeInput input)
        {
            try
            {
                var destAttribute = input.DestAttribute.MapTo<DestAttribute>();
                await _destAttributeManager.UpdateDestAttributeAsync(destAttribute);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> RemoveDestAttributeAsync(int destAttributeId)
        {
            try
            {
                await _destAttributeManager.RemoveDestAttributeAsync(destAttributeId);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<DestAttributeDto> GetDestAttributeByIdAsync(int destAttributeId)
        {
            var attr = await _destAttributeManager.GetDestAttributeByIdAsync(destAttributeId);
            return attr.MapTo<DestAttributeDto>();
        }

        public async Task<IList<DestAttributeDto>> GetSubDestAttributesByParentIdAsync(int parentId)
        {
            var attributes = await _destAttributeManager.GetSubDestAttributesByParentId(parentId);
            return attributes.MapTo<List<DestAttributeDto>>();
        }

        public async Task<ResultOutputDto> AddTicketAttributeAsync(AddTicketAttributeInput input)
        {
            try
            {
                var attribute = input.TicketAttribute.MapTo<TicketAttribute>();
                await _ticketAttributeManager.AddTicketAttributeAsync(attribute);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> UpdateTicketAttributeAsync(UpdateTicketAttributeInput input)
        {
            try
            {
                var attribute = input.TicketAttribute.MapTo<TicketAttribute>();
                await _ticketAttributeManager.UpdateTicketAttributeAsync(attribute);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> RemoveTicketAttributeAsync(int ticketAttributeId)
        {
            try
            {
                await _ticketAttributeManager.RemoveTicketAttributeAsync(ticketAttributeId);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<TicketAttributeDto> GetTicketAttributeByIdAsync(int ticketAttributeId)
        {
            var attribute = await _ticketAttributeManager.GetTicketAttributeByIdAsync(ticketAttributeId);
            return attribute.MapTo<TicketAttributeDto>();
        }

        public async Task<IList<TicketAttributeDto>> GetSubTicketAttributesByParentIdAsync(int parentId)
        {
            var attributes = await _ticketAttributeManager.GetSubTicketAttributesByParentIdAsync(parentId);
            return attributes.MapTo<List<TicketAttributeDto>>();
        }
    }
}
