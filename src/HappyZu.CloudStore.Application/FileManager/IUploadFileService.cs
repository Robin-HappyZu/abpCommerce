using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.FileManager.Dto;

namespace HappyZu.CloudStore.FileManager
{
    public interface IUploadFileService : IApplicationService
    {
        Task<ResultOutputDto> AddFileItem(FileItemInput input);

        Task<IPagedResult<FileItemMappingDto>> GetPagedFileItemsAsync(GetPagedFileItemInput input);
    }
}
