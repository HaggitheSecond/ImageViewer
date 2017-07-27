using System;
using Caliburn.Micro;
using ImageViewer.Extensions;
using ImageViewer.Services.Menu;
using ImageViewer.Views.DirectoryControl;

namespace ImageViewer.Views.SlideshowEditor
{
    public class SlideshowEditorViewModel : ScreenWithMenuItems
    {
        private DirectoryViewModel _directoryViewModel;

        public DirectoryViewModel DirectoryViewModel
        {
            get { return this._directoryViewModel; }
            set { this.SetProperty(ref this._directoryViewModel, value); }
        }

        public SlideshowEditorViewModel(IMenuService menuService) 
            : base(menuService)
        {
            this.DirectoryViewModel = IoC.Get<DirectoryViewModel>();

            this.DirectoryViewModel.LoadDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));

            this.DisplayName = "New slideshow";
            
        }
    }
}