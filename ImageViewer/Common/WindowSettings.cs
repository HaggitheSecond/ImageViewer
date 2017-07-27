using System.Collections.Generic;
using System.Windows;

namespace ImageViewer.Common
{
    public class WindowSettings : Dictionary<string, object>
    {
        public static WindowSettings With => new WindowSettings();
        
        private WindowSettings()
        {
            
        }


        public WindowSettings SizeToContent(SizeToContent sizeToContent)
        {
            this[nameof(SizeToContent)] = sizeToContent;
            return this;
        }

        public WindowSettings StartupLocation(WindowStartupLocation location)
        {
            this[nameof(WindowStartupLocation)] = location;
            return this;
        }

        public WindowSettings Size(int width, int height)
        {
            this[nameof(Window.Width)] = width;
            this[nameof(Window.Height)] = height;
            return this;
        }

        public WindowSettings WindowState(WindowState windowState)
        {
            this[nameof(Window.WindowState)] = windowState;
            return this;
        }

        public WindowSettings ResizeMode(ResizeMode resizeMode)
        {
            this[nameof(Window.ResizeMode)] = resizeMode;
            return this;
        }
    }
}