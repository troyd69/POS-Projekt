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
		MediaPlayer player;

		public MainWindowViewModel(ISongService songService)
		{ 
			_songService = songService;
			player = new MediaPlayer();
			//player.MediaFailed += (o, args) =>
			//{
			//	MessageBox.Show(args.ToString());
			//};
			player.Open(new Uri("C://Users/nikol/Downloads/Lil Peep - Star Shopping(Removed At 1.9 Mil Views).mp3", UriKind.RelativeOrAbsolute));
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
