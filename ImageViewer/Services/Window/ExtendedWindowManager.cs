using System;
using System.Collections.Generic;
using System.Windows;
using Caliburn.Micro;

namespace ImageViewer.Services.Window
{
    public class ExtendedWindowManager : IExtendedWindowManager
    {
        private readonly WindowManager _windowManager;

        public ExtendedWindowManager()
        {
            this._windowManager = new WindowManager();
        }

        public bool? ShowDialog(object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            return this._windowManager.ShowDialog(rootModel, context, settings);
        }

        public void ShowWindow(object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            this._windowManager.ShowWindow(rootModel, context, settings);
        }

        public void ShowPopup(object rootModel, object context = null, IDictionary<string, object> settings = null)
        {
            this._windowManager.ShowPopup(rootModel, context, settings);
        }

        public void SwitchWindowFullScreenMode()
        {
            var mainWindow = Application.Current.MainWindow;

            if (mainWindow == null)
                return;

            if (mainWindow.WindowStyle == WindowStyle.None && mainWindow.WindowState == WindowState.Maximized)
            {
                this.SetFullScreen(mainWindow);
            }
            else
            {
                this.SetNormalScreen(mainWindow);
            }
        }

        private void SetFullScreen(System.Windows.Window mainWindow)
        {
            mainWindow.WindowStyle = WindowStyle.SingleBorderWindow;
            mainWindow.WindowState = WindowState.Normal;
            mainWindow.ResizeMode = ResizeMode.CanResizeWithGrip;
        }

        private void SetNormalScreen(System.Windows.Window mainWindow)
        {
            mainWindow.WindowStyle = WindowStyle.None;
            mainWindow.WindowState = WindowState.Maximized;
            mainWindow.ResizeMode = ResizeMode.NoResize;
        }

        public WindowScreenState CurrentScreenState()
        {
            var mainWindow = Application.Current.MainWindow;

            if (mainWindow == null)
                throw new Exception();

            if (mainWindow.WindowStyle == WindowStyle.None && mainWindow.WindowState == WindowState.Maximized)
            {
                return WindowScreenState.Maximized;
            }

            return WindowScreenState.Normal;
        }
    }

    public enum WindowScreenState
    {
        Normal,
        Maximized
    }
}