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

namespace Client.Commands.RegistrationCommands
{
    public class GetRegistrationPayments :ICommand
    {
        private RegistrationService _RegistrationService;

        public GetRegistrationPayments(RegistrationService chronoService)
        {
            _RegistrationService = chronoService;
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
            Registration r = parameter as Registration;
            try
            {
                r.Invoice = null;
                r.Race = null;
                double sum = _RegistrationService.GetRegistrationPayments(r);
                MessageBox.Show($"Total paid money is {sum}");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}

