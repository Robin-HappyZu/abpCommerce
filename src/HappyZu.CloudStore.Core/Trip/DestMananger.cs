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
    public class DestMananger : IDomainService
    {
        private readonly IRepository<Dest> _destRepository;
        private readonly IRepository<DestAttributeRecord> _destAttributeRecordRepository; 

        public DestMananger(IRepository<Dest> destRepository, IRepository<DestAttributeRecord> destAttributeRecordRepository )
        {
            _destRepository = destRepository;
            _destAttributeRecordRepository = destAttributeRecordRepository;
        }

        public async Task<int> GetDestsCountAsync(int provinceId, int cityId)
        {        
            if (provinceId != 0 && cityId != 0)
            {
                return await _destRepository.CountAsync(dest => dest.ProvinceId == provinceId && dest.CityId == cityId);
            }

            if (provinceId != 0)
            {
                return await _destRepository.CountAsync(dest => dest.ProvinceId == provinceId);
            }

            if (cityId != 0)
            {
                return await _destRepository.CountAsync(dest => dest.CityId == cityId);
            }

            return await _destRepository.CountAsync();
        }

        public IReadOnlyList<Dest> GetDestsByLocation(int provinceId, int cityId, IPagedResultRequest request)
        {
            var dests = _destRepository.GetAll();
            if (provinceId != 0)
            {
                dests = dests.Where( dest => dest.ProvinceId == provinceId);
            }

            if (cityId != 0)
            {
                dests = dests.Where(dest => dest.CityId == cityId);
            }

            return dests.PageBy(request).ToList();
        }

        public Task<IReadOnlyList<Dest>> QuerysListAsync(Func<IQueryable<Dest>, IQueryable<Dest>> query,IPagedResultRequest request)
        {
            if (request.MaxResultCount<=0)
            {
                request.MaxResultCount = int.MaxValue;
            }

            var list= query==null ? 
                _destRepository.GetAll().OrderBy(p=>p.Id).PageBy(request).ToList() :
                _destRepository.Query(query).PageBy(request).ToList();
            return Task.FromResult((IReadOnlyList<Dest>)list);
        }

        public Task<IReadOnlyList<Dest>> QuerysListAsync(IPagedResultRequest request)
        {
            return QuerysListAsync(null, request);
        }

        public Task<int> QueryCountAsync(Func<IQueryable<Dest>, IQueryable<Dest>> query)
        {
            var count = query!=null ?
                    _destRepository.Query(query).Count() :
                    _destRepository.Count();

            return Task.FromResult(count);
        }

        public Task<int> QueryCountAsync()
        {
            return QueryCountAsync(null);
        }

        public async Task<int> AddDestAsync(Dest dest)
        {
            return await _destRepository.InsertAndGetIdAsync(dest);
        }

        public async Task<Dest> GetDestByIdAsync(int destId)
        {
           return await _destRepository.GetAsync(destId);
        }

        public async Task UpdateDestAsync(Dest dest)
        {
            await _destRepository.UpdateAsync(dest);
        }

        public async Task RemoveDestAsync(int destId)
        {
            await _destRepository.DeleteAsync(destId);
        }

        public async Task AttachDestAttributeRecordAsync(List<DestAttributeRecord> records)
        {
            foreach (var record in records)
            {
                await _destAttributeRecordRepository.InsertAsync(record);
            }
        }

        public async Task DetachDestAttributeRecordAsync(List<DestAttributeRecord> records)
        {
            foreach (var record in records)
            {
                await _destAttributeRecordRepository.DeleteAsync(record.Id);
            }
        }

        public async Task<List<DestAttributeRecord>> GetAttributeRecordsByDestIdAsync(int destId)
        {
            return await _destAttributeRecordRepository.GetAllListAsync(record => record.DestId == destId);
        }
    }
}
