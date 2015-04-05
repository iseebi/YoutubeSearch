using Cirrious.MvvmCross.ViewModels;
using Cirrious.CrossCore;

namespace YouTubeSearch.Core.ViewModels
{
    public class FirstViewModel 
		: MvxViewModel
    {
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
                return _doSearchCommand ?? (_doSearchCommand = new MvxCommand(() => {
                    Mvx.Trace("SearchTapped: {0}", SearchWord);
                }));
            }
        }
        MvxCommand _doSearchCommand;
    }
}
