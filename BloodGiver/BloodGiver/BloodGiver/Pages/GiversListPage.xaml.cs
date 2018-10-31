using BloodGiver.Models;
using BloodGiver.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodGiver.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GiversListPage : ContentPage
	{
        public ObservableCollection<BloodUser> BloodUsers;
        private string _bloodGroup;
        private string _country; 
		public GiversListPage (string country, string bloodGroup)
		{
			InitializeComponent ();
            _bloodGroup = bloodGroup;
            _country = country;
            BloodUsers = new ObservableCollection<BloodUser>();
            FindBloodDonars();
		}

        private async void FindBloodDonars()
        {
            var apiServices = new ApiServices();
            var bloodUsers = await apiServices.FindBloodGivers(_country, _bloodGroup);
            //BloodUsers = new ObservableCollection<BloodUser>(bloodUsers);
            foreach (var bloodUser in bloodUsers)
            {
                BloodUsers.Add(bloodUser);
            }
            LvBloodGivers.ItemsSource = BloodUsers;
        }

        private async void LvBloodGivers_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedGiver = e.SelectedItem as BloodUser;
            await Navigation.PushAsync(new GiverProfilePage(selectedGiver));
        }
    }
}