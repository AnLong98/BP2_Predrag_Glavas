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

namespace Client.Commands.RunnerCommands
{
    public class AddRunnerCommand : ICommand
    {
        private RunnerViewModel receiver;
        private RunnersService _runnerService;

        public AddRunnerCommand(RunnerViewModel receiver, RunnersService chronoService)
        {
            this.receiver = receiver;
            _runnerService = chronoService;
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
            Runner r = parameter as Runner;
            try
            {
                _runnerService.Insert(r);
                receiver.LoadAll();
                MessageBox.Show("Runner added");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
