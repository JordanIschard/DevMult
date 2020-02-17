using Fourplaces.ViewModels;
using Storm.Mvvm.Forms;
using System.ComponentModel;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace Fourplaces.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }


        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((MainViewModel)BindingContext).showDetails(e.Item as PlaceItemSummary);
        }
    }
}
