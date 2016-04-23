using UIKit;
using AddressBookUI;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using contact_picker;
using contact_picker.iOS;


[assembly: ExportRenderer (typeof(ChooseContactPage), typeof(ChooseContactRenderer))]

namespace contact_picker.iOS
{
	public partial class ChooseContactRenderer : PageRenderer
	{
		ABPeoplePickerNavigationController _contactController;

		public string type_number;

		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);

			var page = e.NewElement as ChooseContactPage;

			if (e.OldElement != null || Element == null) {
				return;

			}

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			_contactController = new ABPeoplePickerNavigationController ();

			this.PresentModalViewController (_contactController, true); //display contact chooser


			_contactController.Cancelled += delegate {
				Xamarin.Forms.Application.Current.MainPage.Navigation.PopModalAsync ();

				this.DismissModalViewController (true); };

			_contactController.SelectPerson2 += delegate(object sender, ABPeoplePickerSelectPerson2EventArgs e) {

				var getphones = e.Person.GetPhones();
				string number = "";

				if (getphones == null)
				{
					number = "Nothing";
				}
				else if (getphones.Count > 1)
				{
					//il ya plus de 2 num de telephone
					foreach(var t in getphones)
					{
						number = t.Value + "/" + number;
					}
				}
				else if (getphones.Count == 1)
				{
					//il ya 1 num de telephone
					foreach(var t in getphones)
					{
						number = t.Value;
					}
				}


				Xamarin.Forms.Application.Current.MainPage.Navigation.PopModalAsync ();


				var twopage_renderer = new MyPage();
				MessagingCenter.Send<MyPage, string> (twopage_renderer, "num_select", number);
				this.DismissModalViewController (true);



			};
		}

		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();

			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;

			this.DismissModalViewController (true);
		}

		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

