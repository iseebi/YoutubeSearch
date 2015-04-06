// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace YouTubeSearch.Touch.Views
{
	[Register ("SearchResultTableViewCell")]
	partial class SearchResultTableViewCell
	{
		[Outlet]
		UIKit.UILabel ContentLabel { get; set; }

		[Outlet]
		Cirrious.MvvmCross.Binding.Touch.Views.MvxImageView ThumbnailImageView { get; set; }

		[Outlet]
		UIKit.UILabel TitleLabel { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ThumbnailImageView != null) {
				ThumbnailImageView.Dispose ();
				ThumbnailImageView = null;
			}

			if (TitleLabel != null) {
				TitleLabel.Dispose ();
				TitleLabel = null;
			}

			if (ContentLabel != null) {
				ContentLabel.Dispose ();
				ContentLabel = null;
			}
		}
	}
}
