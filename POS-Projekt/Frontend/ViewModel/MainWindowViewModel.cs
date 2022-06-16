using Backend.Domain.Interfaces;
using Backend.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Frontend.ViewModel
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		private readonly ISongService _songService = null!;
		private MediaPlayer player;

		public MainWindowViewModel(ISongService songService)
		{ 
			_songService = songService;
			player = new MediaPlayer();
			player.Open(new Uri("C:/Users/admin/Documents/Schule/3.Klasse/POS/POS-Projekt/POS-Projekt/Frontend/music/StarShopping.mp3"));
			player.Play();
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
