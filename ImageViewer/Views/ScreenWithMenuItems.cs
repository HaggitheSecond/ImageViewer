using System.Collections.Generic;
using System.Windows.Input;
using Caliburn.Micro;
using DevExpress.Mvvm;
using ImageViewer.Extensions;
using ImageViewer.Services.Menu;
using ImageViewer.Views.Menu;
using ImageViewer.Views.Menu.MenuItems;

namespace ImageViewer.Views
{
    public abstract class ScreenWithMenuItems : Screen
    {
        private readonly IMenuService _menuService;
        private IList<IMenuItem> _menuItems;

        public DelegateCommand CloseTabCommand { get; }

        protected ScreenWithMenuItems(IMenuService menuService)
        {
            this._menuService = menuService;
            this._menuItems = new List<IMenuItem>();

            this.CloseTabCommand = new DelegateCommand(this.CloseTab);
        }

        public virtual void CloseTab()
        {
            this.TryClose(true);
        }

        public virtual void ReceiveKeyDownEvent(KeyEventArgs args)
        {
            
        }

        public void Register( params MenuItem[] items)
        {
            foreach (var currentMenuItem in items)
            {
                this._menuItems.Add(currentMenuItem);
            }
        }

        protected override void OnActivate()
        {
            this._menuService.RegisterMenuItems(this._menuItems);

            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            this._menuService.UnregisterMenuItems(this._menuItems);
            
            base.OnDeactivate(close);
        }
    }
}