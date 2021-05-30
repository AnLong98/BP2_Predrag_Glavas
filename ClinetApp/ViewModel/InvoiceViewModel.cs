using Client.Commands.InvoiceCommands;
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
    public class InvoiceViewModel :INotifyPropertyChanged, IViewModelBase
    {
        #region constructor
        public InvoiceViewModel(InvoicesService invoiceService, RegistrationService _regService)
        {
            InvoiceService = invoiceService;
            RegistrationService = _regService;
            AddInvoiceCommand = new AddInvoiceCommand(this, InvoiceService);
            UpdateInvoiceCommand = new UpdateInvoiceCommand(this, InvoiceService);
            DeleteInvoiceCommand = new DeleteInvoiceCommand(this, InvoiceService);
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
        public InvoicesService InvoiceService { get; set; }
        public RegistrationService RegistrationService { get; set; }

        #endregion

        #region commands
        public ICommand AddInvoiceCommand { get; set; }
        public ICommand UpdateInvoiceCommand { get; set; }
        public ICommand DeleteInvoiceCommand { get; set; }

        #endregion

        #region binding 
        public Invoice invoice = new Invoice();
        public Invoice Invoice
        {
            get
            {
                return invoice;
            }
            set
            {
                if (value == null)
                    return;
                invoice = value;
                OnPropertyChanged("Invoice");
            }
        }

        public Invoice selected = new Invoice();


        public Invoice Selected
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
                selected.Payments = null;
                Invoice = CopyHelper.DeepCopy<Invoice>(selected);
                Invoice.Registration = r;
                selected.Registration = r;

                OnPropertyChanged("Selected");
            }
        }


        private BindingList<Invoice> invoices;
        public BindingList<Invoice> AllInvoices
        {
            get
            {
                return invoices;
            }
            set
            {
                invoices = value;
                OnPropertyChanged("AllInvoices");
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
                Invoice.Registration = value;
                

                OnPropertyChanged("SelectedRegistration");
            }
        }


        #endregion

        public void LoadAll()
        {
            AllInvoices = new BindingList<Invoice>(InvoiceService.GetAll());

            Registrations = new BindingList<Registration>();
            foreach (Registration o in RegistrationService.GetAll())
                Registrations.Add(o);
        }
    }
}
