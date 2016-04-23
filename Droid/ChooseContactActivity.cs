using Android.App;
using Android.OS;
using Android.Content;
using Android.Database;
using Xamarin.Forms;

namespace contact_picker.Droid
{

	[Activity (Label = "ChooseContactActivity")]

	public class ChooseContactActivity : Activity 
	{
		public string type_number = "";
		protected override void OnCreate (Bundle savedInstanceState)
		{

			base.OnCreate (savedInstanceState);

			Intent intent = new Intent(Intent.ActionPick, Android.Provider.ContactsContract.CommonDataKinds.Phone.ContentUri);
			StartActivityForResult(intent, 1);
		}

		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			// TODO Auto-generated method stub

			base.OnActivityResult (requestCode, resultCode, data);
			if (requestCode == 1) {
				if (resultCode == Result.Ok) {

					Android.Net.Uri contactData = data.Data;
					ICursor cursor = ContentResolver.Query(contactData, null, null, null, null);

					cursor.MoveToFirst();

					string number = cursor.GetString(cursor.GetColumnIndexOrThrow(Android.Provider.ContactsContract.CommonDataKinds.Phone.Number));


					var twopage_renderer = new MyPage();
					MessagingCenter.Send<MyPage, string> (twopage_renderer, "num_select", number);
					Finish ();
					Xamarin.Forms.Application.Current.MainPage.Navigation.PopModalAsync ();


				}
				else if (resultCode == Result.Canceled)
				{
					Finish ();               
				}
			}
		}
	}
}