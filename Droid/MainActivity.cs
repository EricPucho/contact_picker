using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;

namespace contact_picker.Droid
{
	[Activity (Label = "contact_picker.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			LoadApplication (new App ());


			MessagingCenter.Subscribe<MyPage, string>(this, "android_choose_contact", (sender, args) => {
				Intent i = new Intent (Android.App.Application.Context, typeof(ChooseContactActivity));
				i.PutExtra ("number1", args);
				StartActivity (i);
			});
		}
	}
}

