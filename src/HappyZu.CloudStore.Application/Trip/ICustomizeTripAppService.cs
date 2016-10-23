using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    public interface ICustomizeTripAppService : IApplicationService
    {
        Task<ResultOutputDto> SubmitCustomizationAsync(CustomizeTripDto input);

        Task<IList<CustomizeTripDto>> GetCustomizationsByCustomerIdAsync(long customerId);

        Task<ResultOutputDto> RemoveCustomizationsAsync(IList<int> idList);

        Task<ResultOutputDto> UpdateCustomizationAsync(CustomizeTripDto trip);
    }
}
