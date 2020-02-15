using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Storm.Mvvm;

namespace Fourplaces
{
    public partial class App : MvvmApplication
    {
        public App():base(() => new LoginPage())
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
