using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Linq.Extensions;
using Abp.UI;

namespace HappyZu.CloudStore.FAQ
{
    public class FAQManager : IDomainService
    {
        private readonly IRepository<FAQCategory> _repositoryCategory;
        private readonly IRepository<FAQDetail> _repositoryDetail;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public FAQManager(IRepository<FAQCategory> repositoryCategory, IRepository<FAQDetail> repositoryDetail, IUnitOfWorkManager unitOfWorkManager)
        {
            _repositoryCategory = repositoryCategory;
            _repositoryDetail = repositoryDetail;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<FAQDetail> GetByIdAsync(int id)
        {
            var detail = await _repositoryDetail.GetAsync(id);
            if (detail==null||detail.IsDeleted)
            {
                throw new UserFriendlyException("Could not found the detail, maybe it's deleted!");
            }
            return detail;
        }

        public async Task CreateAsync(FAQDetail @detail)
        {
            await _repositoryDetail.InsertAsync(@detail);
        }

        public async Task DeleteAsync(int id)
        {
            var detail =await GetByIdAsync(id);
            detail.Delete();
        }

        public async Task<IReadOnlyList<FAQDetail>> GetAllByCategoryIdAsync(int categoryId)
        {

            var category =await  _repositoryCategory.GetAsync(categoryId);
            if (category == null || category.IsDeleted)
            {
                throw new UserFriendlyException("Could not found the category, maybe it's deleted!");
            }
            return await _repositoryDetail.GetAllListAsync(detail => !detail.IsDeleted);
        }

        public async Task CreateCategoryAsync(FAQCategory @category)
        {
            await  _repositoryCategory.InsertAsync(@category);
        }

        public async Task<FAQCategory> GetCategoryByIdAsync(int id)
        {

            var category = await _repositoryCategory.GetAsync(id);
            if (category == null || category.IsDeleted)
            {
                throw new UserFriendlyException("Could not found the category, maybe it's deleted!");
            }
            return category;
        }

        public async Task IsEnableCategoryAsync(int id)
        {
            var category =await GetCategoryByIdAsync(id);
            category.Enable();
        }

        public async Task IsDisableCategoryAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);
            category.Disable();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);
            category.Delete();
        }

        public async Task<IReadOnlyList<FAQCategory>> GetAllCategorysAsync()
        {
            return await _repositoryCategory.GetAllListAsync(category => category.IsEnable && !category.IsDeleted);
        }


        public Task<IReadOnlyList<FAQDetail>> QuerysListAsync(Func<IQueryable<FAQDetail>, IQueryable<FAQDetail>> query, IPagedResultRequest request)
        {
            if (request.MaxResultCount <= 0)
            {
                request.MaxResultCount = int.MaxValue;
            }
            var list = query == null ?
                _repositoryDetail.GetAll().OrderBy(p => p.Id).PageBy(request).ToList() :
                _repositoryDetail.Query(query).PageBy(request).ToList();
            return Task.FromResult((IReadOnlyList<FAQDetail>)list);
        }

        public Task<IReadOnlyList<FAQDetail>> QuerysListAsync(IPagedResultRequest request)
        {
            return QuerysListAsync(null, request);
        }

        public Task<int> QueryCountAsync(Func<IQueryable<FAQDetail>, IQueryable<FAQDetail>> query)
        {
            var count = query != null ?
                    _repositoryDetail.Query(query).Count() :
                    _repositoryDetail.Count();

            return Task.FromResult(count);
        }
    }
}
