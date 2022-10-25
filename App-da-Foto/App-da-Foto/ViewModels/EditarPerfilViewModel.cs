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
using App_da_Foto.Views;
using System.IO;
using Windows.Storage;
using System.Threading;

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

        private Foto foto;
        public Foto Foto
        {
            get => foto;
            set => foto = value;
        }

        private bool fotoNull = true;
        public bool FotoNull
        {
            get => fotoNull;
            set => SetProperty(ref fotoNull, value);
        }

        private bool fotoCarregada = false;
        public bool FotoCarregada
        {
            get => fotoCarregada;
            set => SetProperty(ref fotoCarregada, value);
        }

        private ImageSource fotoPerfil;
        public ImageSource FotoPerfil
        {
            get => fotoPerfil;
            set => fotoPerfil = value;
        }

        private Stream fotoPerfilStream;
        public Stream FotoPerfilStream
        {
            get => fotoPerfilStream;
            set => fotoPerfilStream = value;
        }

        private byte[] fotoPerfilByte;
        public byte[] FotoPerfilByte
        {
            get => fotoPerfilByte;
            set => fotoPerfilByte = value;
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

        public EditarPerfilViewModel()
        {
            FotografoCompletoService = new FotografoCompletoService();
            Foto = new Foto();

            CarregarCommand = new Command(async () => await OnCarregarFotografoCompleto());
            SalvarFotografoCommand = new Command(OnSalvarFotografo, ValidarSalvarFotografo);
            this.PropertyChanged +=
                (_, __) => SalvarFotografoCommand.ChangeCanExecute();

            SalvarEnderecoCommand = new Command(OnSalvarEndereco);
            BuscarCepCommand = new Command(BuscarCEP);

            SalvarContatoCommand = new Command(OnSalvarContato);

            Geocoder = new GoogleGeocoder() { ApiKey = "AIzaSyD26qsgGUZ3IasPhI4S2HNXTQi6oQ_RMRo" };
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

                    if (responseService.Data.FotoPerfil != null)
                    {
                        Foto = responseService.Data.FotoPerfil;
                        FotoPerfilStream = new MemoryStream(Foto.Imagem);
                        FotoPerfil = ImageSource.FromStream(() => FotoPerfilStream);
                        FotoNull = false;
                    }

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
            if (ValidarSalvarFotografo())
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

                    ResponseService<Foto> responseServiceFoto = new ResponseService<Foto>();


                    // Foto de Perfil: Em desenvolvimento

                    if (FotoPerfil != null)
                    {
                        Foto foto = new Foto()
                        {
                            NomeArquivo = "Perfil" + id.ToString(),
                            Imagem = FotoPerfilByte,
                            Perfil = "S",
                            IdFotografo = Id,
                        };


                        if (FotoNull == true)
                        {
                            responseServiceFoto = await FotoService.AdicionarFoto(foto);
                        }
                        else
                        {
                            responseServiceFoto = await FotoService.AtualizarFoto(foto);
                        }
                    }
                    ResponseService<Fotografo> responseService = await FotografoService.AtualizarFotografo(newFotografo);


                    if (responseService.IsSuccess)
                    {
                        await Shell.Current.DisplayAlert("Sucesso!", "Cadastrado atualizado com Sucesso!", "Ok");
                    }
                    else
                    {
                        Message = responseServiceFoto.Errors.ToString();
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

                    ResponseService<Endereco> responseService = new ResponseService<Endereco>();

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
    }
}
