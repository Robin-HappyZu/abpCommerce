using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.FAQ.Dto;

namespace HappyZu.CloudStore.FAQ
{
    public interface IFAQAppService:IApplicationService
    {
        #region 帮助详情

        Task<ListResultOutput<FAQDetailDto>> GetDetailListAsync(GetDetailListInput input);

        Task<FAQDetailDto> GetDetailByIdAsync(EntityRequestInput input);

        Task<ResultOutputDto> CreateAsync(CreateDetailInput input);

        Task<ResultOutputDto> DeleteAsync(EntityRequestInput input);

        #endregion

        #region 帮助分类

        Task<ResultOutputDto> CreateCategoryAsync(CreateFAQCategoryInput input);

        Task<FAQCategoryDto> GetCategoryByIdAsync(EntityRequestInput input);

        Task<ResultOutputDto> IsEnableCategoryAsync(EntityRequestInput input);

        Task<ResultOutputDto> IsDisableCategoryAsync(EntityRequestInput input);

        Task<ResultOutputDto> DeleteCategoryAsync(EntityRequestInput input);

        Task<ListResultOutput<FAQCategoryListDto>> GetAllCategorysAsync();
        #endregion
    }
}
