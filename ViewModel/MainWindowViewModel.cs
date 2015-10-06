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

namespace YouTubePlaylistSpy.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        #region Public Properties
        private string _urlInput;
        public string UrlInput
        {
            get { return _urlInput; }
            set
            {
                if (value == _urlInput) return;
                _urlInput = value;
                OnPropertyChanged();
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
        private TimeSpan _totalDuration;
        public TimeSpan TotalDuration
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

        private YouTubePlaylist _youTubePlaylist;
        private YouTubeVideoGroup _videoGroup;
        #endregion
        
        public bool GetPlaylistInfo(string textInput)
        {
            if (!YouTubePlaylist.ValidatePlaylistUrl(textInput)) return false;
            _youTubePlaylist = new YouTubePlaylist(textInput);
            _videoGroup = _youTubePlaylist.GetYouTubeVideoGroup();
            SetInformation();

            return true;
        }

        private void SetInformation()
        {
            PlaylistTitle = _youTubePlaylist.Title;
            Owner = _youTubePlaylist.Owner;
            PlaylistVideoCount = _youTubePlaylist.NumVideos;
            PlaylistViewCount = _youTubePlaylist.Views;
            LastUpdated = _youTubePlaylist.LastUpdated;
            TotalDuration = _youTubePlaylist.Duration;
            TotalVideoViewCount = _videoGroup.TotalViewsCount;
            TotalCommentsCount = _videoGroup.TotalCommentsCount;
            PlaylistVideos = new ObservableCollection<string>(_videoGroup.TitlesList);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}