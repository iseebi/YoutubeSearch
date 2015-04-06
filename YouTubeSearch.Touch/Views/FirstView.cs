using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;

namespace YouTubeSearch.Touch.Views
{
    [Register("FirstView")]
    public class FirstView : MvxViewController
    {
        public override void ViewDidLoad()
        {
            View = new UIView { BackgroundColor = UIColor.White };
            base.ViewDidLoad();

			// ios7 layout
            if (RespondsToSelector(new Selector("edgesForExtendedLayout")))
            {
               EdgesForExtendedLayout = UIRectEdge.None;
            }

            var searchField = new UITextField(CGRect.Empty)
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                BorderStyle = UITextBorderStyle.RoundedRect
            };
            View.Add(searchField);
            var searchButton = new UIButton(UIButtonType.System)
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
            };
            searchButton.SetTitle("Search", UIControlState.Normal);
            searchButton.SetContentHuggingPriority(250, UILayoutConstraintAxis.Horizontal);
            View.Add(searchButton);
            var tableView = new UITableView(CGRect.Empty)
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };
            View.Add(tableView);

            View.AddConstraints(new []
                {
                    NSLayoutConstraint.Create(searchField, NSLayoutAttribute.Top, NSLayoutRelation.Equal, 
                        TopLayoutGuide, NSLayoutAttribute.Bottom, 1.0f, 10),
                    NSLayoutConstraint.Create(searchField, NSLayoutAttribute.Left, NSLayoutRelation.Equal, 
                        View, NSLayoutAttribute.Left, 1.0f, 10),
                    NSLayoutConstraint.Create(searchField, NSLayoutAttribute.Right, NSLayoutRelation.Equal, 
                        searchButton, NSLayoutAttribute.Left, 1.0f, -10),

                    NSLayoutConstraint.Create(searchButton, NSLayoutAttribute.CenterY, NSLayoutRelation.Equal, 
                        searchField, NSLayoutAttribute.CenterY, 1.0f, 0),
                    NSLayoutConstraint.Create(searchButton, NSLayoutAttribute.Right, NSLayoutRelation.Equal, 
                        View, NSLayoutAttribute.Right, 1.0f, -10),

                    NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Top, NSLayoutRelation.Equal, 
                        searchField, NSLayoutAttribute.Bottom, 1.0f, 10),
                    NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Left, NSLayoutRelation.Equal, 
                        View, NSLayoutAttribute.Left, 1.0f, 0),
                    NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, 
                        View, NSLayoutAttribute.Right, 1.0f, 0),
                    NSLayoutConstraint.Create(tableView, NSLayoutAttribute.Bottom, NSLayoutRelation.Equal, 
                        View, NSLayoutAttribute.Bottom, 1.0f, 0),
                });

            var source = new MvxSimpleTableViewSource(tableView, 
                SearchResultTableViewCell.Key.ToString(), SearchResultTableViewCell.Key.ToString());
            tableView.Source = source;

            var set = this.CreateBindingSet<FirstView, Core.ViewModels.FirstViewModel>();
            set.Bind(searchField).To(vm => vm.SearchWord);
            set.Bind(searchButton).To(vm => vm.DoSearchCommand);
            set.Bind(source).For(v => v.ItemsSource).To(vm => vm.SearchResults);
            set.Bind(source).For(v => v.SelectionChangedCommand).To(vm => vm.FeedSelectCommand);
            set.Apply();

            searchButton.TouchUpInside += (sender, e) => searchField.ResignFirstResponder();
        }
    }
}