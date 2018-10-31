using BloodGiver.Helpers;
using BloodGiver.Models;
using BloodGiver.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BloodGiver.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterBloodPage : ContentPage
	{
        private MediaFile _file; 
		public RegisterBloodPage ()
		{
			InitializeComponent ();
		}

        private async void TapOpenCamera_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            _file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (_file == null)
                return;

            await DisplayAlert("File Location", _file.Path, "OK");

            ImgGiver.Source = ImageSource.FromStream(() =>
            {
                var stream = _file.GetStream();
                return stream;
            });
        }

        private async void BtnSubmit_Clicked(object sender, EventArgs e)
        {
            var imageArray = FilesHelper.ReadFully(_file.GetStream());
            _file.Dispose();
            var country = PickerCountry.Items[PickerCountry.SelectedIndex];
            var bloodGroup = PickerBloodGroup.Items[PickerBloodGroup.SelectedIndex];

            DateTime currentTime = DateTime.Now;
            int date = Convert.ToInt32(currentTime .ToOADate());
            var bloodUser = new BloodUser()
            {
                UserName = EntName.Text,
                Email = EntEmail.Text,
                Phone = EntPhone.Text,
                BloodGroup = bloodGroup,
                Country = country,
                ImageArray = imageArray,
                Date = date
            };

            var apiServices = new ApiServices();
            var response =  await apiServices.RegisterGiver(bloodUser);

            if (response)
                await DisplayAlert("Hi", "The record has been added successfully", "Ok");
            else
                await DisplayAlert("Alert", "Something went Wrong", "Cancel");
        }
    }
}