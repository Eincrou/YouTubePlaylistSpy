using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using YouTubeParse;
using YouTubePlaylistSpy.Annotations;
using YouTubePlaylistSpy.ViewModel.Commands;

namespace YouTubePlaylistSpy.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Public Properties
        private string _urlInput;

        private bool _isGeneratingData;
        public bool IsGeneratingData
        {
            get { return _isGeneratingData; }
            set
            {
                if (value == _isGeneratingData) return;
                _isGeneratingData = value;
                OnPropertyChanged();
                GetPlaylistInfoCommand.RaiseCanExecuteChanged();
            }
        }

        public string UrlInput
        {
            get { return _urlInput; }
            set
            {
                if (value == _urlInput) return;
                _urlInput = value;
                OnPropertyChanged();
                GetPlaylistInfoCommand.RaiseCanExecuteChanged();
            }
        }
        private string _playlistTitle;
        public string PlaylistTitle
        {
            get { return _playlistTitle; }
            set
            {
                if (value == _playlistTitle) return;
                _playlistTitle = value;
                OnPropertyChanged();
            }
        }
        private string _owner;
        public string Owner
        {
            get { return _owner; }
            set
            {
                if (value == _owner) return;
                _owner = value;
                OnPropertyChanged();
            }
        }
        private int _playlistVideoCount;
        public int PlaylistVideoCount
        {
            get { return _playlistVideoCount; }
            set
            {
                if (value == _playlistVideoCount) return;
                _playlistVideoCount = value;
                OnPropertyChanged();
            }
        }

        private int _playlistViewCount;
        public int PlaylistViewCount
        {
            get { return _playlistViewCount; }
            set
            {
                if (value == _playlistViewCount) return;
                _playlistViewCount = value;
                OnPropertyChanged();
            }
        }
        private DateTime _lastUpdated;
        public DateTime LastUpdated
        {
            get { return _lastUpdated; }
            set
            {
                if (value.Equals(_lastUpdated)) return;
                _lastUpdated = value;
                OnPropertyChanged();
            }
        }
        private string _totalDuration;
        public string TotalDuration
        {
            get { return _totalDuration; }
            set
            {
                if (value.Equals(_totalDuration)) return;
                _totalDuration = value;
                OnPropertyChanged();
            }
        }
        private int _totalVideoViewCount;
        public int TotalVideoViewCount
        {
            get { return _totalVideoViewCount; }
            set
            {
                if (value == _totalVideoViewCount) return;
                _totalVideoViewCount = value;
                OnPropertyChanged();
            }
        }
        private int _viewsAvg;
        public int ViewsAvg
        {
            get { return _viewsAvg; }
            set
            {
                if (value == _viewsAvg) return;
                _viewsAvg = value;
                OnPropertyChanged();
            }
        }
        private int _totalCommentsCount;
        public int TotalCommentsCount
        {
            get { return _totalCommentsCount; }
            set
            {
                if (value == _totalCommentsCount) return;
                _totalCommentsCount = value;
                OnPropertyChanged();
            }
        }
        private int _commentsAvg;
        public int CommentsAvg
        {
            get { return _commentsAvg; }
            set
            {
                if (value == _commentsAvg) return;
                _commentsAvg = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _playlistVideos;
        public ObservableCollection<string> PlaylistVideos
        {
            get { return _playlistVideos; }
            set
            {
                if (Equals(value, _playlistVideos)) return;
                _playlistVideos = value;
                OnPropertyChanged();
            }
        }

        private YouTubePlaylistPage _youTubePlaylist;
        private YouTubeVideoGroup _videoGroup;
        #endregion
        #region Commands
        public GetPlaylistInfoCommand GetPlaylistInfoCommand { get; set; }
        #endregion

        public MainWindowViewModel()
        {
            GetPlaylistInfoCommand = new GetPlaylistInfoCommand(this);
            TotalDuration = string.Empty;
        }
        public async Task<bool> GetPlaylistInfoAsync(string textInput)
        {
            if (!YouTubePlaylistPage.ValidatePlaylistUrl(textInput)) return false;
            IsGeneratingData = true;
            _youTubePlaylist = new YouTubePlaylistPage(textInput);
            await _youTubePlaylist.DownloadPageAsync();
            _videoGroup = await _youTubePlaylist.GetYouTubeVideoGroupAsync();
            SetInformation();
            IsGeneratingData = false;
            return true;
        }

        private void SetInformation()
        {
            PlaylistTitle = _youTubePlaylist.Title;
            Owner = _youTubePlaylist.Owner;
            PlaylistVideoCount = _youTubePlaylist.NumVideos;
            PlaylistViewCount = _youTubePlaylist.Views;
            LastUpdated = _youTubePlaylist.LastUpdated;
            TotalDuration = SetTotalDuration();
            TotalVideoViewCount = _videoGroup.ViewsTotal;
            TotalCommentsCount = _videoGroup.CommentsTotal;
            PlaylistVideos = new ObservableCollection<string>(_videoGroup.TitlesList);
        }

        private string SetTotalDuration()
        {
            var sb = new StringBuilder();
            if (_videoGroup.DurationTotal.TotalHours > 0)
                sb.Append(string.Format("{0:####} hours, ", _videoGroup.DurationTotal.TotalHours));
            if (_videoGroup.DurationTotal.Minutes > 0)
                sb.Append(string.Format("{0} minutes, ", _videoGroup.DurationTotal.Minutes));
            sb.Append(string.Format("{0} seconds ", _videoGroup.DurationTotal.Seconds));
            return sb.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}