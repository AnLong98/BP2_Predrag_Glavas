using Service.Exceptions;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class InvoicesService
    {

        private readonly RegistrationService _registrationService;

        public InvoicesService(RegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        public void Delete(Invoice entity)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                Invoice invoice = _dbContext.Invoices.Find(entity.invoiceCode);
                if (invoice == null)
                    throw new EntityDoesNotExistException($"Invoice with code {entity.invoiceCode} does not exist");
                _dbContext.Invoices.Remove(invoice);
                _dbContext.SaveChanges();
            }
        }

        public List<Invoice> GetAll()
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Invoices.Include("Registration").Include("Payments").ToList();
            }
        }

        public Invoice GetByKey(Guid key)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Invoices.Include("Registration").Include("Payments").SingleOrDefault(x => x.invoiceCode == key);
            }
        }

        public Invoice Insert(Invoice entity)
        {
            ValidateInvoice(entity);
            if (GetByKey(entity.invoiceCode) != null)
                throw new DuplicateEntityException($"Invoice with code {entity.invoiceCode} already exists");
            using (var _dbContext = new RaceCenterDbContext())
            {
                entity.Registration = _dbContext.Registrations.SingleOrDefault(x => x.Race_raceID == entity.Registration.Race_raceID && x.Runner_email == entity.Registration.Runner_email);
                Invoice newInvoice = _dbContext.Invoices.Add(entity);
                _dbContext.SaveChanges();
                return newInvoice;
            }
        }

        private void ValidateInvoice(Invoice entity)
        {
            if (entity.invoiceCode == null)
                throw new InvalidModelException($"Invoice code cannot be empty.");
            Registration reg = _registrationService.GetByKey(entity.Registration.Runner_email, entity.Registration.Race_raceID);
            if (entity.Registration == null || reg == null)
                throw new InvalidModelException($"Invoice code must be tied to a valid registration");
            if(reg.Invoice != null)
                throw new InvalidModelException($"Invoice already exists for this registration");


        }

        public Invoice Update(Invoice entity)
        {
            ValidateInvoice(entity);
            using (var _dbContext = new RaceCenterDbContext())
            {
                Invoice invoice = _dbContext.Invoices.Include("Registration").Include("Payments").SingleOrDefault(x => x.invoiceCode == entity.invoiceCode);
                if (invoice == null)
                    throw new EntityDoesNotExistException($"Invoice with code {entity.invoiceCode} does not exist");
                invoice.Registration = entity.Registration;
                invoice.Payments = entity.Payments;

                _dbContext.SaveChanges();

                return invoice;
            }
        }
    }
}
