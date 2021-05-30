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
    public class DeleteInvoiceCommand : ICommand
    {
        private InvoiceViewModel receiver;
        private InvoicesService _runnerService;

        public DeleteInvoiceCommand(InvoiceViewModel receiver, InvoicesService InvoiceService)
        {
            this.receiver = receiver;
            _runnerService = InvoiceService;
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
                _runnerService.Delete(r);
                receiver.LoadAll();
                MessageBox.Show("Invoice deleted");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
