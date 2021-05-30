using Gui.ViewModel;
using Service.Model;
using Service.Services;
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

namespace Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            RaceCenterDbContext _db = new RaceCenterDbContext();
            UsersService _users = new UsersService(_db);
            ChronometersService _chrono = new ChronometersService(_db, _users);
            MainViewModel mainViewModel = new MainViewModel(_chrono);
            DataContext = mainViewModel;
            InitializeComponent();
        }
    }
}
