using Client.Commands.EventCommands;
using Client.Contracts;
using Client.Helpers;
using Service.Model;
using Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModel
{
    public class EventViewModel : INotifyPropertyChanged, IViewModelBase
    {
        #region constructor
        public EventViewModel(EventService _eventService, OrganizersService _organizersService)
        {
            EventService = _eventService;
            OrganizersService = _organizersService;
            AddEventCommand = new AddEventCommand(this, _eventService);
            UpdateEventCommand = new UpdateEventCommand(this, _eventService);
            DeleteEventCommand = new DeleteEventCommand(this, _eventService);
            GetEventChronometersCommand = new GetEventChronometersCommand(_eventService);
            LoadAll();
        }
        #endregion

        #region property changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region get set
        public EventService EventService { get; set; }
        public OrganizersService OrganizersService { get; set; }

        #endregion

        #region commands
        public ICommand AddEventCommand { get; set; }
        public ICommand UpdateEventCommand { get; set; }
        public ICommand DeleteEventCommand { get; set; }
        public ICommand GetEventChronometersCommand { get; set; }

        #endregion

        #region binding 
        public Event _event = new Event();
        public Event Event
        {
            get
            {
                return _event;
            }
            set
            {
                if (value == null)
                    return;
                _event = value;
                OnPropertyChanged("Event");
            }
        }

        public Event selected = new Event();
        public Event Selected
        {
            get
            {
                return selected;
            }
            set
            {
                if (value == null)
                    return;
                selected = value;
                selected.Organizer = null;
                selected.Races = null;
                SelectedOrganizer = selected.Organizer_email;
                Event = CopyHelper.DeepCopy<Event>(selected);
                
                OnPropertyChanged("Selected");
            }
        }


        private BindingList<Event> _events;
        public BindingList<Event> AllEvents
        {
            get
            {
                return _events;
            }
            set
            {
                _events = value;
                OnPropertyChanged("AllEvents");
            }
        }

        private BindingList<string> supervisors;
        public BindingList<string> Organizers
        {
            get
            {
                return supervisors;
            }
            set
            {
                supervisors = value;
                OnPropertyChanged("Organizers");
            }
        }

        private string selectedOrganizer;
        public string SelectedOrganizer
        {
            get
            {
                return selectedOrganizer;
            }
            set
            {
                if (value != null)
                {
                    selectedOrganizer = string.Copy(value);
                    Event.Organizer_email= string.Copy(value);
                }
                else
                {
                    selectedOrganizer = value;
                    Event.Organizer_email = value;
                }

                OnPropertyChanged("SelectedOrganizer");
            }
        }

        #endregion

        public void LoadAll()
        {
            AllEvents = new BindingList<Event>(EventService.GetAll());
            Organizers = new BindingList<string>();
            Organizers.Add("");
            foreach (Organizer o in OrganizersService.GetAll())
                Organizers.Add(o.email);
        }
    }
}
