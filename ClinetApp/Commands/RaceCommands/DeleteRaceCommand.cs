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
    public class DeleteRaceCommand : ICommand
    {
        private RaceViewModel receiver;
        private RaceService _RaceService;

        public DeleteRaceCommand(RaceViewModel receiver, RaceService chronoService)
        {
            this.receiver = receiver;
            _RaceService = chronoService;
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
                _RaceService.Delete(r);
                receiver.LoadAll();
                MessageBox.Show("Race deleted");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
