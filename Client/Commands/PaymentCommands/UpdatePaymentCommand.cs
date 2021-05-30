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
    public class UpdatePaymentCommand : ICommand
    {
        private PaymentViewModel receiver;
        private PaymentsService _PaymentService;

        public UpdatePaymentCommand(PaymentViewModel receiver, PaymentsService PaymentService)
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
                _PaymentService.Update(r);
                receiver.LoadAll();
                MessageBox.Show("Payment updated");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
    }
}
