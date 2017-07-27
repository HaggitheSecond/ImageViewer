using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using DevExpress.Mvvm;
using ImageViewer.Extensions;
using ImageViewer.Services.Menu;
using ImageViewer.Views.Image;
using ImageViewer.Views.Menu;
using ImageViewer.Views.Menu.MenuItems;
using ImageViewer.Views.SlideshowEditor;
using Microsoft.Win32;

namespace ImageViewer.Views
{
    public class MainViewModel : Conductor<ScreenWithMenuItems>.Collection.OneActive
    {
        private MenuViewModel _menuViewModel;

        public MenuViewModel MenuViewModel
        {
            get { return this._menuViewModel; }
            set { this.SetProperty(ref this._menuViewModel, value); }
        }

        public MainViewModel(IMenuService menuService)
        {
            this.MenuViewModel = IoC.Get<MenuViewModel>();
            this.ActivateWith(this);

            this.DisplayName = "Image Viewer";

            menuService.RegisterMenuItems(new List<IMenuItem>
            {
                new MenuItem()
                .SetCommand(new DelegateCommand(this.OpenImage))
                .SetIcon(new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/Picture-64.png")))
                .SetText("Open"),

                new MenuItem()
                .SetCommand(new DelegateCommand(this.OpenDiashowEditor))
                .SetIcon(new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/Open Folder-64.png")))
                .SetText("Diashow")
            });
        }

        public void RouteKeyDownEvent(KeyEventArgs args)
        {
            this.ActiveItem?.ReceiveKeyDownEvent(args);
        }

        private void OpenDiashowEditor()
        {
            var viewModel = IoC.Get<SlideshowEditorViewModel>();
            
            this.Items.Add(viewModel);
            this.ActivateItem(viewModel);
        }

        private void OpenImage()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png, *.gif) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png; *.gif;"
            };

            var result = openFileDialog.ShowDialog();

            if (result.HasValue && result.Value && Path.GetExtension(openFileDialog.FileName).IsValidImageExtension())
            {
                var viewModel = IoC.Get<ImageViewerViewModel>();
                viewModel.SetImageFilePath(openFileDialog.FileName);

                this.Items.Add(viewModel);
                this.ActivateItem(viewModel);
            }

        }
    }
}