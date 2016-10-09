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
    public class DestPictureMappingManager : IDomainService
    {
        private readonly IRepository<DestPictureMapping> _pictrueMappingRepository;

        public DestPictureMappingManager(IRepository<DestPictureMapping> fileItemRepository)
        {
            _pictrueMappingRepository = fileItemRepository;
        }

        public async Task AddAsync(DestPictureMapping entity)
        {
            await _pictrueMappingRepository.InsertAsync(entity);
        }

        public async Task<int> AddAndGetIdAsync(DestPictureMapping entity)
        {
            return await _pictrueMappingRepository.InsertAndGetIdAsync(entity);
        }

        public async Task UpdateAsync(DestPictureMapping entity)
        {
            await _pictrueMappingRepository.UpdateAsync(entity);
        }

        public async Task RemoveByIdAsync(int id)
        {
            await _pictrueMappingRepository.DeleteAsync(id);
        }

        public async Task<DestPictureMapping> GetByIdAsync(int id)
        {
            return await _pictrueMappingRepository.GetAsync(id);
        }

        public Task<IReadOnlyList<DestPictureMapping>> QuerysListAsync(Func<IQueryable<DestPictureMapping>, IQueryable<DestPictureMapping>> query, IPagedResultRequest request)
        {
            if (request.MaxResultCount <= 0)
            {
                request.MaxResultCount = int.MaxValue;
            }
            var list = query == null ?
                _pictrueMappingRepository.GetAll().OrderBy(p => p.Id).PageBy(request).ToList() :
                _pictrueMappingRepository.Query(query).PageBy(request).ToList();
            return Task.FromResult((IReadOnlyList<DestPictureMapping>)list);
        }

        public Task<IReadOnlyList<DestPictureMapping>> QuerysListAsync(Func<IQueryable<DestPictureMapping>, IQueryable<DestPictureMapping>> query, Func<DestPictureMapping, DestPictureMapping> select, IPagedResultRequest request)
        {
            if (request.MaxResultCount <= 0)
            {
                request.MaxResultCount = int.MaxValue;
            }
            var list = query == null ?
                _pictrueMappingRepository.GetAll().OrderBy(p => p.Id).PageBy(request).ToList() :
                _pictrueMappingRepository.Query(query).PageBy(request).Select(select).ToList();
            return Task.FromResult((IReadOnlyList<DestPictureMapping>)list);
        }

        public Task<IReadOnlyList<DestPictureMapping>> QuerysListAsync(IPagedResultRequest request)
        {
            return QuerysListAsync(null, request);
        }

        public Task<int> QueryCountAsync(Func<IQueryable<DestPictureMapping>, IQueryable<DestPictureMapping>> query)
        {
            var count = query != null ?
                    _pictrueMappingRepository.Query(query).Count() :
                    _pictrueMappingRepository.Count();

            return Task.FromResult(count);
        }
    }
}
