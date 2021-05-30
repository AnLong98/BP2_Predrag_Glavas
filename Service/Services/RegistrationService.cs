using Service.Exceptions;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class RegistrationService
    {
        private readonly RaceService _raceService;
        private readonly RunnersService _runnersService;

        public RegistrationService(RaceService raceService, RunnersService runnersService)
        {
            _raceService = raceService;
            this._runnersService = runnersService;
        }

        public double GetRegistrationPayments(Registration reg)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                var totalSum =  _dbContext.GetTotalRegistrationPayments(reg.Runner_email, reg.Race_raceID).FirstOrDefault();
                if (totalSum == null)
                    return 0;
                return (double)totalSum.Value;
            }
        }

        public void Delete(Registration entity)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                Registration registration = _dbContext.Registrations.Find(entity.Runner_email, entity.Race_raceID);
                if (registration == null)
                    throw new EntityDoesNotExistException($"Registration  does not exist");
                _dbContext.Registrations.Remove(registration);
                _dbContext.SaveChanges();
            }
        }

        public List<Registration> GetAll()
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Registrations.Include("Race")
                                               .Include("Runner")
                                               .Include("Invoice")
                                               .Include("Result").ToList();
            }
        }

        public Registration GetByKey(string runnerEmail, int raceId)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Registrations.Include("Race")
                                               .Include("Runner")
                                               .Include("Invoice")
                                               .Include("Result")
                                               .SingleOrDefault( x=> x.Runner_email == runnerEmail && x.Race_raceID ==  raceId);
            }
        }

        public Registration Insert(Registration entity)
        {
            ValidateRegistration(entity);
            if (GetByKey(entity.Runner_email, entity.Race_raceID) != null)
                throw new DuplicateEntityException($"Registration for {entity.Runner_email} and {entity.Race_raceID} already exists");
            using (var _dbContext = new RaceCenterDbContext())
            {
                Registration newRegistration = _dbContext.Registrations.Add(entity);
                _dbContext.SaveChanges();
                return newRegistration;
            }
        }

        private void ValidateRegistration(Registration entity)
        {
            if(_runnersService.GetByKey(entity.Runner_email) == null)
                throw new EntityDoesNotExistException($"Runnner for this registration don't exist");
            if(_raceService.GetByKey(entity.Race_raceID) == null)
                throw new EntityDoesNotExistException($"Race for this registration don't exist");
            Race r = _raceService.GetByKey(entity.Race_raceID);

            if (entity.startNumber <  r.minStartNumber || entity.startNumber > r.maxStartNumber)
                throw new InvalidModelException($"Start number is out of bounds for race.");
        }

        public Registration Update(Registration entity)
        {
            ValidateRegistration(entity);
            using (var _dbContext = new RaceCenterDbContext())
            {
                Registration registration = _dbContext.Registrations.Include("Race")
                                               .Include("Runner")
                                               .Include("Invoice")
                                               .Include("Result")
                                               .SingleOrDefault(x => x.Runner_email == entity.Runner_email && x.Race_raceID == x.Race_raceID);
                if (registration == null)
                    throw new EntityDoesNotExistException($"Registration  does not exist");
                registration.startNumber = entity.startNumber;

                _dbContext.SaveChanges();

                return registration;
            }
        }
    }
}
