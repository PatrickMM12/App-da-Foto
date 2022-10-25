using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using App_da_Foto.Services;
using App_da_Foto.Utilities.Load;
using App_da_Foto.ViewModels;
using Microsoft.AppCenter.Analytics;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace App_da_Foto.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapaPage : ContentPage
    {
        MapaViewModel _viewModel;

        EnderecoService enderecoService = new EnderecoService();
        public EnderecoService EnderecoService
        {
            get => enderecoService;
            set => enderecoService = value;
        }

        public MapaPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new MapaViewModel();
            MoverParaLocalizacaoAtual();
            map.MyLocationEnabled = true;
            map.UiSettings.MyLocationButtonEnabled = true;

            map.MyLocationButtonClicked += (sender, e) =>
            {
                MoverParaLocalizacaoAtual();
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void CarregarPinsFotografos(object sender, Xamarin.Forms.GoogleMaps.CameraChangedEventArgs e)
        {
            if(e.Position.Zoom < 15)
            {
                map.Pins.Clear();
                return;
            }
            Analytics.TrackEvent("Carregar Pins dos Fotógrafos");
            try
            {
                double latitudeSul = map.Region.NearLeft.Latitude;
                double latitudeNorte = map.Region.FarLeft.Latitude;
                double longitudeOeste = map.Region.NearLeft.Longitude;
                double longitudeLeste = map.Region.NearRight.Longitude;

                IEnumerable<EnderecoFotografo> enderecoFotografos = await enderecoService.ObterEnderecosPorLocalizacao(latitudeSul, latitudeNorte, longitudeOeste, longitudeLeste);
                if (enderecoFotografos.Any())
                {
                    map.Pins.Clear();
                    foreach (EnderecoFotografo enderecoFotografo in enderecoFotografos)
                    {
                        var pin = new Pin
                        {
                            Type = PinType.Place,
                            Position = new Position(Convert.ToDouble(enderecoFotografo.Endereco.Latitude), Convert.ToDouble(enderecoFotografo.Endereco.Longitude)),
                            Icon = BitmapDescriptorFactory.FromBundle("logo"),
                            Label = enderecoFotografo.Endereco.Fotografo.Nome + " - " + enderecoFotografo.Endereco.Fotografo.Especialidade,
                            Address = enderecoFotografo.Endereco.Logradouro + ", " + enderecoFotografo.Endereco.Numero,
                            Tag = enderecoFotografo.Endereco.Fotografo
                        };

                        map.Pins.Add(pin);
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Atenção!", "Nenhum fotógrafo encontrado!", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro!", ex.Message.ToString(), "Ok");
            }
        }

        private async void MoverParaLocalizacaoAtual()
        {
            try
            {
                //var localizacao = CrossGeolocator.Current;
                //localizacao.DesiredAccuracy = 50;
                //var posicao = await localizacao.GetPositionAsync(TimeSpan.FromSeconds(10.0));

                var localizacao = Geolocation.GetLastKnownLocationAsync();

                if (localizacao != null & localizacao.IsCompleted)
                {
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(
                        new Position(localizacao.Result.Latitude, localizacao.Result.Longitude),
                        Distance.FromMiles(0.5)));
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro!", ex.Message.ToString(), "Ok");
                map.MoveToRegion(MapSpan.FromCenterAndRadius(
                        new Position(-23.5489, -46.6388),
                        Distance.FromMiles(0.5)));
            }
        }

        private async void IrPerfilFotografo(object sender, InfoWindowClickedEventArgs e)
        {
            Fotografo fotografo = new Fotografo();
            fotografo = (Fotografo)e.Pin.Tag;
            if (fotografo == null) return;

            await Shell.Current.Navigation.PushPopupAsync(new Loading());
            await Shell.Current.GoToAsync($"{nameof(FotografoPerfilPage)}?{nameof(FotografoPerfilViewModel.FotografoId)}={fotografo.Id}");
        }
    }
}