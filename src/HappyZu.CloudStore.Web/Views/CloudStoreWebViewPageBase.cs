using Abp.Web.Mvc.Views;

namespace HappyZu.CloudStore.Web.Views
{
    public abstract class CloudStoreWebViewPageBase : CloudStoreWebViewPageBase<dynamic>
    {

    }

    public abstract class CloudStoreWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected CloudStoreWebViewPageBase()
        {
            LocalizationSourceName = CloudStoreConsts.LocalizationSourceName;
        }
    }
}