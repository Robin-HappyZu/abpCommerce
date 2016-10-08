using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HappyZu.CloudStore.Common.Dto;
using HappyZu.CloudStore.FileManager.Dto;
using HappyZu.CloudStore.Trip;

namespace HappyZu.CloudStore.FileManager
{
    public class UploadFileService : IUploadFileService
    {
        private readonly FileItemManager _fileItemManager;
        public UploadFileService(FileItemManager fileItemManager)
        {
            _fileItemManager = fileItemManager;
        }

        public async Task<ResultOutputDto> AddFileItem(FileItemInput input)
        {
            try
            {
                var entity = input.FileItem.MapTo<FileItem>();
                var id= await _fileItemManager.AddAsync(entity);
                return ResultOutputDto.Success(id);
            }
            catch (Exception ex)
            {
                return ResultOutputDto.Exception(ex);
            }
        }

    }
}
