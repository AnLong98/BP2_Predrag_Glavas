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

namespace Client.Commands.PaymentCommands
{
    public class AddPaymentCommand : ICommand
    {
        private PaymentViewModel receiver;
        private PaymentsService _PaymentService;

        public AddPaymentCommand(PaymentViewModel receiver, PaymentsService PaymentService)
        {
            this.receiver = receiver;
            _PaymentService = PaymentService;
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
            Payment r = parameter as Payment;
            try
            {
                r.paymentID = 0;
                r.Invoice = null;
                _PaymentService.Insert(r);
                receiver.LoadAll();
                MessageBox.Show("Payment added");
            }
            catch (Exception e)
            {
                var msg = e.Message;
                while(e.InnerException != null)
                {
                    msg = e.InnerException.Message;
                    e = e.InnerException;
                }

                MessageBox.Show(msg);
            }

        }
    }
}
