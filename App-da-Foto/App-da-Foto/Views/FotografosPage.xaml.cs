using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using App_da_Foto.Services;
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

        public FotografosPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new FotografosViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAparecendo();
        }

        private void BtnBuscaNome_Clicked(object sender, System.EventArgs e)
        {
            EntryBuscaNome.Focus();
        }

        private void BtnBuscaEspecialidade_Clicked(object sender, System.EventArgs e)
        {
            PicEspecialidade.Focus();
        }

        private void BtnDeletarEspecialidade_Clicked(object sender, System.EventArgs e)
        {
            PicEspecialidade.SelectedIndex = -1;
        }

        private void VoltarParaTopo_Tapped(object sender, System.EventArgs e)
        {
            FotografosView.ScrollTo(1);
        }

        private void FotografosView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            if (e.VerticalOffset >= 500.0)
            {
                btnVoltarParaTopo.IsVisible = true;
                btnVoltarParaTopo.IsEnabled = true;
            }
            if (e.VerticalOffset <500.0)
            {
                btnVoltarParaTopo.IsVisible = false;
                btnVoltarParaTopo.IsEnabled = false;
            }

        }
    }
}