using Client.Contracts;
using Client.ViewModel;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;

namespace Gui.ViewModel
{
    public class MainViewModel :INotifyPropertyChanged
    {
        ObservableCollection<object> _children;

        public MainViewModel(ChronometersService _chrono,
            RunnersService _runners,
            OrganizersService _organizer,
            EventService _events,
            RaceService _race,
            RegistrationService _regs,
            InvoicesService _invoices,
            PaymentsService _payments,
            ResultsService _results)
        {
            _children = new ObservableCollection<object>();
            _children.Add(new ChronometerViewModel(_chrono));
            _children.Add(new RunnerViewModel(_runners));
            _children.Add(new OrganizerViewModel(_organizer));
            _children.Add(new EventViewModel(_events, _organizer));
            _children.Add(new RaceViewModel(_race, _events, _chrono));
            _children.Add(new RegistrationViewModel(_regs, _runners, _race));
            _children.Add(new InvoiceViewModel(_invoices, _regs));
            _children.Add(new PaymentViewModel(_payments, _invoices));
            _children.Add(new ResultViewModel(_results, _regs));
        }


        #region property changed
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public int tab = 0;
        public int Tab
        {
            get
            {
                return tab;
            }
            set
            {

                tab = value;
                foreach(object o in _children)
                {
                    (o as IViewModelBase).LoadAll();
                }
                OnPropertyChanged("Tab");
            }
        }

        public ObservableCollection<object> Children { get { return _children; } }
    }
}
