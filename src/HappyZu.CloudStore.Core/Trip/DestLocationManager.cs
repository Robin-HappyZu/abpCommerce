using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Linq.Extensions;

namespace HappyZu.CloudStore.Trip
{
    public class DestLocationManager : IDomainService
    {
        private readonly IRepository<DestProvince> _destProviceRepository;
        private readonly IRepository<DestCity> _destCityRepository;

        public DestLocationManager(IRepository<DestProvince> destProviceRepository,
            IRepository<DestCity> destCityRepository)
        {
            _destProviceRepository = destProviceRepository;
            _destCityRepository = destCityRepository;
        }

        public async Task<IList<DestProvince>> GetAllDestProvicesAsync(CountryType countryType)
        {
            return await _destProviceRepository.GetAllListAsync(province => province.DestType == countryType);
        }

        public Task<IList<DestProvince>> GetDestProvicesAsync(CountryType countryType,IPagedResultRequest page)
        {
            var query =  _destProviceRepository.GetAll().Where(province => province.DestType == countryType).OrderBy(province=>province.CreationTime);
            if (page.MaxResultCount>0)
            {
                query.PageBy(page);
            }
            var list=query.ToList();
            return Task.FromResult((IList<DestProvince>)list);
        }

        public Task<IList<DestCity>> GetDestCitiesByProvinceIdAsync(int provinceId,IPagedResultRequest page)
        {

            var query = _destCityRepository.GetAll().Where(city => city.ProvinceId == provinceId).OrderBy(city => city.CreationTime);
            if (page.MaxResultCount > 0)
            {
                query.PageBy(page);
            }
            var list = query.ToList();
            return Task.FromResult((IList<DestCity>)list);
        }

        public async Task InsertDestProvinceAsync(DestProvince province)
        {
            await _destProviceRepository.InsertAsync(province);
        }

        public async Task<DestProvince> GetDestProvinceByIdAsync(int provinceId)
        {
            return await _destProviceRepository.GetAsync(provinceId);
        }

        public async Task RemoveDestProvinceAsync(int provinceId)
        {
            await _destProviceRepository.DeleteAsync(provinceId);
        }

        public async Task UpdateDestProvinceAsync(DestProvince provice)
        {
            await _destProviceRepository.UpdateAsync(provice);
        }

        public async Task InsertDestCityAsync(DestCity city)
        {
            await _destCityRepository.InsertAsync(city);
        }

        public async Task<DestCity> GetDestCityByIdAsync(int cityId)
        {
            return await _destCityRepository.GetAsync(cityId);
        }

        public Task<DestCity> GetDefaultCity()
        {
            var city = _destCityRepository.GetAll().OrderBy(x=>x.Id).FirstOrDefault();

            return Task.FromResult(city);
        }

        public async Task RemoveDestCityAsync(int cityId)
        {
            await _destCityRepository.DeleteAsync(cityId);
        }

        public async Task UpdateDestCityAsync(DestCity city)
        {
            await _destCityRepository.UpdateAsync(city);
        }
    }
}
