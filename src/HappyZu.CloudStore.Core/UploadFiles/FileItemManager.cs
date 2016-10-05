using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Linq.Extensions;

namespace HappyZu.CloudStore.UploadFiles
{
    public class FileItemManager : IDomainService
    {
        private readonly IRepository<FileItem> _fileItemRepository;

        public FileItemManager(IRepository<FileItem> fileItemRepository)
        {
            _fileItemRepository = fileItemRepository;
        }

        public async Task AddAsync(FileItem entity)
        {
            await _fileItemRepository.InsertAsync(entity);
        }

        public async Task UpdateAsync(FileItem entity)
        {
            await _fileItemRepository.UpdateAsync(entity);
        }

        public async Task RemoveByIdAsync(int id)
        {
            await _fileItemRepository.DeleteAsync(id);
        }

        public async Task<FileItem> GetByIdAsync(int id)
        {
            return await _fileItemRepository.GetAsync(id);
        }

        public Task<IReadOnlyList<FileItem>> QuerysListAsync(Func<IQueryable<FileItem>, IQueryable<FileItem>> query, IPagedResultRequest request)
        {
            if (request.MaxResultCount <= 0)
            {
                request.MaxResultCount = int.MaxValue;
            }
            var list = query == null ?
                _fileItemRepository.GetAll().OrderBy(p => p.Id).PageBy(request).ToList() :
                _fileItemRepository.Query(query).PageBy(request).ToList();
            return Task.FromResult((IReadOnlyList<FileItem>)list);
        }

        public Task<IReadOnlyList<FileItem>> QuerysListAsync(IPagedResultRequest request)
        {
            return QuerysListAsync(null, request);
        }

        public Task<int> QueryCountAsync(Func<IQueryable<FileItem>, IQueryable<FileItem>> query)
        {
            var count = query != null ?
                    _fileItemRepository.Query(query).Count() :
                    _fileItemRepository.Count();

            return Task.FromResult(count);
        }

    }
}
