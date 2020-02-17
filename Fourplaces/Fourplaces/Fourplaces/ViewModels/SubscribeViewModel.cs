using Common.Api.Dtos;
using Fourplaces.Model;
using Fourplaces.Views;
using Storm.Mvvm;
using System.Net.Http;
using System.Windows.Input;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace Fourplaces.ViewModels
{
    class SubscribeViewModel : ViewModelBase
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

        public SubscribeViewModel()
        {
            Create = new Command(Creation);
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
                Login login = Login.GetInstance();
                login.Add("at", result.Data.AccessToken);
                login.Add("rt", result.Data.RefreshToken);

                await NavigationService.PushAsync(new MainPage());
            }
        }
    }
}
