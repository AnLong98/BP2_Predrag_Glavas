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

namespace Client.Commands.OrganizerCommands
{
    public class DeleteOrganizerCommand : ICommand
    {
        private OrganizerViewModel receiver;
        private OrganizersService _runnerService;

        public DeleteOrganizerCommand(OrganizerViewModel receiver, OrganizersService organizerService)
        {
            this.receiver = receiver;
            _runnerService = organizerService;
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
            Organizer r = parameter as Organizer;
            try
            {
                _runnerService.Delete(r);
                receiver.LoadAll();
                MessageBox.Show("Organizer deleted");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
