using App_da_Foto.Models;
using App_da_Foto.ViewModels;
using System;
using Xamarin.Forms;

namespace App_da_Foto.Views
{
    public partial class NovoFotografoPage : ContentPage
    {
        public Fotografo Fotografo { get; set; }
        public GooglePlaceAutoCompletePrediction googlePlaceAuto { get; set; }

        public NovoFotografoPage()
        {
            InitializeComponent();
            BindingContext = new NovoFotografoViewModel();
            list.IsVisible = false;
            list.BackgroundColor = Color.Green;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            while (entry.IsFocused)
            {
                list.IsVisible = true;
                list.BackgroundColor = Color.Red;
            }

        }

        public async void OnEnterAddressTapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new BuscarLugarPage() { BindingContext = this.BindingContext }, false);

        }

        public void Handle_Stop_Clicked(object sender, EventArgs e)
        {
            //searchLayout.IsVisible = true;
            //stopRouteButton.IsVisible = false;
        }
    }
}