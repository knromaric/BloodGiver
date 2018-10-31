using BloodGiver.Models;
using Plugin.Messaging;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodGiver.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GiverProfilePage : ContentPage
	{
        private string _email;
        private string _phoneNumber; 

		public GiverProfilePage (BloodUser bloodUser)
		{
			InitializeComponent ();
            ImgGiver.Source = bloodUser.FullLogoPath;
            LblBloodGroup.Text = bloodUser.BloodGroup;
            LblGiverName.Text = bloodUser.UserName;
            LblCountry.Text = bloodUser.Country;
            _email = bloodUser.Email;
            _phoneNumber = bloodUser.Phone;
		}

        private void TapCall_Tapped(object sender, System.EventArgs e)
        {
            var phoneDialer = CrossMessaging.Current.PhoneDialer;
            if (phoneDialer.CanMakePhoneCall)
                phoneDialer.MakePhoneCall(_phoneNumber);
        }

        private void TapEmail_Tapped(object sender, System.EventArgs e)
        {
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (emailMessenger.CanSendEmail)
            {
                // Send simple e-mail to single receiver without attachments, bcc, cc etc.
                emailMessenger.SendEmail("_email", "Blood Need", "I'm in the need of Blood! URGENT.");
            }
        }
    }
}