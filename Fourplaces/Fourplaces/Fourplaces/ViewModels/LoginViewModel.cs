using System.Net.Http;
using System.Windows.Input;
using Common.Api.Dtos;
using Fourplaces.Model;
using Fourplaces.Views;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace Fourplaces.ViewModels
{
    public class LoginViewModel : ViewModelGen
    {
        private string __email;
        private string __pwd;

        public string Email
        {
            get => __email;
            set => SetProperty(ref __email, value);
        }

        public string Password
        {
            get => __pwd;
            set => SetProperty(ref __pwd, value);
        }

        public ICommand SubmitCommand{ get; }

        public ICommand Close { get; }

        public ICommand NewUser{ get; }

        public async void OnSubmit(object _)
        {
            ApiClient apiClient = new ApiClient();
            LoginRequest request = new LoginRequest();
            request.Email = __email;
            request.Password = __pwd;

            //Debug.WriteLine("Request authentification : " + request.Email + " " + request.Password);

            HttpResponseMessage response = await apiClient.Execute(HttpMethod.Post,Path.AUTHLOG,request);

            //Debug.WriteLine("Request Done : " + response.StatusCode);

            Response<LoginResult> res = await apiClient.ReadFromResponse<Response<LoginResult>>(response);

            if (res.IsSuccess)
            {
                Result = "Authentification successful";
                Color = "Green";



                //  Debug.WriteLine("Parsing Done");

                Login login = Login.GetInstance();
                login.Add("at", res.Data.AccessToken);
                login.Add("rt", res.Data.RefreshToken);

                await NavigationService.PushAsync(new MainPage());

                Result = "";
                Password = "";
                Email = "";
                

            }
            else
            {
                Result = "Authentification failed";
                Color = "Red";
                Password = "";
                Email = "";
            }
        }
            


        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
            NewUser = new Command(Subscribe);
            Close = new Command(Quit);
        }

        private void Quit(object _)
        {
           
        }

        private async void Subscribe(object _)
        {
            await NavigationService.PushAsync(new SubscribePage());
        }


    }
}
