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
    public class ChronometerViewModel
    {
        #region constructor
        public ChronometerViewModel(ChronometersService chronometerService)
        {
            ChronometerService = chronometerService;
            AddChronometerCommand = new AddChronometerCommand(this, chronometerService);
            DeleteChronometerCommand = new DeleteChronometerCommand(this, chronometerService);
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

        internal void AddChronometer(Chronometer chronometer)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region commands
        public ICommand AddChronometerCommand { get; set; }
        public ICommand DeleteChronometerCommand { get; set; }
        #endregion

        #region binding 
        public Chronometer chronometer;
        public Chronometer Chronometer
        {
            get
            {
                return chronometer;
            }
            set
            {
               chronometer = value;
                OnPropertyChanged("Chronometer");
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

        private void LoadAll()
        {
            AllChronometers = new BindingList<Chronometer>(ChronometerService.GetAll());
        }
    }
}
