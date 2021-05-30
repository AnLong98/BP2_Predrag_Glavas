using Gui.ViewModel;
using Service.Model;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.Commands.ChronometerCommands
{
    public class ChangeSelectedChronometerCommand : ICommand
    {
        private ChronometerViewModel receiver;
        private ChronometersService _chronoService;

        public ChangeSelectedChronometerCommand(ChronometerViewModel receiver, ChronometersService chronoService)
        {
            this.receiver = receiver;
            _chronoService = chronoService;
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
            receiver.SelectedChanged(parameter as Chronometer);

        }
    }
}
