using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HappyZu.CloudStore.Common.Dto;
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
            try
            {
                var provinces = await _destLocationManager.GetAllDestProvicesAsync(countryType);

                return new ListResultDto<DestProvinceDto>(provinces.MapTo<List<DestProvinceDto>>());
            }
            catch (Exception e)
            {
                return new ListResultDto<DestProvinceDto>();
            }
        }

        public async Task<IListResult<DestProvinceDto>> GetDestProvincesAsync(GetDestProvincesInput input)
        {
            try
            {
                var countryType = CountryType.Domestic;
                try
                {
                    countryType = (CountryType) input.CountryType;
                }
                catch
                {
                    // ignored
                }

                var provinces = await _destLocationManager.GetDestProvicesAsync(countryType, input );

                return new ListResultDto<DestProvinceDto>(provinces.MapTo<List<DestProvinceDto>>());
            }
            catch (Exception)
            {
                return new ListResultDto<DestProvinceDto>();
            }
        }

        public async Task<DestProvinceDto> GetDestProvinceByIdAsync(int id)
        {
            try
            {
                var province = await _destLocationManager.GetDestProvinceByIdAsync(id);

                return province.MapTo<DestProvinceDto>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IListResult<DestCityDto>> GetDestCitiesByDestProvinceIdAsync(GetDestCitiesInput input)
        {
            try
            {
                var cities = await _destLocationManager.GetDestCitiesByProvinceIdAsync(input.ProvinceId,input);
                return new ListResultDto<DestCityDto>(cities.MapTo<List<DestCityDto>>());
            }
            catch (Exception)
            {
                return new ListResultDto<DestCityDto>();
            }
        }

        public async Task<DestCityDto> GetDestCityByIdAsync(int id)
        {
            try
            {
                var city = await _destLocationManager.GetDestCityByIdAsync(id);

                return city.MapTo<DestCityDto>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ResultOutputDto> AddDestProvinceAsync(AddDestProvinceInput input)
        {
            try
            {
                var provice = new DestProvince()
                {
                    Name = input.Province.Name,
                    DestType = input.Province.DestType,
                    IsDeleted = input.Province.IsDeleted
                };

                await _destLocationManager.InsertDestProvinceAsync(provice);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> UpdateDestProvinceAsync(UpdateDestProvinceInput input)
        {
            try
            {
                var provice = await _destLocationManager.GetDestProvinceByIdAsync(input.Id);
                provice.Name = input.Province.Name;
                provice.DestType = input.Province.DestType;

                await _destLocationManager.UpdateDestProvinceAsync(provice);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> RemoveDestProvinceAsync(int provinceId)
        {
            try
            {
                await _destLocationManager.RemoveDestProvinceAsync(provinceId);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> AddDestCityAsync(AddDestCityInput input)
        {
            try
            {
                var city = new DestCity()
                {
                    Name = input.City.Name,
                    ProvinceId = input.City.ProvinceId,
                    DestCount = input.City.DestCount
                };

                await _destLocationManager.InsertDestCityAsync(city);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> UpdateDestCityAsync(UpdateDestCityInput input)
        {
            try
            {
                var city = await _destLocationManager.GetDestCityByIdAsync(input.Id);
                city.Name = input.City.Name;
                city.ProvinceId = input.City.ProvinceId;
                city.DestCount = input.City.DestCount;

                await _destLocationManager.UpdateDestCityAsync(city);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> RemoveDestCityAsync(int cityId)
        {
            try
            {
                await _destLocationManager.RemoveDestCityAsync(cityId);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<IPagedResult<DestDto>> GetDestsByLocationAsync(GetDestsInput input)
        {
            try
            {
                var count = await _destMananger.GetDestsCountAsync(input.ProvinceId, input.CityId);
                var dests = _destMananger.GetDestsByLocation(input.ProvinceId, input.CityId, input);
                return new PagedResultOutput<DestDto>()
                {
                    TotalCount = count,
                    Items = dests.MapTo<List<DestDto>>()
                };
            }
            catch (Exception)
            {
                return new PagedResultOutput<DestDto>()
                {
                    TotalCount = 0,
                    Items = new List<DestDto>()
                };
            }
        }

        public async Task<ResultOutputDto> AddDestAsync(AddDestInput input)
        {
            try
            {
                var dest = input.Dest.MapTo<Dest>();

                var id = await _destMananger.AddDestAsync(dest);

                return ResultOutputDto.Success(id);
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> RemoveDestAsync(int destId)
        {
            try
            {
                await _destMananger.RemoveDestAsync(destId);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> UpdateDestAsync(UpdateDestInput input)
        {
            try
            {
                var dest = await _destMananger.GetDestByIdAsync(input.Dest.Id);
                await _destMananger.UpdateDestAsync(dest);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> AttachDestAttributeRecordAsync(AttachDestAttributeRecordInput input)
        {
            try
            {
                var records = input.DestAttributeRecords.MapTo<List<DestAttributeRecord>>();
                await _destMananger.AttachDestAttributeRecordAsync(records);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> DetachDestAttributeRecordAsync(DetachDestAttributeRecordInput input)
        {
            try
            {
                var records = input.DestAttributeRecords.MapTo<List<DestAttributeRecord>>();
                await _destMananger.DetachDestAttributeRecordAsync(records);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<List<DestAttributeRecordDto>> GetAllAttributeRecordByDestIdAsync(int destId)
        {
            try
            {
                var records = await _destMananger.GetAttributeRecordsByDestIdAsync(destId);
                return records.MapTo<List<DestAttributeRecordDto>>();
            }
            catch (Exception)
            {
                return new List<DestAttributeRecordDto>();
            }
        }
    }
}
