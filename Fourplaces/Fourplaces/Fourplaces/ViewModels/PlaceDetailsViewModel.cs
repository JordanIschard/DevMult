using Common.Api.Dtos;
using Fourplaces.Model;
using Storm.Mvvm;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using TD.Api.Dtos;

namespace Fourplaces.Views
{
    internal class PlaceDetailsViewModel : ViewModelBase
    {

        private int __idImage;

        private string __description;

        private string __title;

        private List<CommentItem> __list;

        public PlaceDetailsViewModel(int id)
        {
            recupDetails(id);
        }

        public string Title { get => __title; set => SetProperty(ref __title, value); }
        public string Description { get => __description; set => SetProperty(ref __description, value); }
        public int IdImage { get => __idImage; set => SetProperty(ref __idImage, value); }
        public List<CommentItem> List { get => __list; set => SetProperty(ref __list, value); }

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
                PlaceItem place = res.Data;
                Debug.WriteLine("Details recup");
                Title = place.Title;
                Description = place.Description;
                IdImage = place.ImageId;
                List = place.Comments;
            }
        }
    }
}