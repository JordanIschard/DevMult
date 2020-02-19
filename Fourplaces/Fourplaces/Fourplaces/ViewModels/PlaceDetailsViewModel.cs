using Common.Api.Dtos;
using Fourplaces.Model;
using Fourplaces.ViewModels;
using Storm.Mvvm;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Windows.Input;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace Fourplaces.Views
{
    internal class PlaceDetailsViewModel : ViewModelGen
    { 

        private PlaceItem __placeItem;

        private int __placeId;

        private string __comment;

        public PlaceItem Place { get => __placeItem; set => SetProperty(ref __placeItem, value); }

        public string Comment { get => __comment; set => SetProperty(ref __comment, value); }

        public ICommand Send { get; }

        public PlaceDetailsViewModel(int id)
        {
            __placeId = id;
            RecupUser();
            recupDetails(id);
            Return = new Command(Pop);
            Send = new Command(SendMessage);
        }

        private async void SendMessage(object _)
        {
            CreateCommentRequest create = new CreateCommentRequest();
            create.Text = Comment;

            ApiClient apiClient = new ApiClient();

            HttpResponseMessage responseMessage = await apiClient.Execute(HttpMethod.Post, Path.PLACES + "/" + __placeId + "/comments", create,RecupAccessToken());

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK: 
                    Result = "Comment Sended !";
                    Color = "Green";
                    recupDetails(__placeId);
                    break;
                case HttpStatusCode.BadRequest:
                    Result = "Bad Request";
                    Color = "Red";
                    break;
                case HttpStatusCode.NotFound:
                    Result = "Not Found";
                    Color = "Red";
                    break;
            }

            Comment = "";

        }

        private void Pop(object _)
        {
            NavigationService.PopAsync();
        }

        public ICommand Return { get; }

        public async void recupDetails(int id)
        {
            Debug.WriteLine("Start to try recup Details");
            ApiClient apiClient = new ApiClient();

            Debug.WriteLine("Client initialized");

            HttpResponseMessage response = await apiClient.Execute(HttpMethod.Get, Path.PLACES+"/"+id);

            Debug.WriteLine("Request GET Place Details done : " + response.StatusCode);

            Response<PlaceItem> res = await apiClient.ReadFromResponse<Response<PlaceItem>>(response);


            if (res.IsSuccess)
            {
                Place = res.Data;
            }
        }
    }
}