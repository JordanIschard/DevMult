using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Common.Api.Dtos;
using Fourplaces.ViewModels;
using Storm.Mvvm;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace Fourplaces
{
    public class LoginViewModel : ViewModelBase
    {
        private string __email;
        private string __pwd;

        public string Email 
        {
            get => __email;
            set => SetProperty(ref __email, value);
        }

        public ICommand SubmitCommand{ get; }

        public async void OnSubmit(object _)
        {
            ApiClient apiClient = new ApiClient();
            LoginRequest request = new LoginRequest();
            request.Email = __email;
            request.Password = __pwd;

            HttpResponseMessage response = await apiClient.Execute(HttpMethod.Post,Path.AUTHLOG,request);


            if (response.IsSuccessStatusCode)
            {
                Response<LoginResult> res = await apiClient.ReadFromResponse<Response<LoginResult>>(response);
                   
                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("AccessToken",res.Data.AccessToken);
                data.Add("RefreshToken", res.Data.RefreshToken);
                await NavigationService.PushAsync(new MainPage(),data);
          

            }
        }
            


        public LoginViewModel()
        {
            SubmitCommand = new Command(OnSubmit);
        }
    }
}
