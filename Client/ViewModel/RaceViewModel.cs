using Client.Commands.RaceCommands;
using Client.Contracts;
using Client.Helpers;
using Service.Model;
using Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModel
{
    public class RaceViewModel :INotifyPropertyChanged, IViewModelBase
    {
        #region constructor

        public RaceViewModel(RaceService raceService, EventService eventService, ChronometersService chronometersService)
        {
            RaceService = raceService;
            EventService = eventService;
            ChronometersService = chronometersService;
            AddRaceCommand = new AddRaceCommand(this, RaceService);
            UpdateRaceCommand = new UpdateRaceCommand(this, RaceService);
            DeleteRaceCommand = new DeleteRaceCommand(this, RaceService);
            ConsolidateRaceCommand = new ConsolidateRaceCommand(this, RaceService);
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
        public RaceService RaceService { get; set; }
        public EventService EventService { get; set; }
        public ChronometersService ChronometersService { get; set; }

        #endregion

        #region commands
        public ICommand AddRaceCommand { get; set; }
        public ICommand UpdateRaceCommand { get; set; }
        public ICommand DeleteRaceCommand { get; set; }
        public ICommand ConsolidateRaceCommand { get; set; }

        #endregion

        #region binding 
        public Race _event = new Race()
        {
            date = DateTime.Now,
            type = "MARATHON"
        };
        public Race Race
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
                OnPropertyChanged("Race");
            }
        }

        public Race selected = new Race();
      

        public Race Selected
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
                selected.Chronometer = null;
                selected.Event = null;
                selected.Registrations = null;
                SelectedChronometer = value.Chronometer_email;
                SelectedEvent = Events.SingleOrDefault(x => x.eventID == value.Event_eventID);
                Race = CopyHelper.DeepCopy<Race>(selected);

                OnPropertyChanged("Selected");
            }
        }


        private BindingList<Race> _events;
        public BindingList<Race> AllRaces
        {
            get
            {
                return _events;
            }
            set
            {
                _events = value;
                OnPropertyChanged("AllRaces");
            }
        }

        private BindingList<string> raceTypes = new BindingList<string>() {
            "MARATHON",
            "HALFMARATHON",
            "OBSTACLE",
            "RELAY"
        };

        public BindingList<string> RaceTypes
        {
            get
            {
                return raceTypes;
            }
            set
            {
                raceTypes = value;
                OnPropertyChanged("RaceTypes");
            }
        }

        private BindingList<string> chronometers;
        public BindingList<string> Chronometers
        {
            get
            {
                return chronometers;
            }
            set
            {
                chronometers = value;
                OnPropertyChanged("Chronometers");
            }
        }

        private BindingList<Event> events;
        public BindingList<Event> Events
        {
            get
            {
                return events;
            }
            set
            {
                events = value;
                OnPropertyChanged("Events");
            }
        }

        private string selectedChronometer;
        public string SelectedChronometer
        {
            get
            {
                return selectedChronometer;
            }
            set
            {
                if (value != null)
                {
                    selectedChronometer = string.Copy(value);
                    Race.Chronometer_email = string.Copy(value);
                }
                else
                {
                    selectedChronometer = value;
                    Race.Chronometer_email = value;
                }

                OnPropertyChanged("SelectedChronometer");
            }
        }

        private Event selectedEvent;
        public Event SelectedEvent
        {
            get
            {
                return selectedEvent;
            }
            set
            {
                if (value == null)
                    return;
                selectedEvent = value;
                Race.Event= value;
                Race.Event_eventID = value.eventID;
                

                OnPropertyChanged("SelectedEvent");
            }
        }

        #endregion

        public void LoadAll()
        {
            AllRaces = new BindingList<Race>(RaceService.GetAll());
            Chronometers = new BindingList<string>();
            Chronometers.Add("");
            foreach (Chronometer o in ChronometersService.GetAll())
                Chronometers.Add(o.email);

            Events = new BindingList<Event>();
            foreach (Event o in EventService.GetAll())
                Events.Add(o);
        }
    }
}
