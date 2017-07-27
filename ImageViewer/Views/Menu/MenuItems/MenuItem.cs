using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using DevExpress.Mvvm;
using ImageViewer.Extensions;

namespace ImageViewer.Views.Menu.MenuItems
{
    public class MenuItem : PropertyChangedBase, IMenuItem
    {
        private ImageSource _icon;
        private string _text;

        private bool _isEnabled;

        public ImageSource Icon
        {
            get { return this._icon; }
            set { this.SetProperty(ref this._icon, value); }
        }

        public string Text
        {
            get { return this._text; }
            set { this.SetProperty(ref this._text, value); }
        }

        public bool IsEnabled
        {
            get { return this._isEnabled; }
            set { this.SetProperty(ref this._isEnabled, value); }
        }

        public DelegateCommand Command { get; private set; }

        public MenuItem()
        {
            this.IsEnabled = true;

            // set defaults
            this.Icon = new BitmapImage(new Uri("pack://application:,,,/Resources/Icons/Info-64.png"));
            this.Text = "-";
        }

        public MenuItem SetCommand(DelegateCommand command)
        {
            this.Command = command;
            return this;
        }

        public MenuItem SetIcon(BitmapImage image)
        {
            this.Icon = image;
            return this;
        }

        public MenuItem SetText(string text)
        {
            this.Text = text;
            return this;
        }
    }
}