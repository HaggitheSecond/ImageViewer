using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Caliburn.Micro;
using ImageViewer.Common;
using ImageViewer.Services.Menu;
using ImageViewer.Services.Window;
using ImageViewer.Views;
using ImageViewer.Views.DirectoryControl;
using ImageViewer.Views.Image;
using ImageViewer.Views.Menu;
using ImageViewer.Views.SlideshowEditor;

namespace ImageViewer
{
    public class Bootstrapper : BootstrapperBase
    {
        #region Constructor and IoC-Intialization

        private SimpleContainer _container;

        public Bootstrapper()
        {
            this.Initialize();
        }

        protected override void Configure()
        {
            this._container = new SimpleContainer();
            this._container.Instance(this._container);

            this.RegisterServices();
        }

        private void RegisterServices()
        {
            this._container
                .Singleton<IWindowManager, ExtendedWindowManager>()
                .Singleton<IMenuService, MenuService>();

            this._container
                .PerRequest<MainViewModel>()
                .PerRequest<ImageViewerViewModel>()
                .PerRequest<MenuViewModel>()
                .PerRequest<SlideshowEditorViewModel>()
                .PerRequest<DirectoryViewModel>();
        }

        #endregion

        #region Startup

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            this.DisplayRootViewFor<MainViewModel>(WindowSettings.With
                .SizeToContent(SizeToContent.Manual)
                .Size(1200, 800)
                .StartupLocation(WindowStartupLocation.CenterScreen)
                .WindowState(WindowState.Normal)
                .ResizeMode(ResizeMode.CanResizeWithGrip));
        }

        #endregion

        #region IoC

        protected override object GetInstance(Type service, string key)
        {
            return this._container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this._container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            this._container.BuildUp(instance);
        }

        #endregion

        protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            MessageBox.Show(e.Exception.Message, "An error as occurred", MessageBoxButton.OK);
        }
    }
}