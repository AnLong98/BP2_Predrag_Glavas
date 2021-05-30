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
    public class GetRunnerExpensesCommand :ICommand
    {
        private RunnersService _runnerService;

        public GetRunnerExpensesCommand(RunnersService chronoService)
        {
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
                double expense = _runnerService.GetRunnerExpenses(r);
                MessageBox.Show($"Runner's total expenses are {expense}");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
