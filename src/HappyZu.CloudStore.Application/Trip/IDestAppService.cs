using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    public interface IDestAppService : IApplicationService
    {
        #region 景点地址

        /// <summary>
        /// 获取景点省份列表
        /// </summary>
        /// <param name="countryType"></param>
        /// <returns></returns>
        Task<IListResult<DestProvinceDto>> GetAllDestProvincesAsync(CountryType countryType = CountryType.Domestic);

        Task<IListResult<DestCityDto>> GetDestCitiesByDestProvinceIdAsync(int destProviceId);

        Task AddDestProvinceAsync(AddDestProvinceInput input);

        Task UpdateDestProvinceAsync(UpdateDestProvinceInput input);

        Task RemoveDestProvinceAsync(int provinceId);

        Task AddDestCityAsync(AddDestCityInput input);

        Task UpdateDestCityAsync(UpdateDestCityInput input);

        Task RemoveDestCityAsync(int cityId);
        #endregion
            
        #region 景点

        Task<IPagedResult<DestDto>> GetDestsByLocationAsync(GetDestsInput input);

        Task AddDestAsync(AddDestInput input);

        Task RemoveDestAsync(int destId);

        Task UpdateDestAsync(UpdateDestInput input);

        #endregion
    }
}
