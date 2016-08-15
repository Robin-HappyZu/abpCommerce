using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using HappyZu.CloudStore.MultiTenancy;

namespace HappyZu.CloudStore.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}