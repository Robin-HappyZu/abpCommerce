using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    interface ICustomizeTripAppService : IApplicationService
    {
        Task<ResultOutputDto> SubmitCustomization(CustomizeTripDto input);

        Task<IList<CustomizeTripDto>> GetCustomizationsByCustomerIdAsync(long customerId);

        Task<ResultOutputDto> RemoveCustomizations(IList<int> idList);

        Task<ResultOutputDto> UpdateCustomization(CustomizeTripDto trip);
    }
}
