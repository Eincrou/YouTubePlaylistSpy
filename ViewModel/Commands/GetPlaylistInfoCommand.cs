
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace YouTubePlaylistSpy.ViewModel.Commands
{
    using System;
    using System.Windows.Input;
    using YouTubeParse;
    public class GetPlaylistInfoCommand : ICommand
    {
        private readonly MainWindowViewModel _viewModel;

        public GetPlaylistInfoCommand(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
        }
        public bool CanExecute(object parameter)
        {
            if (parameter == null) return false;
            return YouTubePlaylistPage.ValidatePlaylistUrl(parameter as string) && !_viewModel.IsGeneratingData;
        }

        public void Execute(object parameter)
        {
            _viewModel.GetPlaylistInfoAsync(parameter as string);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
