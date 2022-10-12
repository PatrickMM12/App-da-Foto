using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using App_da_Foto.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App_da_Foto.Views
{
    public partial class EditarPerfilPage : ContentPage
    {
        EditarPerfilViewModel _viewModel;

        public Fotografo Fotografo { get; set; }

        public GooglePlaceAutoCompletePrediction GooglePlaceAuto { get; set; }

        public EditarPerfilPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new EditarPerfilViewModel();
            list.IsVisible = false;
            list.BackgroundColor = Color.Green;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.OnAparecendo();

            while (entryEndereco.IsFocused)
            {
                list.IsVisible = true;
                list.BackgroundColor = Color.Red;
            }
            dtpNascimento.MaximumDate = DateTime.Now.AddYears(-16);

            if (_viewModel.Sexo == "M") { checkMasculino.IsChecked = true; entryMasculino.Text = "Masculino"; }
            else if (_viewModel.Sexo == "F") { checkFeminino.IsChecked = true; entryFeminino.Text = "Feminino"; }
            else { checkMasculino.IsChecked = false; entryMasculino.Text = String.Empty; checkFeminino.IsChecked = false; entryFeminino.Text = String.Empty; }

        }

        public async void OnEnterAddressTapped(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new BuscarLugarPage() { BindingContext = this.BindingContext }, false);

        }

        public void Handle_Stop_Clicked(object sender, EventArgs e)
        {
            //searchLayout.IsVisible = true;
            //stopRouteButton.IsVisible = false;
        }

        private void CheckMasculino_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (checkFeminino.IsChecked == false & checkMasculino.IsChecked == false)
            {
                entryMasculino.Text = String.Empty;
                entryFeminino.Text = String.Empty;
            }

            if (checkMasculino.IsChecked)
            {
                entryFeminino.Text = String.Empty;
                entryMasculino.Text = "Masculino";
                _viewModel.Sexo = "M";
            }
        }

        private void CheckFeminino_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (checkFeminino.IsChecked == false & checkMasculino.IsChecked == false)
            {
                entryMasculino.Text = String.Empty;
                entryFeminino.Text = String.Empty;
            }

            if (checkFeminino.IsChecked)
            {
                entryMasculino.Text = String.Empty;
                entryFeminino.Text = "Feminino";
                _viewModel.Sexo = "F";
            }
        }

        private void dtpNascimento_DateSelected(object sender, DateChangedEventArgs e)
        {
            dtpNascimento.TextColor = Color.FromHex("000000");
        }
    }
}