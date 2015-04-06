using Android.App;
using Android.OS;
using Cirrious.MvvmCross.Droid.Views;
using Android.Widget;
using Android.Views.InputMethods;
using Android.Content;

namespace YouTubeSearch.Droid.Views
{
    [Activity(Label = "View for FirstViewModel")]
    public class FirstView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.FirstView);
            var textField = FindViewById<EditText>(Resource.Id.edittext_search);
            var button = FindViewById<Button>(Resource.Id.button_search);
            button.Click += (sender, e) => {
                var imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(textField.WindowToken, 0);
            };
        }
    }
}