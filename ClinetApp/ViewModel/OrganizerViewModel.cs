using Client.Commands.OrganizerCommands;
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
    public class OrganizerViewModel : INotifyPropertyChanged, IViewModelBase
    {
        #region constructor
        public OrganizerViewModel(OrganizersService organizerService)
        {
            OrganizerService = organizerService;
            AddOrganizerCommand = new AddOrganizerCommand(this, organizerService);
            UpdateOrganizerCommand = new UpdateOrganizerCommand(this, organizerService);
            DeleteOrganizerCommand = new DeleteOrganizerCommand(this, organizerService);
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
        public OrganizersService OrganizerService { get; set; }

        internal void SelectedChanged(Organizer organizer)
        {
            Organizer = organizer;
        }
        #endregion

        #region commands
        public ICommand AddOrganizerCommand { get; set; }
        public ICommand UpdateOrganizerCommand { get; set; }
        public ICommand DeleteOrganizerCommand { get; set; }

        public ICommand ChangeSelectedOrganizerCommand { get; set; }
        #endregion

        #region binding 
        public Organizer organizer = new Organizer();
        public Organizer Organizer
        {
            get
            {
                return organizer;
            }
            set
            {
                if (value == null)
                    return;
                organizer = value;
                OnPropertyChanged("Organizer");
            }
        }

        public Organizer selected = new Organizer();
        public Organizer Selected
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
                selected.Supervisor = null;
                selected.Employees = null;
                selected.Events = null;
                Organizer = CopyHelper.DeepCopy<Organizer>(selected);
                SelectedSupervisor = selected.supervisorEmail;
                OnPropertyChanged("Selected");
            }
        }


        private BindingList<Organizer> organizers;
        public BindingList<Organizer> AllOrganizers
        {
            get
            {
                return organizers;
            }
            set
            {
                organizers = value;
                OnPropertyChanged("AllOrganizers");
            }
        }

        private BindingList<string> supervisors;
        public BindingList<string> Supervisors
        {
            get
            {
                return supervisors;
            }
            set
            {
                supervisors = value;
                OnPropertyChanged("Supervisors");
            }
        }

        private string selectedSupervisor;
        public string SelectedSupervisor
        {
            get
            {
                return selectedSupervisor;
            }
            set
            {
                if(value != null)
                {
                    selectedSupervisor = string.Copy(value);
                    Organizer.supervisorEmail = string.Copy(value);
                }
                else
                {
                    selectedSupervisor = value;
                    Organizer.supervisorEmail = value;
                }

                OnPropertyChanged("SelectedSupervisor");
            }
        }

        #endregion

        public void LoadAll()
        {
            AllOrganizers = new BindingList<Organizer>(OrganizerService.GetAll());
            Supervisors = new BindingList<string>();
            Supervisors.Add("");
            foreach (Organizer o in AllOrganizers)
                Supervisors.Add(o.email);
        }
    }
}
