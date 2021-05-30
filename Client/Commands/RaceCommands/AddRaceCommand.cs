using Client.ViewModel;
using Service.Model;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.Commands.RaceCommands
{
    public class AddRaceCommand : ICommand
    {
        private RaceViewModel receiver;
        private RaceService _raceService;

        public AddRaceCommand(RaceViewModel receiver, RaceService chronoService)
        {
            this.receiver = receiver;
            _raceService = chronoService;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return parameter != null;
        }

        public void Execute(object parameter)
        {
            Race r = parameter as Race;
            try
            {
                r.Event = null;
                r.Chronometer = null;
                r.raceID = 0;
                _raceService.Insert(r);
                receiver.LoadAll();
                MessageBox.Show("Race added");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
