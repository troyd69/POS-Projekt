using Backend.Domain.Interfaces;
using Backend.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Imaging;

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

		private bool isPlaying = true;

		private List<SSong> allSongs;

		private string selNameCreatePL;

		private string selNameEditPL;
		
		private SSong selSongAllSongsCreatePL;

		private ObservableCollection<SSong> addSongsCreatePL;

		private SSong selAddSongsCreatePL;

		private ObservableCollection<SSong> addSongsEditPL;
		
		private string songNameTB;

		private CCategory selCategory;

		private List<CCategory> allCategories;

		private string fileName;

		private SSong selAddSongsEditPL;


		private SSong selSongAllSongsEditPL;

		private AArtist selArtist;

		OpenFileDialog dialog = new OpenFileDialog();










		//Relay Commands

		public ICommand AnmeldenBtn { get; }

		public ICommand AnmeldenRegistrierenBtn { get; }

		public ICommand RegistrierenBtn { get; }

		public ICommand RegistrierenAnmeldenBtn { get; }

		public ICommand MainPageAlleSongsBtn { get; }

		public ICommand PausePlayBtn { get; }

		public ICommand CreatePlayListBTN { get; }

		public ICommand AddSongCreatePlayList { get; }

		public ICommand AddSongEditPlayList { get; }

		public ICommand LetzerSong { get; }

		public ICommand NaechsterSong { get; }

		public ICommand AddSongCreatePlayListRemoveBtn { get; }
		public ICommand AddSongEditPlayListRemoveBtn { get; }
		
		public ICommand CreatePlayListDBBtn { get; }

		public ICommand EditPlayListBTN { get; }

		public ICommand EditPlayListDBBtn { get; }
		
		public ICommand MainPageAddNewSongMenuItem { get; }

		public ICommand HomeBtn { get; }

		public ICommand LogOutBtn { get; }

		public ICommand SelectFile { get; }

		public ICommand AddSong { get; }





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
			AllCategories = _categoryService.ListCategorys();
			SelCategory = AllCategories[0];

			player.MediaEnded += MediaEnded;


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

				   isPlaying = !isPlaying;
			   },
			   () => SelSong != null);

			LetzerSong = new RelayCommand(
			   () =>
			   {
				   int index = SongsVonPlaylist.IndexOf(SelSong);

				   Trace.WriteLine("dings " +  SongsVonPlaylist.IndexOf(SelSong));


				   if (index > 0)
					   SelSong = SongsVonPlaylist[--index];
				   else
					   SelSong = SongsVonPlaylist[SongsVonPlaylist.Count - 1];

				   
			   },
			   () => SelSong != null);

			NaechsterSong = new RelayCommand(
			   () =>
			   {
				   int index = SongsVonPlaylist.IndexOf(SelSong);

				   if (index < (SongsVonPlaylist.Count - 1))
					   SelSong = SongsVonPlaylist[++index];
				   else
					   SelSong = SongsVonPlaylist[0];
			   },
			   () => SelSong != null);

			CreatePlayListBTN = new RelayCommand(
			  () =>
			  {
				  AllSongs = _songService.ListSongs();//-----------------------------------------------------------
				  AddSongsCreatePL = new();
				  ActiveMenu = "CreatePlayList";
			  },
			  () => ActiveMenu != "Anmelden");

			AddSongCreatePlayList = new RelayCommand(
			   () =>
			   {
				   if (!AddSongsCreatePL.Contains(selSongAllSongsCreatePL))
					   AddSongsCreatePL.Add(SelSongAllSongsCreatePL);
				   PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddSongsCreatePL)));
			   },
			   () => selSongAllSongsCreatePL != null);

			AddSongEditPlayList = new RelayCommand(
			   () =>
			   {
				   if (!AddSongsEditPL.Contains(SelSongAllSongsEditPL))
					   AddSongsEditPL.Add(SelSongAllSongsEditPL);
				   PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddSongsEditPL)));
			   },
			   () => selSongAllSongsEditPL != null); 
			
			AddSongCreatePlayListRemoveBtn = new RelayCommand(
			() =>
			{
				AddSongsCreatePL.Remove(SelAddSongsCreatePL);
			},
			() => SelAddSongsCreatePL != null);

			AddSongEditPlayListRemoveBtn = new RelayCommand(
			() =>
			{
				AddSongsEditPL.Remove(SelAddSongsEditPL);
			},
			() => SelAddSongsEditPL != null); 
			
			CreatePlayListDBBtn = new RelayCommand(
			  () =>
			  {
				  if (AddSongsCreatePL.Count > 0)
					  if (SelNameCreatePL != null)
						  _playlistService.AddPlayList(SelNameCreatePL, SelUser.UId, AddSongsCreatePL.ToList());
				  PlaylistsVonUser = _playlistService.PlayListvonUser(SelUser.UId);
				  ActiveMenu = "MainPage";
			  },
			  () => SelAddSongsCreatePL != null);

			EditPlayListDBBtn = new RelayCommand(
			  () =>
			  {
				  if (AddSongsEditPL.Count > 0)
					  if (SelNameEditPL != null)
						  _playlistService.ChangePlayList(SelNameEditPL, AddSongsEditPL.ToList(), SelPlaylist.PId);
				  PlaylistsVonUser = _playlistService.PlayListvonUser(SelUser.UId);
				  SongsVonPlaylist = _songService.GetSongsfromPlaylist(SelPlaylist);
				  ActiveMenu = "MainPage";
			  },
			  () => SelAddSongsEditPL != null); 
			
			EditPlayListBTN = new RelayCommand(
			   () =>
			   {
				   AddSongsEditPL = new(_songService.GetSongsfromPlaylist(SelPlaylist));
				   AllSongs = _songService.ListSongs();//-----------------------------------------------------------
				   try
				   {
					   SelNameEditPL = SelPlaylist.PName;
					   if (SelPlaylist != null)
					   {
						   ActiveMenu = "EditPlayList";
					   }
				   }
				   catch (Exception ex)
				   { 
				   
				   }
				   
			   },
			   () => ActiveMenu != "Anmelden");

			MainPageAddNewSongMenuItem = new RelayCommand(
			   () =>
			   {

				   ActiveMenu = "AddSong";
			   },
			   () => ActiveMenu != "Anmelden");

			HomeBtn = new RelayCommand(
			   () =>
			   {

				   ActiveMenu = "MainPage";
			   },
			   () => ActiveMenu != "Anmelden");

			LogOutBtn = new RelayCommand(
			   () =>
			   {

				   ActiveMenu = "Anmelden";
			   },
			   () => ActiveMenu != "Anmelden");


			SelectFile = new RelayCommand(
			   () =>
			   {


				   dialog.FileName = "Song.mp3"; // Default file name
				   dialog.DefaultExt = ".txt"; // Default file extension
				   dialog.Filter = "(.mp3)|*.mp3"; // Filter files by extension

				   // Show open file dialog box
				   bool? myResult;
				   myResult = dialog.ShowDialog();
				   if (myResult != null && myResult == true)
				   {

					   if (!Directory.Exists("music"))
					   {
						   Directory.CreateDirectory("music");
					   }

					   FileName = dialog.SafeFileName.Substring(0, (dialog.SafeFileName.Length - 4));
				   }
			   },
			   () => true);

			AddSong = new RelayCommand(
			   () =>
			   {

				   string filePath = CurrentDirectory + $@"\music\" + Path.GetFileName(FileName) + ".mp3";
				   File.Copy(dialog.FileName, filePath, true);
				   _songService.AddSong(SongNameTB, SelCategory, FileName, SelArtist);
				   SongsVonPlaylist = _songService.ListSongs();
				   ActiveMenu = "MainPage";
			   },
			   () => SelCategory != null &&
						!String.IsNullOrEmpty(FileName) &&
						!String.IsNullOrEmpty(songNameTB) &&
						SelArtist != null);
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
				
				if (value != null)
				{
					selPlaylist = value;
					SongsVonPlaylist = _songService.GetSongsfromPlaylist(SelPlaylist);
					AddSongsEditPL = new(_songService.GetSongsfromPlaylist(SelPlaylist));
				}
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelPlaylist)));
			}
		}

		public SSong SelSong
		{
			get => selSong;
			set
			{
				if (value != null)
                {

					selSong = value;
					player.Open(new Uri(CurrentDirectory + $@"\music\{selSong.SPath}.mp3"));
				}
					
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

		public string SelNameEditPL
		{
			get => selNameEditPL;
			set
			{
				selNameEditPL = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelNameEditPL)));
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

		public SSong SelSongAllSongsEditPL
		{
			get => selSongAllSongsEditPL;
			set
			{
				selSongAllSongsEditPL = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelSongAllSongsEditPL)));
			}
		}
		
		
		public ObservableCollection<SSong> AddSongsCreatePL
		{
			get => addSongsCreatePL;
			set
			{
				addSongsCreatePL = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddSongsCreatePL)));
			}
		}
		public SSong SelAddSongsCreatePL
		{
			get => selAddSongsCreatePL;
			set
			{
				selAddSongsCreatePL = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelAddSongsCreatePL)));
			}
		}

		public List<AArtist> AllArtists
		{
			get =>  _artistService.listArtists();
		}
        public string SongNameTB
		{
			get => songNameTB;
			set
			{
				songNameTB = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SongNameTB)));
			}
		}

        public CCategory SelCategory
		{
			get => selCategory;
			set
			{
				selCategory = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelCategory)));
			}
		}

        public List<CCategory> AllCategories
		{
			get => allCategories;
			set
			{
				allCategories = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AllCategories)));
			}
		}

        public string FileName
		{
			get => fileName;
			set
			{
				fileName = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FileName)));
			}
		}


		public ObservableCollection<SSong> AddSongsEditPL
		{
			get => addSongsEditPL;
			set
			{
				addSongsEditPL = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AddSongsEditPL)));
			}
		}
		public SSong SelAddSongsEditPL
		{
			get => selAddSongsEditPL;
			set
			{
				selAddSongsEditPL = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelAddSongsEditPL)));
			}
		}

        public AArtist SelArtist
		{
			get => selArtist;
			set
			{
				selArtist = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelArtist)));
			}
		}

		public void MediaEnded(object sender, EventArgs e)
        {
			int index = SongsVonPlaylist.IndexOf(SelSong);

			if (index < (SongsVonPlaylist.Count - 1))
				SelSong = SongsVonPlaylist[++index];
			else
				SelSong = SongsVonPlaylist[0];
		}
    }
}
