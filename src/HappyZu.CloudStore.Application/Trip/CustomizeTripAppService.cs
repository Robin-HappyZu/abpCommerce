using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    public class CustomizeTripAppService : ICustomizeTripAppService
    {
        private readonly IRepository<CustomizeTrip> _customizeTripRepository;

        public CustomizeTripAppService(IRepository<CustomizeTrip> customizeTripRepository)
        {
            _customizeTripRepository = customizeTripRepository;
        }

        public async Task<ResultOutputDto> SubmitCustomization(CustomizeTripDto trip)
        {
            try
            {
                var customization = trip.MapTo<CustomizeTrip>();
                customization.Id = 0;

                await _customizeTripRepository.InsertAsync(customization);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<IList<CustomizeTripDto>> GetCustomizationsByCustomerIdAsync(long customerId)
        {
            try
            {
                var customizations = await _customizeTripRepository.GetAllListAsync(c => c.CustomerId == customerId);

                return customizations.MapTo<List<CustomizeTripDto>>();
            }
            catch (Exception e)
            {
                return new List<CustomizeTripDto>();
            }
        }

        public async Task<ResultOutputDto> RemoveCustomizations(IList<int> idList)
        {
            try
            {
                foreach (var id in idList)
                {
                    await _customizeTripRepository.DeleteAsync(id);
                }

                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> UpdateCustomization(CustomizeTripDto trip)
        {
            try
            {
                var customization = await _customizeTripRepository.GetAsync(trip.Id);
                if (customization == null)
                {
                    return ResultOutputDto.Fail(0, "定制不存在");
                }

                customization.Contact = trip.Contact;
                customization.Email = trip.Email;
                customization.Mobile = trip.Mobile;

                await _customizeTripRepository.UpdateAsync(customization);

                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }
    }
}
