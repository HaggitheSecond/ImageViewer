using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using ImageViewer.Extensions;
using ImageViewer.Services.Menu;
using ImageViewer.Services.Window;
using MenuItem = ImageViewer.Views.Menu.MenuItems.MenuItem;

namespace ImageViewer.Views.Image
{
    public class ImageViewerViewModel : ScreenWithMenuItems
    {
        private readonly IExtendedWindowManager _windowManager;

        private MenuItem _fullScreenMenuItem;

        private string _imageSource;

        private int _currentAngle;


        public string ImageSource
        {
            get { return this._imageSource; }
            set { this.SetProperty(ref this._imageSource, value); }
        }

        public int CurrentAngle
        {
            get { return this._currentAngle; }
            set { this.SetProperty(ref this._currentAngle, value); }
        }

        public DelegateCommand MoveForwardCommand { get; }

        public DelegateCommand MoveBackwardCommand { get; }

        public DelegateCommand RotateImageCommand { get; }

        public DelegateCommand ChangeFullscreenModeCommand { get; }

        public ImageViewerViewModel(IMenuService menuService, IWindowManager windowManager)
            : base(menuService)
        {
            this._windowManager = windowManager as IExtendedWindowManager;

            this.MoveForwardCommand = new DelegateCommand(this.MoveForward);
            this.MoveBackwardCommand = new DelegateCommand(this.MoveBackward);

            this.RotateImageCommand = new DelegateCommand(this.Rotate);

            this.ChangeFullscreenModeCommand = new DelegateCommand(this.ChangeFullscreenMode);

            this._fullScreenMenuItem = new MenuItem()
            .SetCommand(this.ChangeFullscreenModeCommand)
            .SetIcon(new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/Resize Four Directions-64.png")))
            .SetText("Fullscreen");

            base.Register(new MenuItem()
            .SetCommand(this.RotateImageCommand)
            .SetIcon(new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/Rotate-64.png")))
            .SetText("Rotate"),
            new MenuItem()
            .SetCommand(this.MoveForwardCommand)
            .SetIcon(new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/Right-64.png")))
            .SetText("Next"),
            new MenuItem()
            .SetCommand(this.MoveBackwardCommand)
            .SetIcon(new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/Left-64.png")))
            .SetText("Back"),
            this._fullScreenMenuItem);

            this.CurrentAngle = 0;
        }

        private void ChangeFullscreenMode()
        {
           this._windowManager.SwitchWindowFullScreenMode();

            if (this._windowManager.CurrentScreenState() == WindowScreenState.Maximized)
            {
                this._fullScreenMenuItem.Icon = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/Collapse-64.png"));
                this._fullScreenMenuItem.Text = "Exit fullscreen";
            }
            else
            {
                this._fullScreenMenuItem.Icon = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/Resize Four Directions-64.png"));
                this._fullScreenMenuItem.Text = "Enter fullscreen";
            }
        }

        public override void ReceiveKeyDownEvent(KeyEventArgs args)
        {
            if (args.Key == Key.Left || args.Key == Key.A)
            {
                this.MoveBackwardCommand.Execute(null);
            }
            if (args.Key == Key.Right || args.Key == Key.A)
            {
                this.MoveForwardCommand.Execute(null);
            }
            if (args.Key == Key.F11)
            {
                this.ChangeFullscreenModeCommand.Execute(null);
            }
            if (args.Key == Key.Escape)
            {
                if(this._windowManager.CurrentScreenState() == WindowScreenState.Maximized)
                    this.ChangeFullscreenModeCommand.Execute(null);
            }
        }

        private void MoveBackward()
        {
            var files = this.GetFilesInDirectory();

            var currentFileIndex = files.IndexOf(this.ImageSource);

            if(currentFileIndex == 0)
                return;

            var newFile = files[currentFileIndex - 1];

            this.SetImageFilePath(newFile);
        }

        private void MoveForward()
        {
            var files = this.GetFilesInDirectory();

            var currentFileIndex = files.IndexOf(this.ImageSource);

            if (currentFileIndex == files.Count-1)
                return;

            var newFile = files[currentFileIndex + 1];

            this.SetImageFilePath(newFile);
        }

        private IList<string> GetFilesInDirectory()
        {
            var filePath = Path.GetDirectoryName(this.ImageSource);

            if(filePath == null)
                return new List<string>();

            var files = Directory.GetFiles(filePath);
            return files.Where(f => Path.GetExtension(f).IsValidImageExtension()).ToList();
        }

        private void Rotate()
        {
            this.CurrentAngle += 90;
        }

        public void SetImageFilePath(string filePath)
        {
            this.ImageSource = filePath;
            this.DisplayName = Path.GetFileName(filePath);
        }
    }
}