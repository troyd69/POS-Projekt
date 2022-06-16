using Backend.Domain.Interfaces;
using Backend.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Frontend.ViewModel
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;


		private readonly ISongService _songService = null!;
		private readonly ICategoryService _categoryService = null!;
		private readonly IPlaylistService _playlistService = null!;
		private readonly IArtistService _artistService = null!;
		private readonly IUserService _userService = null!;

		string CurrentDirectory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);


		MediaPlayer player = new MediaPlayer();


		private UUser selUser;

		private string anmeldenUsernameTB;

		private string registrierenUsernameTB;

		private DateOnly registrierenDate;

		private List<SSong> songsVonPlaylist;

		private List<PPlaylist> playlistsVonUser;

		private PPlaylist selPlaylist;

		private SSong selSong;

		private bool isPlaying;

		private List<SSong> allSongs;

		private string selNameCreatePL;

		private SSong selSongAllSongsCreatePL;







		//Relay Commands

		public ICommand AnmeldenBtn { get; }

		public ICommand AnmeldenRegistrierenBtn { get; }

		public ICommand RegistrierenBtn { get; }

		public ICommand RegistrierenAnmeldenBtn { get; }

		public ICommand MainPageAlleSongsBtn { get; }

		public ICommand PausePlayBtn { get; }

		public ICommand CreatePlayListBTN { get; }

		public ICommand AddSongCreatePlayList { get; }





		//-------------

		public MainWindowViewModel(ISongService songService, ICategoryService categoryService, IPlaylistService playlistService, IArtistService artistService, IUserService userService)
		{
			_songService = songService;
			_categoryService = categoryService;
			_playlistService = playlistService;
			_artistService = artistService;
			_userService = userService;

			ActiveMenu = "Anmelden";
			SongsVonPlaylist = _songService.ListSongs();



            //var CurrentDirectory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //Console.WriteLine(CurrentDirectory);
            //player = new MediaPlayer();
            //player.Open(new Uri(CurrentDirectory + @"\music\StarShopping.mp3"));
            //player.Play();


            AnmeldenBtn = new RelayCommand(
			   (param) =>
			   {
				   var passwordbox = param as PasswordBox;
				   var password = passwordbox.Password;

				   UUser temp = _userService.Anmelden(anmeldenUsernameTB, password);

				   if (temp == null)
					   return;

				   SelUser = temp;
				   PlaylistsVonUser = _playlistService.PlayListvonUser(SelUser.UId);
				   ActiveMenu = "MainPage";
			   },
			   (param) => !(String.IsNullOrEmpty(anmeldenUsernameTB)));

			AnmeldenRegistrierenBtn = new RelayCommand(
			   () =>
			   {

				   ActiveMenu = "Registrieren";
			   },
			   () => true);

			RegistrierenAnmeldenBtn = new RelayCommand(
			   () =>
			   {

				   ActiveMenu = "Anmelden";
			   },
			   () => true);

			RegistrierenBtn = new RelayCommand(
			   (param) =>
			   {
				   var passwordbox = param as PasswordBox;
				   var password = passwordbox.Password;

				   UUser temp = _userService.Registrieren(RegistrierenUsernameTB, password, RegistrierenDate);

				   if (temp == null)
					   return;

				   SelUser = temp;
				   PlaylistsVonUser = _playlistService.PlayListvonUser(SelUser.UId);

				   ActiveMenu = "MainPage";
			   },
			   (param) => !(String.IsNullOrEmpty(RegistrierenUsernameTB)));

			MainPageAlleSongsBtn = new RelayCommand(
			   () =>
			   {

				   SongsVonPlaylist = _songService.ListSongs();
				   SelPlaylist = null;
			   },
			   () => true);

			PausePlayBtn = new RelayCommand(
			   () =>
			   {

				   if (isPlaying)
					   player.Pause();
				   else
					   player.Play();
			   },
			   () => true);

			CreatePlayListBTN = new RelayCommand(
			  () =>
			  {

				  ActiveMenu = "CreatePlayList";
			  },
			  () => ActiveMenu != "Anmelden");

			AddSongCreatePlayList = new RelayCommand(
			   () =>
			   {

			   },
			   () => selSongAllSongsCreatePL != null);

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

		public string AnmeldenUsernameTB
		{
			get => anmeldenUsernameTB;
			set
			{
				anmeldenUsernameTB = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnmeldenUsernameTB)));
			}
		}

		public string RegistrierenUsernameTB
		{
			get => registrierenUsernameTB;
			set
			{
				registrierenUsernameTB = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RegistrierenUsernameTB)));
			}
		}
		public DateOnly RegistrierenDate
		{
			get => registrierenDate;
			set
			{
				registrierenDate = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RegistrierenDate)));
			}
		}

		public List<SSong> SongsVonPlaylist
		{
			get => songsVonPlaylist;
			set
			{
				songsVonPlaylist = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SongsVonPlaylist)));
			}
		}
		public List<PPlaylist> PlaylistsVonUser
		{
			get => playlistsVonUser;
			set
			{
				playlistsVonUser = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PlaylistsVonUser)));
			}
		}

        public PPlaylist SelPlaylist
		{
			get => selPlaylist;
			set
			{
				selPlaylist = value;
				if(selPlaylist != null)
					SongsVonPlaylist = _songService.GetSongsfromPlaylist(SelPlaylist);
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelPlaylist)));
			}
		}

        public SSong SelSong
		{
			get => selSong;
			set
			{
				selSong = value;
				player.Open(new Uri(CurrentDirectory + @"\music\StarShopping.mp3"));
				player.Play();
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelSong)));
			}
		}

		public List<SSong> AllSongs
		{
			get => allSongs;
			set
			{
				allSongs = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AllSongs)));
			}
		}

		public string SelNameCreatePL
		{
			get => selNameCreatePL;
			set
			{
				selNameCreatePL = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelNameCreatePL)));
			}
		}

		public SSong SelSongAllSongsCreatePL
		{
			get => selSongAllSongsCreatePL;
			set
			{
				selSongAllSongsCreatePL = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelSongAllSongsCreatePL)));
			}
		}
	}
}
