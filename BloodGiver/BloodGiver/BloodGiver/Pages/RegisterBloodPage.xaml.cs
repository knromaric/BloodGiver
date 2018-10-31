using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodGiver.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterBloodPage : ContentPage
	{
		public RegisterBloodPage ()
		{
			InitializeComponent ();
		}

        private void TapOpenCamera_Tapped(object sender, EventArgs e)
        {

        }

        private void BtnSubmit_Clicked(object sender, EventArgs e)
        {

        }
    }
}