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
        private readonly IFAQManager _faqManager;
        public FAQAppService(IFAQManager faqManager)
        {
            _faqManager = faqManager;
        }

        public async Task<ListResultDto<FAQDetailDto>> GetDetailListAsync(GetDetailListInput input)
        {
            var list=await _faqManager.GetAllByCategoryIdAsync(input.CategoryId);

            return new ListResultDto<FAQDetailDto>(list.MapTo<List<FAQDetailDto>>());
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
