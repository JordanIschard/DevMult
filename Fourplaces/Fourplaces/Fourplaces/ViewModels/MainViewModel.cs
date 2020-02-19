using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows.Input;
using Common.Api.Dtos;
using Fourplaces.Model;
using Fourplaces.Views;
using TD.Api.Dtos;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Fourplaces.ViewModels
{
    class MainViewModel : ViewModelGen
    {
        private List<PlaceItemSummary> __listplace = new List<PlaceItemSummary>();

        private UserItem __me;

        public UserItem Me { get => __me; set => SetProperty(ref __me, value); }

        public List<PlaceItemSummary> ListPlace    
        {
            get => __listplace;
            set => SetProperty(ref __listplace, value);
        }

        public ICommand SignOut { get; }

        public ICommand Add { get; }

        public ICommand Show { get; }

        public MainViewModel()
        {
            //Debug.WriteLine("Start to instance MainViewModel");
            RecupUser();
            recupPlaces();

            Show = new Command(GoToProfileAsync);
            SignOut = new Command(Disconnect);
            Add = new Command(AddingPlace);

            //Debug.WriteLine("Finish to instance MainViewModel");
        }

        private async void GoToProfileAsync(object _)
        {
           await NavigationService.PushAsync(new ProfilePage());
        }

        private void AddingPlace(object obj)
        {
            throw new NotImplementedException();
        }

        private void Disconnect(object _)
        {
            Login login = Login.GetInstance();
            login.Clear();
            NavigationService.PopAsync();
        }

        public async void showDetails(PlaceItemSummary placeItemSummary)
        {
            PlaceDetailsPage detailsPage = new PlaceDetailsPage();
            detailsPage.BindingContext = new PlaceDetailsViewModel(placeItemSummary.Id);
            await NavigationService.PushAsync(detailsPage);
        }

        public async void recupPlaces()
        {
            //Debug.WriteLine("Start to try recup places");
            ApiClient apiClient = new ApiClient();

            //Debug.WriteLine("Client initialized");

            HttpResponseMessage response = await apiClient.Execute(HttpMethod.Get, Path.PLACES);

            //Debug.WriteLine("Request GET Places done : " + response.StatusCode);

            Response<List<PlaceItemSummary>> res = await apiClient.ReadFromResponse<Response<List<PlaceItemSummary>>>(response);

            /*Debug.WriteLine("List : ");
            foreach (PlaceItemSummary itemSummary in res.Data)
            {
                Debug.WriteLine("List : " + itemSummary.ToString());
            }*/

            

            if (res.IsSuccess)
            {
                //Debug.WriteLine("Places recup");
                ListPlace = res.Data;
            }
     
              

        }


    }
}
