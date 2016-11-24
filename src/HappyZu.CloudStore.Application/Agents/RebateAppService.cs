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

        

        //private static Func<IQueryable<Rebate>, IOrderedQueryable<Rebate>> GetIQueryble(string orderColumn, string orderType)
        //{
        //    Type typeQueryable = typeof(IQueryable<Rebate>);
        //    ParameterExpression argQueryable = Expression.Parameter(typeQueryable, "p");
        //    var outerExpression = Expression.Lambda(argQueryable, argQueryable);
        //    string[] props = orderColumn.Split('.');
        //    IQueryable<Rebate> query = new List<Rebate>().AsQueryable<Rebate>();
        //    Type type = typeof(Rebate);
        //    ParameterExpression arg = Expression.Parameter(type, "x");

        //    Expression expr = arg;
        //    foreach (string prop in props)
        //    {
        //        PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        //        expr = Expression.Property(expr, pi);
        //        type = pi.PropertyType;
        //    }
        //    LambdaExpression lambda = Expression.Lambda(expr, arg);
        //    string methodName = orderType == "asc" ? "OrderBy" : "OrderByDescending";

        //    MethodCallExpression resultExp =
        //        Expression.Call(typeof(Queryable), methodName, new Type[] { typeof(Rebate), type }, outerExpression.Body, Expression.Quote(lambda));
            
        //    var finalLambda = Expression.Lambda(resultExp, argQueryable);
        //    return (Func<IQueryable<Rebate>, IOrderedQueryable<Rebate>>)finalLambda.Compile();
        //}

        public Task<IPagedResult<RebateDto>> QueryListAsync(QueryRebatesInput input)
        {
            if (input.MaxResultCount <= 0)
            {
                input.MaxResultCount = int.MaxValue;
            }

            Func<IQueryable<Rebate>, IQueryable<Rebate>> query = q => q.Where(x => x.AgentId == input.AgentId).OrderByDescending(x=>x.Id);


            query += q => q.Where(x => x.UserName == input.UserName);
            

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
