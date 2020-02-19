using Common.Api.Dtos;
using Fourplaces.Model;
using Fourplaces.Views;
using Plugin.Permissions.Abstractions;
using System;
using System.Net.Http;
using System.Windows.Input;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace Fourplaces.ViewModels
{
    class SubscribeViewModel : ViewModelGen
    {
        private string __email;
        private string __lname;
        private string __fname;
        private string __pwd;

        public string Email
        {
            get => __email;
            set => SetProperty(ref __email, value);
        }

        public string LastName
        {
            get => __lname;
            set => SetProperty(ref __lname, value);
        }

        public string FirstName
        {
            get => __fname;
            set => SetProperty(ref __fname, value);
        }

        public string Password
        {
            get => __pwd;
            set => SetProperty(ref __pwd, value);
        }

        public ICommand Create { get; }

        public ICommand Cancel { get;  }

       // public ICommand TakePhoto { get;  }

        //public ImageSource Image { get; set; }

        public SubscribeViewModel()
        {
            Create = new Command(Creation);
            Cancel = new Command(Canceled);
            //TakePhoto = new Command(TakingPhotoAsync);

        }

        /*
        private async void TakingPhotoAsync(object _)
        {
            Permission[] permissions = { Permission.Camera, Permission.Storage };


            await Plugin.Media.CrossMedia.Current.Initialize();

            await Plugin.Permissions.CrossPermissions.Current.RequestPermissionsAsync(permissions[0]);
            await Plugin.Permissions.CrossPermissions.Current.RequestPermissionsAsync(permissions[1]);

            PermissionStatus permissionStatus = await Plugin.Permissions.CrossPermissions.Current.CheckPermissionStatusAsync(permissions[0]);

            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
                Image = ImageSource.FromStream(() => { return photo.GetStream(); });
        }*/

        private void Canceled(object _)
        {
            NavigationService.PopAsync();
        }

        private async void Creation(object _)
        {
            RegisterRequest register = new RegisterRequest();
            register.Email = Email;
            register.FirstName = FirstName;
            register.LastName = LastName;
            register.Password = Password;

            ApiClient apiClient = new ApiClient();

            HttpResponseMessage responseMessage = await apiClient.Execute(HttpMethod.Post, Path.AUTHREG, register);

            Response<LoginResult> result = await apiClient.ReadFromResponse<Response<LoginResult>>(responseMessage);

            if (result.IsSuccess)
            {
                Result = "Creation successful !";
                Color = "Green";

                Login login = Login.GetInstance();
                login.Add("at", result.Data.AccessToken);
                login.Add("rt", result.Data.RefreshToken);

                await NavigationService.PushAsync(new MainPage());
            }
            else
            {
                Result = "Creation Failed !";
                Color = "Red";
                Email = "";
                LastName = "";
                FirstName = "";
                Password = "";
            }
        }
    }
}
