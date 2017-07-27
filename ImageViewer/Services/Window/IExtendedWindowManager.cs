using Caliburn.Micro;

namespace ImageViewer.Services.Window
{
    public interface IExtendedWindowManager : IWindowManager
    {
        void SwitchWindowFullScreenMode();
        WindowScreenState CurrentScreenState();
    }
}