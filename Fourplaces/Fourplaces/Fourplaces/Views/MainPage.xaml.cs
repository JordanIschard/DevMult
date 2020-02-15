using Fourplaces.ViewModels;
using Storm.Mvvm.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace Fourplaces
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : BaseContentPage
    {

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            PlaceItemSummary selectedItem = e.SelectedItem as PlaceItemSummary;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            PlaceItemSummary tappedItem = e.Item as PlaceItemSummary;
        }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
