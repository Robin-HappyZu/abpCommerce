using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;

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

        public async Task<IList<DestCity>> GetDestCitiesByProvinceIdAsync(int provinceId)
        {
            return await _destCityRepository.GetAllListAsync(city => city.ProvinceId == provinceId);
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
