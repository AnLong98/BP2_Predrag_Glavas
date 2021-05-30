using Client.Commands.PaymentCommands;
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
    public class PaymentViewModel :INotifyPropertyChanged, IViewModelBase
    {
        #region constructor
        public PaymentViewModel(PaymentsService paymentService, InvoicesService _inv)
        {
            PaymentService = paymentService;
            InvoicesService = _inv;
            AddPaymentCommand = new AddPaymentCommand(this, PaymentService);
            UpdatePaymentCommand = new UpdatePaymentCommand(this, PaymentService);
            DeletePaymentCommand = new DeletePaymentCommand(this, PaymentService);
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
        public PaymentsService PaymentService { get; set; }
        public InvoicesService InvoicesService { get; set; }

        #endregion

        #region commands
        public ICommand AddPaymentCommand { get; set; }
        public ICommand UpdatePaymentCommand { get; set; }
        public ICommand DeletePaymentCommand { get; set; }

        #endregion

        #region binding 
        public Payment payment = new Payment() {
            type = "SLIP"
        };

        public Payment Payment
        {
            get
            {
                return payment;
            }
            set
            {
                if (value == null)
                    return;
                payment = value;
                OnPropertyChanged("Payment");
            }
        }

        public Payment selected = new Payment();


        public Payment Selected
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
                SelectedInvoice = value.Invoice_invoiceCode;
                selected.Invoice = null;
                Payment = CopyHelper.DeepCopy<Payment>(selected);

                OnPropertyChanged("Selected");
            }
        }

        private BindingList<string> paymentTypes = new BindingList<string>() {
            "SLIP",
            "PAYPAL",
            "CARD",
        };

        public BindingList<string> PaymentTypes
        {
            get
            {
                return paymentTypes;
            }
            set
            {
                paymentTypes = value;
                OnPropertyChanged("PaymentTypes");
            }
        }


        private BindingList<Payment> payments;
        public BindingList<Payment> AllPayments
        {
            get
            {
                return payments;
            }
            set
            {
                payments = value;
                OnPropertyChanged("AllPayments");
            }
        }


        private BindingList<Guid> invoices;
        public BindingList<Guid> Invoices
        {
            get
            {
                return invoices;
            }
            set
            {
                invoices = value;
                OnPropertyChanged("Invoices");
            }
        }

        private Guid selectedInvoice;
        public Guid SelectedInvoice
        {
            get
            {
                return selectedInvoice;
            }
            set
            {
                if (value == null)
                    return;
                selectedInvoice = value;
                Payment.Invoice_invoiceCode = value;


                OnPropertyChanged("SelectedInvoice");
            }
        }


        #endregion

        public void LoadAll()
        {
            AllPayments = new BindingList<Payment>(PaymentService.GetAll());

            Invoices = new BindingList<Guid>();
            foreach (Invoice o in InvoicesService.GetAll())
                Invoices.Add(o.invoiceCode);
        }
    }
}
