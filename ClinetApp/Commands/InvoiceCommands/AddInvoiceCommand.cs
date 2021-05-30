using Client.ViewModel;
using Service.Model;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Client.Commands.InvoiceCommands
{
    public class AddInvoiceCommand : ICommand
    {
        private InvoiceViewModel receiver;
        private InvoicesService _InvoiceService;

        public AddInvoiceCommand(InvoiceViewModel receiver, InvoicesService InvoiceService)
        {
            this.receiver = receiver;
            _InvoiceService = InvoiceService;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return parameter != null;
        }

        public void Execute(object parameter)
        {
            Invoice r = parameter as Invoice;
            try
            {
                r.invoiceCode = Guid.NewGuid();
                r.Payments = null;
                _InvoiceService.Insert(r);
                receiver.LoadAll();
                MessageBox.Show("Invoice added");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
