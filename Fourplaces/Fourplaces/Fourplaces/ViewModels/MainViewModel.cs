using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Dtos;
using Storm.Mvvm;
using TD.Api.Dtos;


namespace Fourplaces.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private List<PlaceItemSummary> __listplace;

        public List<PlaceItemSummary> ListPlace    
        {
            get => __listplace;
            set => SetProperty(ref __listplace, value);
        }

        public MainViewModel()
        {
            Task<List<PlaceItemSummary>> res = recupPlaces();

            if (res.IsCompleted)
            {
                ListPlace = res.Result;
            }
        }

        public async Task<List<PlaceItemSummary>> recupPlaces()
        {
            ApiClient apiClient = new ApiClient();

            HttpResponseMessage response = await apiClient.Execute(HttpMethod.Get, Path.PLACES);


            if (response.IsSuccessStatusCode)
            {
                Response<List<PlaceItemSummary>> res = await apiClient.ReadFromResponse<Response<List<PlaceItemSummary>>>(response);

                return res.Data;


            }

            return new List<PlaceItemSummary>();
        }


    }
}
