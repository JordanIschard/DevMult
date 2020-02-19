using Common.Api.Dtos;
using Fourplaces.Model;
using Storm.Mvvm;
using System.Net.Http;
using TD.Api.Dtos;

namespace Fourplaces.ViewModels
{
    public class ViewModelGen : ViewModelBase
    {
        private string __result;
        private string __color;
        private UserItem __userItem;

        public string Result { get => __result; set => SetProperty(ref __result, value); }

        public string Color { get => __color; set => SetProperty(ref __color, value); }

        public UserItem User { get => __userItem; set => SetProperty(ref __userItem, value); }

        public async void RefreshTokenAsync()
        {
            Login login = Login.GetInstance();

            ApiClient apiClient = new ApiClient();
            RefreshRequest refresh = new RefreshRequest();
            refresh.RefreshToken = login.Get("rt");
            HttpResponseMessage responseMessage = await apiClient.Execute(HttpMethod.Post, Path.AUTHREF, refresh);

            Response<LoginResult> result = await apiClient.ReadFromResponse<Response<LoginResult>>(responseMessage);

            if (result.IsSuccess)
            {
                login.Add("at", result.Data.AccessToken);
                login.Add("rt", result.Data.RefreshToken);
            }
            
        }

        public string RecupAccessToken()
        {
            Login l = Login.GetInstance();
            return l.Get("at");
        }

        public async void RecupUser()
        {

            Login login = Login.GetInstance();

            ApiClient apiClient = new ApiClient();
            string accessToken = login.Get("at");
            HttpResponseMessage responseMessage = await apiClient.Execute(HttpMethod.Get, Path.ME, null,accessToken) ;

            Response<UserItem> response = await apiClient.ReadFromResponse<Response<UserItem>>(responseMessage);

            if (response.IsSuccess)
            {
                User = response.Data;
            }
            

        }
    }
}
