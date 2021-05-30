using Client.Commands.ResultCommands;
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
    public class ResultViewModel :INotifyPropertyChanged, IViewModelBase
    {
        #region constructor
        public ResultViewModel(ResultsService resultService, RegistrationService _regService)
        {
            ResultService = resultService;
            RegistrationService = _regService;
            AddResultCommand = new AddResultCommand(this, ResultService);
            UpdateResultCommand = new UpdateResultCommand(this, ResultService);
            DeleteResultCommand = new DeleteResultCommand(this, ResultService);
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
        public ResultsService ResultService { get; set; }
        public RegistrationService RegistrationService { get; set; }

        #endregion

        #region commands
        public ICommand AddResultCommand { get; set; }
        public ICommand UpdateResultCommand { get; set; }
        public ICommand DeleteResultCommand { get; set; }

        #endregion

        #region binding 
        public Result result = new Result();
        public Result Result
        {
            get
            {
                return result;
            }
            set
            {
                if (value == null)
                    return;
                result = value;
                OnPropertyChanged("Result");
            }
        }

        public Result selected = new Result();


        public Result Selected
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
                SelectedRegistration = Registrations.SingleOrDefault(x => x.Race_raceID == value.Registration.Race_raceID
                                                                    && x.Runner_email == value.Registration.Runner_email);
                Registration r = selected.Registration;
                selected.Registration = null;
                Result = CopyHelper.DeepCopy<Result>(selected);
                Result.Registration = r;
                selected.Registration = r;

                OnPropertyChanged("Selected");
            }
        }


        private BindingList<Result> results;
        public BindingList<Result> AllResults
        {
            get
            {
                return results;
            }
            set
            {
                results = value;
                OnPropertyChanged("AllResults");
            }
        }


        private BindingList<Registration> registrations;
        public BindingList<Registration> Registrations
        {
            get
            {
                return registrations;
            }
            set
            {
                registrations = value;
                OnPropertyChanged("Registrations");
            }
        }

        private Registration selectedRegistration;
        public Registration SelectedRegistration
        {
            get
            {
                return selectedRegistration;
            }
            set
            {
                if (value == null)
                    return;
                selectedRegistration = value;
                Result.Registration = value;


                OnPropertyChanged("SelectedRegistration");
            }
        }


        #endregion

        public void LoadAll()
        {
            AllResults = new BindingList<Result>(ResultService.GetAll());

            Registrations = new BindingList<Registration>();
            foreach (Registration o in RegistrationService.GetAll())
                Registrations.Add(o);
        }
    }
}

