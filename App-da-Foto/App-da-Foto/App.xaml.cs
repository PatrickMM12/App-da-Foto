using App_da_Foto.Services;
using App_da_Foto.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_da_Foto
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
