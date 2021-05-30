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
    public class DeleteRegistrationCommand : ICommand
    {
        private RegistrationViewModel receiver;
        private RegistrationService _RegistrationService;

        public DeleteRegistrationCommand(RegistrationViewModel receiver, RegistrationService chronoService)
        {
            this.receiver = receiver;
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
                _RegistrationService.Delete(r);
                receiver.LoadAll();
                MessageBox.Show("Registration deleted");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
