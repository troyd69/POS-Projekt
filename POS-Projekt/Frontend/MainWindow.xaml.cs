using Backend.Model;
using Backend.Services;
using Frontend.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MusicDBContext context = new();
        public MainWindowViewModel vm;

        public MainWindow()
        {
            vm = new MainWindowViewModel(
            new SongService(context),
            new CategoryService(context),
            new PlaylistService(context),
            new ArtistService(context),
            new UserService(context));
            DataContext = vm;
            InitializeComponent();
        }
    }
}
