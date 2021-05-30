using Client.Commands;
using Client.Commands.ChronometerCommands;
using Client.Contracts;
using Client.Helpers;
using Gui.Commands;
using Service.Model;
using Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace Gui.ViewModel
{
    public class ChronometerViewModel: INotifyPropertyChanged, IViewModelBase
    {
        #region constructor
        public ChronometerViewModel(ChronometersService chronometerService)
        {
            ChronometerService = chronometerService;
            AddChronometerCommand = new AddChronometerCommand(this, chronometerService);
            UpdateChronometerCommand = new UpdateChronometerCommand(this, chronometerService);
            DeleteChronometerCommand = new DeleteChronometerCommand(this, chronometerService);
            ChangeSelectedChronometerCommand = new ChangeSelectedChronometerCommand(this, chronometerService);
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
        public ChronometersService ChronometerService { get; set; }

        internal void SelectedChanged(Chronometer chronometer)
        {
            Chronometer = chronometer;
        }
        #endregion

        #region commands
        public ICommand AddChronometerCommand { get; set; }
        public ICommand UpdateChronometerCommand { get; set; }
        public ICommand DeleteChronometerCommand { get; set; }

        public ICommand ChangeSelectedChronometerCommand { get; set; }
        #endregion

        #region binding 
        public Chronometer chronometer =  new Chronometer();
        public Chronometer Chronometer
        {
            get
            {
                return chronometer;
            }
            set
            {
                if (value == null)
                    return;
                chronometer = value;
                OnPropertyChanged("Chronometer");
            }
        }

        public Chronometer selected = new Chronometer();
        public Chronometer Selected
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
                selected.Races = null;
                Chronometer = CopyHelper.DeepCopy<Chronometer>(selected);
                OnPropertyChanged("Selected");
            }
        }


        private BindingList<Chronometer> chronometers;
        public BindingList<Chronometer> AllChronometers
        {
            get
            {
                return chronometers;
            }
            set
            {
                chronometers = value;
                OnPropertyChanged("AllChronometers");
            }
        }
        #endregion

        public void LoadAll()
        {
            AllChronometers = new BindingList<Chronometer>(ChronometerService.GetAll());
        }
    }
}
