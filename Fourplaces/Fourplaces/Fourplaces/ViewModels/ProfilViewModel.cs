using Fourplaces.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace Fourplaces.ViewModels
{
    class ProfilViewModel : ViewModelGen
    {
        private string __opwd ="";

        private string __npwd = "";

        public ICommand Return { get;  }
        public ICommand Modify { get;  }

        public string OldPassword { get => __opwd; set => SetProperty(ref __opwd, value); }

        public string NewPassword { get => __npwd; set => SetProperty(ref __npwd, value); }

        public ProfilViewModel()
        {
            RecupUser();
            Return = new Command(FuckGoBack);
            Modify = new Command(EditAsync);
        }

        private async void FuckGoBack(object _)
        {
            await NavigationService.PopAsync();
        }

        private async void EditAsync(object _)
        {

            ApiClient api = new ApiClient();
            HttpMethod patch = new HttpMethod("PATCH");

            if (!(OldPassword == "") || !(NewPassword == ""))
            {
                UpdatePasswordRequest update = new UpdatePasswordRequest();
                update.OldPassword = OldPassword;
                update.NewPassword = NewPassword;


                HttpResponseMessage response = await api.Execute(patch, Path.MYPASS, update,RecupAccessToken());

                if (!response.IsSuccessStatusCode)
                {
                    Result = "Editing Failed";
                    Color = "Red";
                    return;
                }

                
            }

            UpdateProfileRequest updateProfileRequest = new UpdateProfileRequest();
            updateProfileRequest.FirstName = User.FirstName;
            updateProfileRequest.LastName = User.LastName;
            updateProfileRequest.ImageId = null;

            HttpResponseMessage httpResponse = await api.Execute(patch, Path.ME, updateProfileRequest, RecupAccessToken());

            if (httpResponse.IsSuccessStatusCode)
            {
                User.FirstName = "";
                User.LastName = "";
                OldPassword = "";
                NewPassword = "";

                RecupUser();
                Result = "Editing successful";
                Color = "Green";
            }
            else
            {
                Result = "Editing Failed";
                Color = "Red";
            }

           
        }
    }
}
