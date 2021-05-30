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

namespace Client.Commands.EventCommands
{
    public class AddEventCommand : ICommand
    {
        private EventViewModel receiver;
        private EventService _eventService;

        public AddEventCommand(EventViewModel receiver, EventService eventService)
        {
            this.receiver = receiver;
            _eventService = eventService;
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
            Event r = parameter as Event;
            try
            {
                r.eventID = 0;
                _eventService.Insert(r);
                receiver.LoadAll();
                MessageBox.Show("Event added");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
