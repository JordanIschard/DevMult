using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Common.Api.Dtos;
using Fourplaces.Model;
using Fourplaces.Views;
using Storm.Mvvm;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace Fourplaces.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private List<PlaceItemSummary> __listplace = new List<PlaceItemSummary>();

        public List<PlaceItemSummary> ListPlace    
        {
            get => __listplace;
            set => SetProperty(ref __listplace, value);
        }

        public ICommand SignOut { get; }

        public MainViewModel()
        {
            //Debug.WriteLine("Start to instance MainViewModel");

            recupPlaces();
            SignOut = new Command(Disconnect);

            //Debug.WriteLine("Finish to instance MainViewModel");
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
