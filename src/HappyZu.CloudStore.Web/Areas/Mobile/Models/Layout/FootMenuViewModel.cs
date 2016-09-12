using Abp.Application.Navigation;

namespace HappyZu.CloudStore.Web.Areas.Mobile.Models.Layout
{
    public class FootMenuViewModel
    {
        public UserMenu MainMenu { get; set; }

        public string ActiveMenuItemName { get; set; }

        public int ShoppingCartItemCount { get; set; }
    }
}