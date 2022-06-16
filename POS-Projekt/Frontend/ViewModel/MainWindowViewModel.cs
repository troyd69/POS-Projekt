using Backend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Frontend.ViewModel
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		private readonly ISongService _songService = null!;

		public MainWindowViewModel(ISongService songService)
		{ 
			_songService = songService;
		}

		string _activeMenu = "Anmelden";

		public string ActiveMenu
		{
			get => _activeMenu;
			set
			{
				_activeMenu = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ActiveMenu)));
			}
		}


	}
}
