using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Abp.IdentityFramework;
using Abp.UI;
using Abp.Web.Mvc.Controllers;
using Microsoft.AspNet.Identity;

namespace HappyZu.CloudStore.Web.Areas.Admin.Controllers
{
    public class AdminControllerBase : AbpController
    {
        protected AdminControllerBase()
        {
            LocalizationSourceName = CloudStoreConsts.LocalizationSourceName;
        }

        protected virtual void CheckModelState()
        {
            if (!ModelState.IsValid)
            {
                throw new UserFriendlyException(L("FormIsNotValidMessage"));
            }
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}