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
    public class GetEventChronometersCommand :ICommand
    {
        private EventService _eventService;

        public GetEventChronometersCommand( EventService eventService)
        {
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
                var message = "";
                foreach(var chrono in _eventService.GetEventChronometers(r))
                {
                    message = message + chrono + "\n";
                }
                MessageBox.Show($"Event chronometers are: {message}");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
