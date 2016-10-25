using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
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

        public async Task<ResultOutputDto> SubmitCustomizationAsync(CustomizeTripDto trip)
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

        public async Task<IPagedResult<CustomizeTripDto>> QueryCustomizationsAsync(QueryCustomizationsInput input)
        {
            try
            {
                if (input.MaxResultCount<=0)
                {
                    input.MaxResultCount = int.MaxValue;
                }
                var count =await _customizeTripRepository.CountAsync(x => x.CustomerId == input.CustomerId);
                var result =
                    _customizeTripRepository.Query(q => q.Where(x => x.CustomerId == input.CustomerId))
                        .OrderByDescending(x => x.Id)
                        .PageBy(input).ToList();

                return new PagedResultDto<CustomizeTripDto>()
                {
                    TotalCount = count,
                    Items = result.MapTo<IReadOnlyList<CustomizeTripDto>>()
                };
            }
            catch (Exception)
            {
                return new PagedResultDto<CustomizeTripDto>()
                {
                    TotalCount = 0,
                    Items = new List<CustomizeTripDto>()
                };
            }
        }

        public async Task<ResultOutputDto> RemoveCustomizationsAsync(IList<int> idList)
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

        public async Task<ResultOutputDto> UpdateCustomizationAsync(CustomizeTripDto trip)
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
