using Gui.ViewModel;
using Service.Model;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            UsersService _users = new UsersService();
            OrganizersService _organizers = new OrganizersService( _users);
            RunnersService _runners = new RunnersService( _users);
            ChronometersService _chrono = new ChronometersService( _users);
            EventService _events = new EventService(_organizers);
            RaceService _race = new RaceService(_chrono, _events);
            RegistrationService _regs = new RegistrationService(_race, _runners);
            InvoicesService _invoice = new InvoicesService(_regs);
            PaymentsService _payments = new PaymentsService(_invoice);
            ResultsService _results = new ResultsService(_regs);
            MainViewModel mainViewModel = new MainViewModel(_chrono, _runners, _organizers, _events, _race, _regs, _invoice, _payments, _results);
            DataContext = mainViewModel;
            InitializeComponent();
        }






































        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
