using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.UI;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.FAQ.Dto;

namespace HappyZu.CloudStore.FAQ
{
    public class FAQAppService:CloudStoreAppServiceBase,IFAQAppService
    {
        private readonly FAQManager _faqManager;
        public FAQAppService(FAQManager faqManager)
        {
            _faqManager = faqManager;
        }

        public async Task<IPagedResult<FAQDetailDto>> GetDetailListAsync(GetDetailListInput input)
        {
            Func<IQueryable<FAQDetail>, IQueryable<FAQDetail>> query =
                q => q.Where(x => x.CategoryId == input.CategoryId).OrderBy(x=>x.Sort).ThenBy(x=>x.Id);
            if (input.CategoryId==0)
            {
                query = null;
            }
            var list=await _faqManager.QuerysListAsync(query,input);
            var count = await _faqManager.QueryCountAsync(query);
            var result=new PagedResultDto<FAQDetailDto>()
            {
                TotalCount = count,
                Items = list.MapTo<List<FAQDetailDto>>()
            };

            return result;
        }

        public async Task<FAQDetailDto> GetDetailByIdAsync(EntityDto input)
        {
            var @detail =await _faqManager.GetByIdAsync(input.Id);
            if (@detail == null)
            {
                throw new UserFriendlyException("Could not found the detail, maybe it's deleted.");
            }
            return @detail.MapTo<FAQDetailDto>();
        }

        public async Task<ResultOutputDto> CreateAsync(CreateDetailInput input)
        {
            var @detail = FAQDetail.Create(input.Title,input.CategoryId,input.Discription,input.Sort,input.IsDelete);
            await _faqManager.CreateAsync(@detail);

            return ResultOutputDto.Successed;
        }

        public async Task<ResultOutputDto> DeleteAsync(EntityDto input)
        {
            await _faqManager.DeleteAsync(input.Id);

            return ResultOutputDto.Successed;
        }

        public async Task<ResultOutputDto> CreateCategoryAsync(CreateFAQCategoryInput input)
        {
            var @category = FAQCategory.Create(input.Name,input.Icon,input.FontIcon,input.Sort,input.IsEnable,input.IsDelete);
            await _faqManager.CreateCategoryAsync(@category);

            return ResultOutputDto.Successed;
        }

        public async Task<FAQCategoryDto> GetCategoryByIdAsync(EntityDto input)
        {
            var @category = await _faqManager.GetCategoryByIdAsync(input.Id);
            if (@category == null)
            {
                throw new UserFriendlyException("Could not found the detail, maybe it's deleted.");
            }
            return @category.MapTo<FAQCategoryDto>();
        }

        public async Task<ResultOutputDto> IsEnableCategoryAsync(EntityDto input)
        {
            await _faqManager.IsEnableCategoryAsync(input.Id);

            return ResultOutputDto.Successed;

        }

        public async Task<ResultOutputDto> IsDisableCategoryAsync(EntityDto input)
        {
            await _faqManager.IsDisableCategoryAsync(input.Id);

            return ResultOutputDto.Successed;
        }

        public async Task<ResultOutputDto> DeleteCategoryAsync(EntityDto input)
        {
            await _faqManager.DeleteCategoryAsync(input.Id);

            return ResultOutputDto.Successed;
        }

        public async Task<ListResultDto<FAQCategoryListDto>> GetAllCategorysAsync()
        {
            var list = await _faqManager.GetAllCategorysAsync();

            return new ListResultDto<FAQCategoryListDto>(list.MapTo<List<FAQCategoryListDto>>());

        }
    }
}
