using App_da_Foto.Models;
using App_da_Foto.ViewModels;
using Repositories;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace App_da_Foto.Views
{
    public partial class FotografosPage : ContentPage
    {
        FotografosViewModel _viewModel;

        FotografoRepositorio fotografoRepositorio;
        IEnumerable<Fotografo> fotografos;

        public FotografosPage()
        {
            InitializeComponent();

            fotografoRepositorio = new FotografoRepositorio();
            AtualizaDados();

            BindingContext = _viewModel = new FotografosViewModel();
        }

        async void AtualizaDados()
        {
            fotografos = await fotografoRepositorio.GetFotografosAsync();
            FotografosView.ItemsSource = fotografos.OrderBy(item => item.Nome).ToList();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAparecendo();
        }
    }
}