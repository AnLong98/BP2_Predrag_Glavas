using Client.Commands.RegistrationCommands;
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
    public class RegistrationViewModel: INotifyPropertyChanged, IViewModelBase
    {
        #region constructor
        public RegistrationViewModel(RegistrationService registrationService, RunnersService _runnersS, RaceService _raceS)
        {
            RegistrationService = registrationService;
            RunnersService= _runnersS;
            RaceService = _raceS;
            AddRegistrationCommand = new AddRegistrationCommand(this, RegistrationService);
            UpdateRegistrationCommand = new UpdateRegistrationCommand(this, RegistrationService);
            DeleteRegistrationCommand = new DeleteRegistrationCommand(this, RegistrationService);
            GetRegPaymentsCommand = new GetRegistrationPayments(registrationService);
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
        public RegistrationService RegistrationService { get; set; }
        public RaceService RaceService { get; set; }
        public RunnersService RunnersService { get; set; }

        #endregion

        #region commands
        public ICommand AddRegistrationCommand { get; set; }
        public ICommand UpdateRegistrationCommand { get; set; }
        public ICommand DeleteRegistrationCommand { get; set; }
        public ICommand GetRegPaymentsCommand { get; set; }

        #endregion

        #region binding 
        public Registration registration = new Registration();
        public Registration Registration
        {
            get
            {
                return registration;
            }
            set
            {
                if (value == null)
                    return;
                registration = value;
                OnPropertyChanged("Registration");
            }
        }

        public Registration selected = new Registration();


        public Registration Selected
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
                selected.Runner = null;
                selected.Result = null;
                selected.Race = null;
                selected.Invoice = null;
                SelectedRunner= value.Runner_email;
                SelectedRace = Races.SingleOrDefault(x => x.raceID == value.Race_raceID);
                Registration = CopyHelper.DeepCopy<Registration>(selected);

                OnPropertyChanged("Selected");
            }
        }


        private BindingList<Registration> registrations;
        public BindingList<Registration> AllRegistrations
        {
            get
            {
                return registrations;
            }
            set
            {
                registrations = value;
                OnPropertyChanged("AllRegistrations");
            }
        }


        private BindingList<string> runners;
        public BindingList<string> Runners
        {
            get
            {
                return runners;
            }
            set
            {
                runners = value;
                OnPropertyChanged("Runners");
            }
        }

        private BindingList<Race> races;
        public BindingList<Race> Races
        {
            get
            {
                return races;
            }
            set
            {
                races = value;
                OnPropertyChanged("Races");
            }
        }

        private string selectedRunner;
        public string SelectedRunner
        {
            get
            {
                return selectedRunner;
            }
            set
            {
                if (value != null)
                {
                    selectedRunner = string.Copy(value);
                    Registration.Runner_email= string.Copy(value);
                }
                else
                {
                    selectedRunner = value;
                    Registration.Runner_email = value;
                }

                OnPropertyChanged("SelectedRunner");
            }
        }

        private Race selectedRace;
        public Race SelectedRace
        {
            get
            {
                return selectedRace;
            }
            set
            {
                if (value == null)
                    return;
                selectedRace = value;
                Registration.Race_raceID = value.raceID;


                OnPropertyChanged("SelectedRace");
            }
        }

        #endregion

        public void LoadAll()
        {
            AllRegistrations = new BindingList<Registration>(RegistrationService.GetAll());
            Runners = new BindingList<string>();
            Runners.Add("");
            foreach (Runner r in RunnersService.GetAll())
                Runners.Add(r.email);

            Races = new BindingList<Race>();
            foreach (Race o in RaceService.GetAll())
                Races.Add(o);
        }
    }
}
