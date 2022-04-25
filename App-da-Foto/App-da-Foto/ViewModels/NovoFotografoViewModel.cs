using App_da_Foto.Models;
using System;
using Xamarin.Forms;

namespace App_da_Foto.ViewModels
{
	public class NovoFotografoViewModel : BaseViewModel
	{
		private string nome;
		private string especialidade;

		public NovoFotografoViewModel()
		{
			SaveCommand = new Command(OnSave, ValidateSave);
			CancelCommand = new Command(OnCancel);
			this.PropertyChanged +=
				(_, __) => SaveCommand.ChangeCanExecute();
		}

		private bool ValidateSave()
		{
			return !String.IsNullOrWhiteSpace(nome)
				&& !String.IsNullOrWhiteSpace(especialidade);
		}

		public string Nome
		{
			get => nome;
			set => SetProperty(ref nome, value);
		}

		public string Especialidade
		{
			get => especialidade;
			set => SetProperty(ref especialidade, value);
		}

		public Command SaveCommand { get; }
		public Command CancelCommand { get; }

		private async void OnCancel()
		{
			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}

		private async void OnSave()
		{
			Fotografo newFotografo = new Fotografo()
			{
				Id = Guid.NewGuid().ToString(),
				Nome = Nome,
				Especialidade = Especialidade
			};

			await DataStore.AddFotografoAsync(newFotografo);

			// This will pop the current page off the navigation stack
			await Shell.Current.GoToAsync("..");
		}
	}
}
