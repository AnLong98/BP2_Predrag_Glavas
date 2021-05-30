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
    public class UpdateRegistrationCommand : ICommand
    {
        private RegistrationViewModel receiver;
        private RegistrationService _registrationService;

        public UpdateRegistrationCommand(RegistrationViewModel receiver, RegistrationService chronoService)
        {
            this.receiver = receiver;
            _registrationService = chronoService;
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
                _registrationService.Update(r);
                receiver.LoadAll();
                MessageBox.Show("Registration updated");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
