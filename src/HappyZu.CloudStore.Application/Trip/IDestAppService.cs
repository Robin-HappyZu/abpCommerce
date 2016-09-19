using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HappyZu.CloudStore.Common.Dto;
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
        Task<IListResult<DestProvinceDto>> GetDestProvincesAsync(GetDestProvincesInput input);

        Task<DestProvinceDto> GetDestProvinceByIdAsync(int id);

        Task<IListResult<DestCityDto>> GetDestCitiesByDestProvinceIdAsync(int destProviceId);

        Task<ResultOutputDto> AddDestProvinceAsync(AddDestProvinceInput input);

        Task<ResultOutputDto> UpdateDestProvinceAsync(UpdateDestProvinceInput input);

        Task<ResultOutputDto> RemoveDestProvinceAsync(int provinceId);

        Task<ResultOutputDto> AddDestCityAsync(AddDestCityInput input);

        Task<ResultOutputDto> UpdateDestCityAsync(UpdateDestCityInput input);

        Task<ResultOutputDto> RemoveDestCityAsync(int cityId);
        #endregion
            
        #region 景点

        Task<IPagedResult<DestDto>> GetDestsByLocationAsync(GetDestsInput input);

        Task<ResultOutputDto> AddDestAsync(AddDestInput input);

        Task<ResultOutputDto> RemoveDestAsync(int destId);

        Task<ResultOutputDto> UpdateDestAsync(UpdateDestInput input);

        #endregion
    }
}
