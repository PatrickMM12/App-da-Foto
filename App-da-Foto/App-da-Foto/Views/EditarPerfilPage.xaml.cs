using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using App_da_Foto.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App_da_Foto.Views
{
    public partial class EditarPerfilPage : ContentPage
    {
        EditarPerfilViewModel _viewModel;

        public Fotografo Fotografo { get; set; }

        public EditarPerfilPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new EditarPerfilViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.OnAparecendo();

            dtpNascimento.MaximumDate = DateTime.Now.AddYears(-16);

            if (_viewModel.Sexo == "M") { checkMasculino.IsChecked = true; entryMasculino.Text = "Masculino"; }
            else if (_viewModel.Sexo == "F") { checkFeminino.IsChecked = true; entryFeminino.Text = "Feminino"; }
            else { checkMasculino.IsChecked = false; entryMasculino.Text = String.Empty; checkFeminino.IsChecked = false; entryFeminino.Text = String.Empty; }

            //if (_viewModel.FotoPerfil == null)
            //{
            //    var image = ImageSource.FromResource("Utilities.Imagens.perfil.png");
            //    fotoPerfil.Source = "Assets/Imagens/perfil.png";
            //}
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

        private void DataNascimentoSelecionada(object sender, DateChangedEventArgs e)
        {
            dtpNascimento.TextColor = Color.FromHex("000000");
        }

        private async void CarregarFoto(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Shell.Current.DisplayAlert("Não suportado!", "Não há suporte para carregar foto do dispositivo!", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
            {
                PhotoSize = PhotoSize.Large,
            });

            if (file == null)
                return;

            await DisplayAlert("File Location", file.Path, "OK");

            var stream = file.GetStream();

            MemoryStream ms = new MemoryStream();
            stream.CopyTo(ms);
            _viewModel.FotoPerfilByte = ms.ToArray();

            _viewModel.FotoPerfilStream = stream;
            _viewModel.FotoPerfil = ImageSource.FromStream(() => _viewModel.FotoPerfilStream);

            _viewModel.FotoCarregada = true;
        }
    }
}