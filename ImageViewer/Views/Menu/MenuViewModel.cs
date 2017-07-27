using System.Collections.Generic;
using System.Net.Mime;
using System.Windows;
using Caliburn.Micro;
using ImageViewer.Extensions;
using ImageViewer.Services.Menu;
using ImageViewer.Views.Menu.MenuItems;

namespace ImageViewer.Views.Menu
{
    public class MenuViewModel : Screen
    {
        private BindableCollection<IMenuItem> _buttons;

        private MenuState _state;

        public BindableCollection<IMenuItem> Buttons
        {
            get { return this._buttons; }
            set { this.SetProperty(ref this._buttons, value); }
        }

        public MenuState State
        {
            get { return this._state; }
            set { this.SetProperty(ref this._state, value); }
        }

        public MenuViewModel(IMenuService menuService)
        {
            menuService.RegisterMenu(this);

            this.Buttons = new BindableCollection<IMenuItem>();
            this.State = MenuState.Wide;
        }
    }
}