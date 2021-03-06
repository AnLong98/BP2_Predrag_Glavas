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

namespace Client.Commands.ResultCommands
{
    public class DeleteResultCommand : ICommand
    {
        private ResultViewModel receiver;
        private ResultsService _ResultService;

        public DeleteResultCommand(ResultViewModel receiver, ResultsService chronoService)
        {
            this.receiver = receiver;
            _ResultService = chronoService;
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
            Result r = parameter as Result;
            try
            { 
                _ResultService.Delete(r);
                receiver.LoadAll();
                MessageBox.Show("Result deleted");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
