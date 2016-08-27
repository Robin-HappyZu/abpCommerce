using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Services;

namespace HappyZu.CloudStore.FAQ
{
    public interface IFAQManager:IDomainService
    {
        Task<FAQDetail> GetByIdAsync(int id);

        Task CreateAsync(FAQDetail @detail);

        Task DeleteAsync(int id);

        Task<IReadOnlyList<FAQDetail>> GetAllByCategoryIdAsync(int categoryId);

        Task CreateCategoryAsync(FAQCategory @category);

        Task<FAQCategory> GetCategoryByIdAsync(int id);

        Task IsEnableCategoryAsync(int id);

        Task IsDisableCategoryAsync(int id);

        Task DeleteCategoryAsync(int id);

        Task<IReadOnlyList<FAQCategory>> GetAllCategorysAsync();
    }
}
