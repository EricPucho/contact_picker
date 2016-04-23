using System;

using Xamarin.Forms;

namespace contact_picker
{
	public class MyPage : ContentPage
	{
		Button button;
		public MyPage ()
		{
			button = new Button {
				Text = "choose contact"
			};

			button.Clicked += async (object sender, EventArgs e) => {
				
					if (Device.OS == TargetPlatform.iOS) {
						await Navigation.PushModalAsync (new ChooseContactPage ());
						}
					else if (Device.OS == TargetPlatform.Android)
					{
						MessagingCenter.Send (this, "android_choose_contact", "number1");
					}


			};

			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" },
					button
				}
			};
		}

		protected override void OnSizeAllocated (double width, double height)
		{
			base.OnSizeAllocated (width, height);

			MessagingCenter.Subscribe<MyPage, string> (this, "num_select", (sender, arg) => {
				DisplayAlert ("contact", arg, "OK");
			});
				
		}
	}
}


