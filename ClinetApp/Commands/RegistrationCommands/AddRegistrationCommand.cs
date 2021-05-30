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
    public class AddRegistrationCommand : ICommand
    {
        private RegistrationViewModel receiver;
        private RegistrationService _RegistrationService;

        public AddRegistrationCommand(RegistrationViewModel receiver, RegistrationService chronoService)
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
                r.Invoice = null;
                r.Race = null;
                _RegistrationService.Insert(r);
                receiver.LoadAll();
                MessageBox.Show("Registration added");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
