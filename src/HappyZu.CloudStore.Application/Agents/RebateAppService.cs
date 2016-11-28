using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using HappyZu.CloudStore.Agents.Dto;
using HappyZu.CloudStore.Common.Dto;
using Abp.AutoMapper;
using Abp.Linq.Extensions;
using HappyZu.CloudStore.Common;

namespace HappyZu.CloudStore.Agents
{
    public class RebateAppService:IRebateAppService
    {
        private readonly IRepository<Rebate> _rebateRepository;

        public RebateAppService(IRepository<Rebate> rebateRepository)
        {
            _rebateRepository = rebateRepository;
        }

        public async Task<RebateDto> GetByIdAsync(int id)
        {
            var entity = await _rebateRepository.GetAsync(id);
            return entity.MapTo<RebateDto>();
        }

        public async Task<RebateDto> GetByIdAsync(int id, long agentId)
        {
            var entity = await _rebateRepository.FirstOrDefaultAsync(x => x.AgentId == agentId && x.Id == id);
            return entity?.MapTo<RebateDto>();
        }

        public async Task<ResultOutputDto> CreateRecordAsync(CreateRecordInput input)
        {
            try
            {
                var entity = input.Rebate.MapTo<Rebate>();
                await _rebateRepository.InsertAsync(entity);
                return ResultOutputDto.Successed;
            }
            catch (Exception)
            {
                return ResultOutputDto.Failed;
            }
            
        }
        

        public Task<IPagedResult<RebateDto>> QueryListAsync(QueryRebatesInput input)
        {
            if (input.MaxResultCount <= 0)
            {
                input.MaxResultCount = int.MaxValue;
            }
            var predicates = new List<Expression<Func<Rebate, bool>>>();
            var orderBys = new List<QueryableOrderBy>();

            if (input.AgentId>0)
            {
                predicates.Add(x => x.AgentId == input.AgentId);
            }

            if (!string.IsNullOrWhiteSpace(input.UserName))
            {
                predicates.Add(x => x.UserName == input.UserName);
            }

            if (input.RebateStatus!=null)
            {
                predicates.Add(x => x.RebateStatus == input.RebateStatus);
            }

            orderBys.Add(new QueryableOrderBy {OrderColumn = "Id", OrderType = OrderType.OrderByDescending});

            var query = QueryPredicateHelper.GetOrderedQuery<Rebate>(orderBys.ToArray(),predicates.ToArray<Expression>());

            var list = _rebateRepository.Query(query).PageBy(input).ToList();

            var count = _rebateRepository.Query(query).Count();

            IPagedResult<RebateDto> result = new PagedResultDto<RebateDto>
            {
                TotalCount = count,
                Items = list.MapTo<IReadOnlyList<RebateDto>>()
            };
            return Task.FromResult(result);
        }

        public async Task<ResultOutputDto> UpdateStatus(int id, RebateStatus status)
        {
            try
            {
                await _rebateRepository.UpdateAsync(id, action =>
                {
                    action.RebateStatus = status;
                    return Task.FromResult(action);
                });
                return ResultOutputDto.Successed;
            }
            catch (Exception)
            {
                return ResultOutputDto.Failed;
            }
           
        }
    }
}
