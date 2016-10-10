using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.FileManager;
using HappyZu.CloudStore.FileManager.Dto;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    public class DestAppService : IDestAppService
    {
        private readonly DestLocationManager _destLocationManager;
        private readonly DestMananger _destMananger;
        private readonly DestPictureMappingManager _pictureMappingManager;
        private readonly FileItemManager _fileItemManager;

        public DestAppService(DestMananger destMananger, DestLocationManager destLocationManager, 
            DestPictureMappingManager pictureMappingManager, FileItemManager fileItemManager)
        {
            _destMananger = destMananger;
            _destLocationManager = destLocationManager;
            _pictureMappingManager = pictureMappingManager;
            _fileItemManager = fileItemManager;
        }

        #region 景点目的地省份城市管理
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

        public async Task<DestCityDto> GetDefaultCity()
        {
            try
            {
                var city = await _destLocationManager.GetDefaultCity();

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

        #endregion

        #region 景点管理

        public async Task<IPagedResult<DestDto>> GetDestsByLocationAsync(GetDestsInput input)
        {
            try
            {
                var count = await _destMananger.QueryCountAsync(x=>x.Where(i=>i.CityId==input.CityId));
                var dests =await _destMananger.QuerysListAsync(x => x.Where(i => i.CityId == input.CityId).OrderBy(o=>o.DisplayOrder).ThenByDescending(o=>o.Id), input);
                return new PagedResultDto<DestDto>()
                {
                    TotalCount = count,
                    Items = dests.MapTo<List<DestDto>>()
                };
            }
            catch (Exception)
            {
                return new PagedResultDto<DestDto>()
                {
                    TotalCount = 0,
                    Items = new List<DestDto>()
                };
            }
        }

        public async Task<PagedResultDto<DestDto>> GetDestsAsync(GetDestsInput input)
        {
            try
            {
                var count = await _destMananger.QueryCountAsync();
                var dests = await _destMananger.QuerysListAsync(input);
                return new PagedResultDto<DestDto>()
                {
                    TotalCount = count,
                    Items = dests.MapTo<List<DestDto>>()
                };
            }
            catch (Exception)
            {
                return new PagedResultDto<DestDto>()
                {
                    TotalCount = 0,
                    Items = new List<DestDto>()
                };
            }
        }

        public async Task<DestDto> GetDestByIdAsync(int id)
        {
            try
            {
                var dest =await _destMananger.GetDestByIdAsync(id);
                return dest.MapTo<DestDto>();
            }
            catch (Exception)
            {
                return null;
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
                dest.CityId = input.Dest.CityId;
                dest.Address = input.Dest.Address;
                dest.Agreement = input.Dest.Agreement;
                dest.BookingNotice = input.Dest.BookingNotice;
                dest.Coding = input.Dest.Coding;
                dest.DestType = input.Dest.DestType;
                dest.Feature = input.Dest.Feature;
                dest.DisplayOrder = input.Dest.DisplayOrder;
                dest.Introduce = input.Dest.Introduce;
                dest.IsPublished = input.Dest.IsPublished;
                dest.Lat = input.Dest.Lat;
                dest.Lng = input.Dest.Lng;
                dest.MetaTitle = input.Dest.MetaTitle;
                dest.MetaKeywords = input.Dest.MetaKeywords;
                dest.MetaDescription = input.Dest.MetaDescription;
                dest.OpenTime = input.Dest.OpenTime;
                dest.ProvinceId = input.Dest.ProvinceId;
                dest.PublishDateTime = input.Dest.PublishDateTime;
                dest.Subject = input.Dest.Subject;
                dest.Title = input.Dest.Title;
                dest.SupplierId = input.Dest.SupplierId;
                dest.Supplier = input.Dest.Supplier;

                await _destMananger.UpdateDestAsync(dest);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }
        #endregion

        #region 景点属性

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

        #endregion

        #region 景点图片集

        public async Task<ResultOutputDto> AddDestPictureMapping(DestPictureMappingInput input)
        {
            try
            {
                var entity = input.DestPictureMapping.MapTo<DestPictureMapping>();
                var id=await _pictureMappingManager.AddAndGetIdAsync(entity);
                return ResultOutputDto.Success(id);
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> RemoveDestPictureMapping(DestPictureMappingInput input)
        {
            try
            {
                await _pictureMappingManager.RemoveByIdAsync(input.DestPictureMapping.Id);
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }

        public async Task<ResultOutputDto> SetDefaultDestPicture(DestPictureMappingInput input)
        {
            try
            {

                var entity=await _pictureMappingManager.GetByIdAsync(input.DestPictureMapping.Id);
                if (entity==null)
                {
                    return ResultOutputDto.Successed;
                }

                entity.IsDefault = true;
                await _pictureMappingManager.UpdateAsync(entity);

                var oldDefault =
                    await
                        _pictureMappingManager.QuerysListAsync(q => q.Where(x => x.IsDefault && x.Id != entity.Id),
                            new PagedResultRequestDto());
                foreach (var item in oldDefault)
                {
                    item.IsDefault = false;
                    await _pictureMappingManager.UpdateAsync(item);
                }
                return ResultOutputDto.Successed;
            }
            catch (Exception e)
            {
                return ResultOutputDto.Exception(e);
            }
        }
        
        public async Task<IPagedResult<FileItemMappingDto>> GetPagedDestPicturesAsync(GetPagedFileItemInput input)
        {
            try
            {
                var ids =await _pictureMappingManager.QuerysListAsync(m => m.Where(x => x.DestId == input.MappingId).OrderBy(x=>x.DisplayOrder).ThenBy(x=>x.Id),x=>new DestPictureMapping()
                {
                    FileId = x.FileId,
                    Id=x.Id,
                    IsDefault = x.IsDefault,
                    DisplayOrder = x.DisplayOrder
                }, input);
                var idlist = ids.Select(x => x.FileId);
                var list =await _fileItemManager.QuerysListAsync(m => m.Where(x => idlist.Contains(x.Id)).OrderBy(x=>x.Id),input);

                var maplist=list.MapTo<List<FileItemDto>>();
                var resultList= (from item in maplist
                    let destPicture = ids.FirstOrDefault(x => x.FileId == item.Id)
                    select new FileItemMappingDto()
                    {
                        FileItem = item, Id = destPicture.Id,
                        DisplayOrder = destPicture.DisplayOrder,
                        IsDefault = destPicture.IsDefault
                    }).ToList();

                return new PagedResultDto<FileItemMappingDto>(list.Count, resultList);
            }
            catch (Exception ex)
            {
                return new PagedResultDto<FileItemMappingDto>(0, new List<FileItemMappingDto>());
            }
        }
        #endregion
    }
}
