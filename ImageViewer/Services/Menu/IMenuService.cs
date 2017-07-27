using System.Collections.Generic;
using ImageViewer.Views.Menu;
using ImageViewer.Views.Menu.MenuItems;

namespace ImageViewer.Services.Menu
{
    public interface IMenuService
    {
        void RegisterMenu(MenuViewModel menuViewModel);

        void RegisterMenuItems(IList<IMenuItem> items);

        void UnregisterMenuItems(IList<IMenuItem> items);

        void SetMode(MenuState state);
    }
}