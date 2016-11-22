using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using HappyZu.CloudStore.Agents.Dto;
using HappyZu.CloudStore.Common.Dto;

namespace HappyZu.CloudStore.Agents
{
    //[RemoteService(false)]
    public interface IRebateAppService:IApplicationService
    {
        Task<RebateDto> GetByIdAsync(int id);

        Task<RebateDto> GetByIdAsync(int id, long agentId);

        Task<ResultOutputDto> CreateRecordAsync(CreateRecordInput input);

        Task<IPagedResult<RebateDto>> QueryListAsync(QueryRebatesInput input);

        Task<ResultOutputDto> UpdateStatus(int id, RebateStatus status);
    }
}
