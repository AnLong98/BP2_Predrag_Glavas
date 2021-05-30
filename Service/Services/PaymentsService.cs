using Service.Exceptions;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PaymentsService
    {
        private readonly InvoicesService _invoicesService;

        public PaymentsService( InvoicesService invoicesService)
        {
            _invoicesService = invoicesService;
        }


        public void Delete(Payment entity)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                Payment payment = _dbContext.Payments.Find(entity.paymentID);
                if (payment == null)
                    throw new EntityDoesNotExistException($"Payment with id {entity.paymentID} does not exist");
                _dbContext.Payments.Remove(payment);
                _dbContext.SaveChanges();
            }
        }

        public List<Payment> GetAll()
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Payments.Include("Invoice").ToList();
            }
        }

        public Payment GetByKey(int key)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Payments.Include("Invoice").SingleOrDefault( x=> x.paymentID == key);
            }
        }

        public Payment Insert(Payment entity)
        {
            ValidatePayment(entity);
            if (GetByKey(entity.paymentID) != null)
                throw new DuplicateEntityException($"Payment with id {entity.paymentID} already exists");
            using (var _dbContext = new RaceCenterDbContext())
            {
                Payment newPayment = _dbContext.Payments.Add(entity);
                _dbContext.SaveChanges();
                return newPayment;
            }
        }

        private void ValidatePayment(Payment entity)
        {
            if (entity.moneyPaid <= 0)
                throw new InvalidModelException($"Payment cannot be entered for zero or negative values.");

            if (entity.type != "SLIP" && entity.type != "CARD" && entity.type != "PAYPAL")
                throw new InvalidModelException($"Payment method {entity.type} is not allowed.Allowed payment methods are SLIP, PAYPAL and CARD");
            if(_invoicesService.GetByKey(entity.Invoice_invoiceCode) == null)
                throw new InvalidModelException($"Cannot attach payment to non existing invoice");
        }

        public Payment Update(Payment entity)
        {
            ValidatePayment(entity);
            using (var _dbContext = new RaceCenterDbContext())
            {
                Payment payment = _dbContext.Payments.Include("Invoice").SingleOrDefault(x => x.paymentID == entity.paymentID);
                if (payment == null)
                    throw new EntityDoesNotExistException($"Payment with ID {entity.paymentID} does not exist");
                payment.moneyPaid = entity.moneyPaid;
                payment.type = entity.type;

                _dbContext.SaveChanges();

                return payment;
            }
        }
    }
}
