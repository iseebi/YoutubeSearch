using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;
using System.Collections.Generic;
using Cirrious.MvvmCross.Plugins.WebBrowser;

namespace YouTubeSearch.Core.ViewModels
{
    public class FirstViewModel 
		: MvxViewModel
    {
        readonly IMvxWebBrowserTask _webBrowserTask;

        public FirstViewModel(IMvxWebBrowserTask webBrowserTask)
        {
            this._webBrowserTask = webBrowserTask;
        }

        public string SearchWord
        {
            get { return _searchWord; }
            set
            {
                _searchWord = value;
                RaisePropertyChanged(() => SearchWord);
            }
        }
        string _searchWord;

        public MvxCommand DoSearchCommand
        {
            get
            {
                return _doSearchCommand ?? (_doSearchCommand = new MvxCommand(async () => {
                    SearchResults = await YouTubeFeeds.Search(SearchWord);
                }));
            }
        }
        MvxCommand _doSearchCommand;

        public List<YouTubeFeed> SearchResults
        {
            get { return _searchResults;
            }
            set
            {
                _searchResults = value;
                RaisePropertyChanged(() => SearchResults);
            }
        }
        List<YouTubeFeed> _searchResults;

        public MvxCommand<YouTubeFeed> FeedSelectCommand
        {
            get
            {
                return _feedSelectCommand ?? (_feedSelectCommand = new MvxCommand<YouTubeFeed>(
                        feed => _webBrowserTask.ShowWebPage(feed.Url)));
            }
        }
        MvxCommand<YouTubeFeed> _feedSelectCommand;
    }
}
