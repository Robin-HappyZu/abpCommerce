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

        Task<ListResultDto<FAQDetailDto>> GetDetailListAsync(GetDetailListInput input);

        Task<FAQDetailDto> GetDetailByIdAsync(EntityDto input);

        Task<ResultOutputDto> CreateAsync(CreateDetailInput input);

        Task<ResultOutputDto> DeleteAsync(EntityDto input);

        #endregion

        #region 帮助分类

        Task<ResultOutputDto> CreateCategoryAsync(CreateFAQCategoryInput input);

        Task<FAQCategoryDto> GetCategoryByIdAsync(EntityDto input);

        Task<ResultOutputDto> IsEnableCategoryAsync(EntityDto input);

        Task<ResultOutputDto> IsDisableCategoryAsync(EntityDto input);

        Task<ResultOutputDto> DeleteCategoryAsync(EntityDto input);

        Task<ListResultDto<FAQCategoryListDto>> GetAllCategorysAsync();
        #endregion
    }
}
