
using System;

using Foundation;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Binding.BindingContext;

namespace YouTubeSearch.Touch.Views
{
    public partial class SearchResultTableViewCell : MvxTableViewCell
    {
        public static readonly UINib Nib = UINib.FromName("SearchResultTableViewCell", NSBundle.MainBundle);
        public static readonly NSString Key = new NSString("SearchResultTableViewCell");

        public SearchResultTableViewCell(IntPtr handle)
            : base(handle)
        {
            this.DelayBind(() =>
                {
                    var set = this.CreateBindingSet<MvxTableViewCell, Core.YouTubeFeed>();
                    set.Bind(ThumbnailImageView).To(vm => vm.LargeThumbnail.Url);
                    set.Bind(TitleLabel).To(vm => vm.Title);
                    set.Bind(ContentLabel).To(vm => vm.Content);
                    set.Apply();
                });
        }

        public static SearchResultTableViewCell Create()
        {
            return (SearchResultTableViewCell)Nib.Instantiate(null, null)[0];
        }
    }
}

