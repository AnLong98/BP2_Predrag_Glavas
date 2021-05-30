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
    public class DeletePaymentCommand : ICommand
    {
        private PaymentViewModel receiver;
        private PaymentsService paymentService;

        public DeletePaymentCommand(PaymentViewModel receiver, PaymentsService PaymentService)
        {
            this.receiver = receiver;
            paymentService = PaymentService;
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
                paymentService.Delete(r);
                receiver.LoadAll();
                MessageBox.Show("Payment deleted");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
