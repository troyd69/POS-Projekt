using Backend.Domain.Interfaces;
using Backend.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Frontend.ViewModel
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		private readonly ISongService _songService = null!;
		private MediaPlayer player;

		private UUser selUser;







		//Relay Commands

		public ICommand AnmeldenBtn { get; }

		//-------------

		public MainWindowViewModel(ISongService songService)
		{

			Environment.CurrentDirectory = Environment.GetEnvironmentVariable("windir");
			DirectoryInfo info = new DirectoryInfo(".");

			Trace.WriteLine("Directory Info:   " + info.FullName);
			_songService = songService;

			Uri dings = new("ms - appx:///music/StarShopping.mp3");
			Trace.WriteLine(dings.AbsolutePath);
			player = new MediaPlayer();
			player.Open(new Uri(@"music/StarShopping.mp3", UriKind.Relative));
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

		public UUser SelUser
		{
			get => selUser;
			set
			{
				selUser = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelUser)));
			}
		}
	}
}
