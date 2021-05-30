using Gui.ViewModel;
using Service.Model;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Gui.Commands
{
    public class AddChronometerCommand : ICommand
    {
        private ChronometerViewModel receiver;
        private ChronometersService _chronoService;

        public AddChronometerCommand(ChronometerViewModel receiver, ChronometersService chronoService)
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
            Chronometer chronometer = parameter as Chronometer;
            try
            {
                _chronoService.Insert(chronometer);
                receiver.LoadAll();
                MessageBox.Show("Chronometer added");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }
    }
}
