using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using App_da_Foto.Services;
using App_da_Foto.Utilities.Load;
using Geocoding.Google;
using Geocoding;
using Microsoft.AppCenter.Analytics;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.PlatformConfiguration;

namespace App_da_Foto.ViewModels
{
    class EditarPerfilViewModel : BaseViewModel
    {
        private int id;
        public int Id 
        {
            get
            {
                Fotografo fotografo = JsonConvert.DeserializeObject<Fotografo>(App.Current.Properties["Fotografo"].ToString());
                return id = fotografo.Id;
            }
            set => SetProperty(ref id, value);
        }

        private string nome;
        public string Nome
        {
            get => nome;
            set => SetProperty(ref nome, value);
        }

        private string especialidade;
        public string Especialidade
        {
            get => especialidade;
            set => SetProperty(ref especialidade, value);
        }

        private string sexo;
        public string Sexo
        {
            get => sexo;
            set => SetProperty(ref sexo, value);
        }

        private DateTime nascimento;
        public DateTime Nascimento
        {
            get => DateTime.Parse(nascimento.ToString("yyyy/MM/dd"));
            set => SetProperty(ref nascimento, value);
        }

        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private string senha;
        public string Senha
        {
            get => senha;
            set => SetProperty(ref senha, value);
        }

        private bool enderecoNull = true;
        public bool EnderecoNull
        {
            get => enderecoNull;
            set => SetProperty(ref enderecoNull, value);
        }

        private int idEndereco;
        public int IdEndereco
        {
            get => idEndereco;
            set => SetProperty(ref idEndereco, value);
        }

        private string logradouro;
        public string Logradouro
        {
            get => logradouro;
            set => SetProperty(ref logradouro, value);
        }

        private string numero;
        public string Numero
        {
            get => numero;
            set => SetProperty(ref numero, value);
        }

        private string complemento;
        public string Complemento
        {
            get => complemento;
            set => SetProperty(ref complemento, value);
        }

        private string bairro;
        public string Bairro
        {
            get => bairro;
            set => SetProperty(ref bairro, value);
        }

        private string cidade;
        public string Cidade
        {
            get => cidade;
            set => SetProperty(ref cidade, value);
        }

        private string estado;
        public string Estado
        {
            get => estado;
            set => SetProperty(ref estado, value);
        }

        private string cep;
        public string Cep
        {
            get => cep;
            set => SetProperty(ref cep, value);
        }

        private bool contatoNull = true;
        public bool ContatoNull
        {
            get => contatoNull;
            set => SetProperty(ref contatoNull, value);
        }

        private int idContato;
        public int IdContato
        {
            get => idContato;
            set => SetProperty(ref idContato, value);
        }

        private string telefone;
        public string Telefone
        {
            get => telefone;
            set => SetProperty(ref telefone, value);
        }

        private string tipoTelefone;
        public string TipoTelefone
        {
            get => tipoTelefone;
            set => SetProperty(ref tipoTelefone, value);
        }

        private string instagram;
        public string Instagram
        {
            get => instagram;
            set => SetProperty(ref instagram, value);
        }

        private string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        private ObservableCollection<string> _listaTipoTelefone = new ObservableCollection<string>
                {
                "CEL",
                "RES",
                "COM"
                };
        public ObservableCollection<string> ListaTipoTelefone
        {
            get
            {
                return _listaTipoTelefone;
            }
            set
            {
                _listaTipoTelefone = value;
                OnPropertyChanged();
            }
        }

        FotografoCompletoService fotografoCompletoService;
        public FotografoCompletoService FotografoCompletoService 
        {
            get { return fotografoCompletoService; }
            set { SetProperty(ref fotografoCompletoService, value); }
        }

        public Command SalvarFotografoCommand { get; set; }
        public Command SalvarEnderecoCommand { get; }
        public Command BuscarCepCommand { get; }
        public Command SalvarContatoCommand { get; }
        public Command CancelCommand { get; }
        public Command CarregarCommand { get; }

        IGeocoder geocoder;
        public IGeocoder Geocoder 
        { 
            get => geocoder; 
            set => geocoder = value; 
        }

        string _originLatitud;
        string _originLongitud;
        string _destinationLatitud;
        string _destinationLongitud;

        string _pickupText;
        public string PickupText
        {
            get
            {
                return _pickupText;
            }
            set
            {
                _pickupText = value;
                if (!string.IsNullOrEmpty(_pickupText))
                {
                    _isPickupFocused = true;
                    GetPlacesCommand.Execute(_pickupText);
                }
            }
        }

        string _originText;
        public string OriginText
        {
            get
            {
                return _originText;
            }
            set
            {
                _originText = value;
                if (!string.IsNullOrEmpty(_originText))
                {
                    _isPickupFocused = false;
                    GetPlacesCommand.Execute(_originText);
                }
            }
        }

        IGoogleMapsApiService googleMapsApi = new GoogleMapsApiService();

        public ObservableCollection<GooglePlaceAutoCompletePrediction> Places { get; set; }
        public ObservableCollection<GooglePlaceAutoCompletePrediction> RecentPlaces { get; set; } = new ObservableCollection<GooglePlaceAutoCompletePrediction>();
        
        GooglePlaceAutoCompletePrediction _placeSelected;
        public GooglePlaceAutoCompletePrediction PlaceSelected
        {
            get
            {
                return _placeSelected;
            }
            set
            {
                _placeSelected = value;
                if (_placeSelected != null)
                    GetPlaceDetailCommand.Execute(_placeSelected);
            }
        }

        public ICommand FocusOriginCommand { get; set; }
        public ICommand GetPlacesCommand { get; set; }
        public ICommand GetPlaceDetailCommand { get; set; }
        public ICommand GetLocationNameCommand { get; set; }

        public bool ShowRecentPlaces { get; set; }

        bool _isPickupFocused = true;

        public EditarPerfilViewModel()
        {
            FotografoCompletoService = new FotografoCompletoService();

            CarregarCommand = new Command(async () => await OnCarregarFotografoCompleto());
            SalvarFotografoCommand = new Command(OnSalvarFotografo, ValidarSalvarFotografo);
            this.PropertyChanged +=
                (_, __) => SalvarFotografoCommand.ChangeCanExecute();
            SalvarEnderecoCommand = new Command(OnSalvarEndereco);
            BuscarCepCommand = new Command(BuscarCEP);
            SalvarContatoCommand = new Command(OnSalvarContato);
            CancelCommand = new Command(OnCancel);

            Geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyD26qsgGUZ3IasPhI4S2HNXTQi6oQ_RMRo" };
            GetPlacesCommand = new Command<string>(async (param) => await GetPlacesByName(param));
            GetPlaceDetailCommand = new Command<GooglePlaceAutoCompletePrediction>(async (param) => await GetPlacesDetail(param));
        }

        public async Task OnAparecendo()
        {
            await Shell.Current.Navigation.PushPopupAsync(new Loading());
            await OnCarregarFotografoCompleto();
            await Shell.Current.Navigation.PopAllPopupAsync();
        }

        private async Task OnCarregarFotografoCompleto()
        {
            Analytics.TrackEvent("Carregar Fotografo Completo");

            try
            {
                ResponseService<FotografoCompleto> responseService = await FotografoCompletoService.ObterFotografo(Id);

                if (responseService.IsSuccess)
                {
                    Nome = responseService.Data.Fotografo.Nome;
                    Especialidade = responseService.Data.Fotografo.Especialidade;
                    Sexo = responseService.Data.Fotografo.Sexo;
                    if (responseService.Data.Fotografo.Nascimento != string.Empty)
                    {
                        Nascimento = DateTime.Parse(responseService.Data.Fotografo.Nascimento);
                    }
                    Email = responseService.Data.Fotografo.Email;
                    Senha = responseService.Data.Fotografo.Senha;

                    if (responseService.Data.Endereco != null)
                    {
                        EnderecoNull = false;
                        IdEndereco = responseService.Data.Endereco.Id;
                        Logradouro = responseService.Data.Endereco.Logradouro;
                        Numero = responseService.Data.Endereco.Numero;
                        Complemento = responseService.Data.Endereco.Complemento;
                        Bairro = responseService.Data.Endereco.Bairro;
                        Cidade = responseService.Data.Endereco.Cidade;
                        Estado = responseService.Data.Endereco.Estado;
                        Cep = responseService.Data.Endereco.Cep;
                    }

                    if (responseService.Data.Contato != null)
                    {
                        ContatoNull = false;
                        IdContato = responseService.Data.Contato.Id;
                        Telefone = responseService.Data.Contato.Telefone;
                        TipoTelefone = responseService.Data.Contato.TipoTelefone;
                        Instagram = responseService.Data.Contato.Instagram;
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Erro!", responseService.StatusCode.ToString() + ": " + responseService.Errors.ToString(), "OK");
                    await Shell.Current.GoToAsync("..");
                }
            }
            catch (Exception erro)
            {
                await Shell.Current.DisplayAlert("Erro!", erro.Message.ToString(), "Ok");
                await Shell.Current.GoToAsync("..");
            }
        }

        private async void OnSalvarFotografo()
        {
            if(ValidarSalvarFotografo())
            {
                Analytics.TrackEvent("Atualizar Fotografo");
                await Shell.Current.Navigation.PushPopupAsync(new Loading());
                Message = String.Empty;
                try
                {
                    Fotografo newFotografo = new Fotografo()
                    {
                        Id = Id,
                        Nome = Nome = Regex.Replace(nome, @"((^\w)|(\s|\p{P})\w)",
                                    match => match.Value.ToUpper()),
                        Especialidade = Especialidade,
                        Sexo = Sexo.ToUpper(),
                        Nascimento = Nascimento.ToString("yyyy-MM-dd"),
                        Email = Email.ToLower(),
                        Senha = Senha,
                    };
                    ResponseService<Fotografo> responseService = await FotografoService.AtualizarFotografo(newFotografo);

                    if (responseService.IsSuccess)
                    {
                        await Shell.Current.DisplayAlert("Sucesso!", "Cadastrado atualizado com Sucesso!", "Ok");
                    }

                    else
                    {
                        //await Shell.Current.DisplayAlert("Erro!", responseService.Errors.ToString(), "Ok");
                        //StringBuilder stringBuider = new StringBuilder();
                        //foreach (var dicKey in responseService.Errors)
                        //{
                        //    foreach (var message in dicKey.Value)
                        //    {
                        //        stringBuider.AppendLine(message);
                        //    }
                        //}
                        //Message = stringBuider.ToString();
                        Message = responseService.Errors.ToString();
                        await Shell.Current.DisplayAlert("Erro!", Message, "Ok");
                    }
                }
                catch (Exception ex)
                {
                    Message = "Erro!" + ex.Message.ToString();
                    await Shell.Current.DisplayAlert("Erro!", ex.ToString() + Message, "Ok");
                }

                await Shell.Current.Navigation.PopAllPopupAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("Ops!", "Campos Nome ou Senha vazio(s)!", "Ok");
            }

        }

        private bool ValidarSalvarFotografo()
        {
            return !String.IsNullOrWhiteSpace(Nome)
                && !String.IsNullOrWhiteSpace(Especialidade)
                && !String.IsNullOrWhiteSpace(Senha);
        }

        private async void OnSalvarEndereco()
        {
            if (ValidarSalvarEndereco())
            {
                Analytics.TrackEvent("Atualizar/Adicionar Endereço");
                await Shell.Current.Navigation.PushPopupAsync(new Loading());
                Message = String.Empty;
                try
                {
                    String endereco = Logradouro + ", " + Numero + " - " + Bairro + ", " + Cidade + " - " + Estado;

                    var localizacao = Geocoder.GeocodeAsync(endereco);

                    if (localizacao == null)
                    {
                        throw new Exception(Message = "Localização não encontrada");
                    }

                    Endereco newEndereco = new Endereco()
                    {
                        Id = IdEndereco,
                        Logradouro = Logradouro = Regex.Replace(logradouro, @"((^\w)|(\s|\p{P})\w)",
                                    match => match.Value.ToUpper()),
                        Numero = Numero,
                        Complemento = Complemento = Regex.Replace(complemento, @"((^\w)|(\s|\p{P})\w)",
                                    match => match.Value.ToUpper()),
                        Bairro = Bairro = Regex.Replace(bairro, @"((^\w)|(\s|\p{P})\w)",
                                    match => match.Value.ToUpper()),
                        Cidade = Cidade = Regex.Replace(cidade, @"((^\w)|(\s|\p{P})\w)",
                                    match => match.Value.ToUpper()),
                        Estado = Estado.ToUpper(),
                        Cep = Cep = Regex.Replace(cep, "[^0-9]+", ""),
                        Latitude = localizacao.Result.First().Coordinates.Latitude.ToString(),
                        Longitude = localizacao.Result.First().Coordinates.Longitude.ToString(),
                        IdFotografo = Id
                    };

                    ResponseService < Endereco > responseService = new ResponseService<Endereco>();

                    if (EnderecoNull == true)
                    {
                        responseService = await EnderecoService.AdicionarEndereco(newEndereco);
                    }
                    else
                    {
                        responseService = await EnderecoService.AtualizarEndereco(newEndereco);
                    }

                    if (responseService.IsSuccess)
                    {
                        await Shell.Current.DisplayAlert("Sucesso!", "Endereço atualizado com Sucesso!", "Ok");
                        EnderecoNull = false;
                    }

                    else
                    {
                        //await Shell.Current.DisplayAlert("Erro!", responseService.Errors.ToString(), "Ok");
                        //StringBuilder stringBuider = new StringBuilder();
                        //foreach (var dicKey in responseService.Errors)
                        //{
                        //    foreach (var message in dicKey.Value)
                        //    {
                        //        stringBuider.AppendLine(message);
                        //    }
                        //}
                        //Message = stringBuider.ToString();
                        Message = responseService.Errors.ToString();
                        await Shell.Current.DisplayAlert("Erro!", Message, "Ok");
                    }
                }
                catch (Exception ex)
                {
                    Message = "Erro: " + ex.Message.ToString();
                    await Shell.Current.DisplayAlert("Erro!", Message, "Ok");
                }
                await Shell.Current.Navigation.PopAllPopupAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("Ops!", "Preencha todos os seguintes campos: \r\n\r\nCEP \r\nLogradouro \r\nNumero \r\nBairro \r\nCidade \r\nEstado", "Ok");
            }

        }

        private bool ValidarSalvarEndereco()
        {
            return !String.IsNullOrWhiteSpace(logradouro)
                && !String.IsNullOrWhiteSpace(numero)
                && !String.IsNullOrWhiteSpace(bairro)
                && !String.IsNullOrWhiteSpace(cidade)
                && !String.IsNullOrWhiteSpace(estado)
                && !String.IsNullOrWhiteSpace(cep);
        }

        private async void BuscarCEP()
        {
            if (Cep != null && Cep.Count() == 9)
            {
                Analytics.TrackEvent("Buscar CEP");
                await Shell.Current.Navigation.PushPopupAsync(new Loading());
                Message = String.Empty;
                try
                {
                    EnderecoWeb enderecoLocalizado = await EnderecoService.BuscarCep(Cep);

                    if (enderecoLocalizado.logradouro != null)
                    {
                        Logradouro = enderecoLocalizado.logradouro;
                        Complemento = enderecoLocalizado.complemento;
                        Bairro = enderecoLocalizado.bairro;
                        Cidade = enderecoLocalizado.localidade;
                        Estado = enderecoLocalizado.uf;
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Ops!", "CEP não localizado!", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Erro!", ex.Message.ToString(), "Ok");
                }
                await Shell.Current.Navigation.PopAllPopupAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("Erro!", "Informe um CEP válido!", "Ok");
            }
        }

        private async void OnSalvarContato()
        {
            if (ValidarSalvarContato())
            {
                Analytics.TrackEvent("Atualizar Contato");
                await Shell.Current.Navigation.PushPopupAsync(new Loading());
                Message = String.Empty;

                try
                {
                    ResponseService<Contato> responseService = new ResponseService<Contato>();

                    Contato newContato = new Contato()
                    {
                        Id = IdContato,
                        Telefone = Telefone = Regex.Replace(telefone, "[^0-9]+", ""),
                        TipoTelefone = TipoTelefone,
                        Instagram = Instagram ?? "",
                        AcessosInstagram = 0,
                        IdFotografo = Id
                    };


                    if (ContatoNull == true)
                    {
                        responseService = await ContatoService.AdicionarContato(newContato);
                    }
                    else
                    {
                        responseService = await ContatoService.AtualizarContato(newContato);
                    }

                    if (responseService.IsSuccess)
                    {
                        await Shell.Current.DisplayAlert("Sucesso!", "Contato atualizado com Sucesso!", "Ok");
                        ContatoNull = false;
                    }

                    else
                    {
                        //StringBuilder stringBuider = new StringBuilder();
                        //foreach (var dicKey in responseService.Errors)
                        //{
                        //    foreach (var message in dicKey.Value)
                        //    {
                        //        stringBuider.AppendLine(message);
                        //    }
                        //}
                        //Message = stringBuider.ToString();
                        Message = responseService.Errors.ToString();
                        await Shell.Current.DisplayAlert("Erro!", Message, "Ok");
                    }
                }
                catch (Exception ex)
                {
                    Message = "Erro!" + ex.Message.ToString();
                    await Shell.Current.DisplayAlert("Erro!", ex.ToString() + Message, "Ok");
                }
                await Shell.Current.Navigation.PopAllPopupAsync();
            }
            else
            {
                await Shell.Current.DisplayAlert("Ops!", "Campos Numero de Celular ou Tipo de Telefone vazio(s)!", "Ok");
            }
        }

        private bool ValidarSalvarContato()
        {
            return !String.IsNullOrWhiteSpace(telefone)
                && !String.IsNullOrWhiteSpace(tipoTelefone);
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        public async Task GetPlacesByName(string placeText)
        {
            var places = await googleMapsApi.GetPlaces(placeText);
            var placeResult = places.AutoCompletePlaces;
            if (placeResult != null && placeResult.Count > 0)
            {
                Places = new ObservableCollection<GooglePlaceAutoCompletePrediction>(placeResult);
            }

            ShowRecentPlaces = (placeResult == null || placeResult.Count == 0);
        }

        public async Task GetPlacesDetail(GooglePlaceAutoCompletePrediction placeA)
        {
            var place = await googleMapsApi.GetPlaceDetails(placeA.PlaceId);
            if (place != null)
            {
                if (_isPickupFocused)
                {
                    PickupText = place.Name;
                    _originLatitud = $"{place.Latitude}";
                    _originLongitud = $"{place.Longitude}";
                    _isPickupFocused = false;
                    FocusOriginCommand.Execute(null);
                }
                else
                {
                    _destinationLatitud = $"{place.Latitude}";
                    _destinationLongitud = $"{place.Longitude}";

                    RecentPlaces.Add(placeA);

                    if (_originLatitud == _destinationLatitud && _originLongitud == _destinationLongitud)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Origin route should be different than destination route", "Ok");
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PopAsync(false);
                        CleanFields();
                    }

                }
            }
        }

        void CleanFields()
        {
            PickupText = OriginText = string.Empty;
            ShowRecentPlaces = true;
            PlaceSelected = null;
        }

        //public async Task GetLocationName(Position position)
        //{
        //    try
        //    {
        //        var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
        //        var placemark = placemarks?.FirstOrDefault();
        //        if (placemark != null)
        //        {
        //            PickupText = placemark.FeatureName;
        //        }
        //        else
        //        {
        //            PickupText = string.Empty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.ToString());
        //    }
        //}
    }
}
