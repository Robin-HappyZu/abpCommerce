using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace HappyZu.CloudStore.Users.Dto
{
    public class QueryUserInput: IPagedResultRequest
    {
        public int MaxResultCount { get; set; }

        public int SkipCount { get; set; }

        public string UserName { get; set; }

        public string NickName { get; set; }
    }
}
