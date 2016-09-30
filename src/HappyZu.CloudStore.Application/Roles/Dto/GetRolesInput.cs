using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;

namespace HappyZu.CloudStore.Roles.Dto
{
    public class GetRolesInput : IPagedResultRequest, ISortedResultRequest, ICustomValidate
    {
        public int MaxResultCount { get; set; }
        public int SkipCount { get; set; }
        public string Sorting { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
