using System.Collections.Generic;
using System.Linq;
using ImageViewer.Views.Menu;
using ImageViewer.Views.Menu.MenuItems;

namespace ImageViewer.Services.Menu
{
    public class MenuService : IMenuService
    {
        private MenuViewModel _menuViewModel;

        public void RegisterMenu(MenuViewModel menuViewModel)
        {
            this._menuViewModel = menuViewModel;
        }

        public void RegisterMenuItems(IList<IMenuItem> items)
        {
            if (items == null || items.Count == 0)
                return;

            if (this._menuViewModel.Buttons.Count > 0)
                this._menuViewModel.Buttons.Add(new MenuSeperator());

            this._menuViewModel.Buttons.AddRange(items);
        }

        public void UnregisterMenuItems(IList<IMenuItem> items)
        {
            if(items == null || items.Count == 0)
                return;

            var indexOfFirstItem = this._menuViewModel.Buttons.IndexOf(items.First());

            // remove seperator
            if (indexOfFirstItem > 0)
                this._menuViewModel.Buttons.RemoveAt(indexOfFirstItem - 1);

            this._menuViewModel.Buttons.RemoveRange(items);
        }

        public void SetMode(MenuState state)
        {
            this._menuViewModel.State = state;
        }
    }
}