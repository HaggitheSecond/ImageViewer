using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Caliburn.Micro;
using ImageViewer.Services.Menu;
using ImageViewer.Views.Menu;

namespace ImageViewer.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainViewModel ViewModel => this.DataContext as MainViewModel;

        public MainView()
        {
            this.InitializeComponent();
        }

        private void FrameworkElement_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var view = sender as Grid;
            var menuService = IoC.Get<IMenuService>();

            if(view == null)
                return;

            menuService.SetMode(view.ActualWidth > 500 ? MenuState.Wide : MenuState.Minimized);
        }

        private void MainView_OnLoaded(object sender, RoutedEventArgs e)
        {
            // set initial keyboard focus
            this.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        private void MainView_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                return;
            }

            this.ViewModel.RouteKeyDownEvent(e);
        }
    }
}
