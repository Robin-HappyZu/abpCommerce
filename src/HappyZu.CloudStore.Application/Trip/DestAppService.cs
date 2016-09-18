using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    public class DestAppService : IDestAppService
    {
        private readonly DestLocationManager _destLocationManager;
        private readonly DestMananger _destMananger;

        public DestAppService(DestMananger destMananger, DestLocationManager destLocationManager)
        {
            _destMananger = destMananger;
            _destLocationManager = destLocationManager;
        }

        /// <summary>
        /// 获取景点省份列表
        /// </summary>
        /// <param name="countryType"></param>
        /// <returns></returns>
        public async Task<IListResult<DestProvinceDto>> GetAllDestProvincesAsync(CountryType countryType = CountryType.Domestic)
        {
            var provinces = await _destLocationManager.GetAllDestProvicesAsync(countryType);

            return new ListResultDto<DestProvinceDto>(provinces.MapTo<List<DestProvinceDto>>());
        }

        public async Task<IListResult<DestCityDto>> GetDestCitiesByDestProvinceIdAsync(int destProviceId)
        {
            var cities = await _destLocationManager.GetDestCitiesByProvinceIdAsync(destProviceId);
            return new ListResultDto<DestCityDto>(cities.MapTo<List<DestCityDto>>());
        }

        public async Task AddDestProvinceAsync(AddDestProvinceInput input)
        {
            var provice = new DestProvince()
            {
                Name = input.Province.Name,
                DestType = input.Province.DestType,
                IsDeleted = input.Province.IsDeleted
            };

            await _destLocationManager.InsertDestProvinceAsync(provice);
        }

        public async Task UpdateDestProvinceAsync(UpdateDestProvinceInput input)
        {
            var provice = await _destLocationManager.GetDestProvinceByIdAsync(input.Province.Id);
            provice.Name = input.Province.Name;
            provice.DestType = input.Province.DestType;

            await _destLocationManager.UpdateDestProvinceAsync(provice);
        }

        public async Task RemoveDestProvinceAsync(int provinceId)
        {
            await _destLocationManager.RemoveDestProvinceAsync(provinceId);
        }

        public async Task AddDestCityAsync(AddDestCityInput input)
        {
            var city = new DestCity()
            {
                Name = input.City.Name,
                ProvinceId = input.City.ProvinceId,
                DestCount = input.City.DestCount
            };

            await _destLocationManager.InsertDestCityAsync(city);
        }

        public async Task UpdateDestCityAsync(UpdateDestCityInput input)
        {
            var city = await _destLocationManager.GetDestCityByIdAsync(input.City.Id);
            city.Name = input.City.Name;
            city.ProvinceId = input.City.ProvinceId;
            city.DestCount = input.City.DestCount;

            await _destLocationManager.UpdateDestCityAsync(city);
        }

        public async Task RemoveDestCityAsync(int cityId)
        {
            await _destLocationManager.RemoveDestCityAsync(cityId);
        }

        public async Task<IPagedResult<DestDto>> GetDestsByLocationAsync(GetDestsInput input)
        {
            var count = await _destMananger.GetDestsCountAsync(input.ProvinceId, input.CityId);
            var dests = _destMananger.GetDestsByLocation(input.ProvinceId, input.CityId, input);
            return new PagedResultOutput<DestDto>()
            {
                TotalCount = count,
                Items = dests.MapTo<List<DestDto>>()
            };
        }

        public async Task AddDestAsync(AddDestInput input)
        {
            var dest = input.Dest.MapTo<Dest>();

            await _destMananger.AddDestAsync(dest);
        }

        public async Task RemoveDestAsync(int destId)
        {
            await _destMananger.RemoveDestAsync(destId);
        }

        public async Task UpdateDestAsync(UpdateDestInput input)
        {
            var dest = await _destMananger.GetDestByIdAsync(input.Dest.Id);

            await _destMananger.UpdateDestAsync(dest);
        }
    }
}
