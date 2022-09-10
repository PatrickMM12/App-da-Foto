using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using App_da_Foto.ViewModels;
using System;
using Xamarin.Forms;

namespace App_da_Foto.Views
{
    public partial class NovoFotografoPage : ContentPage
    {
        public NovoFotografoPage()
        {
            InitializeComponent();
            BindingContext = new NovoFotografoViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}