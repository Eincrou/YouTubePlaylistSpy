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
using YouTubePlaylistSpy.ViewModel;

namespace YouTubePlaylistSpy.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = FindResource("ViewModel") as MainWindowViewModel;
        }

        private void UrlInputButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_viewModel.GetPlaylistInfo(PlaylistUrlInput.Text))
                MessageBox.Show("Invalid YouTube Playlist URL. Please try again!", "Invalid URL", MessageBoxButton.OK,
                    MessageBoxImage.Error);
        }
    }
}
