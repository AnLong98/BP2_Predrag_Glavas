using Service.Exceptions;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ResultsService
    {
        private readonly RegistrationService _registrationService;

        public ResultsService(RegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        public void Delete(Result entity)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                Result result = _dbContext.Results.Find(entity.tagID);
                if (result == null)
                    throw new EntityDoesNotExistException($"Result with id {entity.tagID} does not exist.");
                _dbContext.Results.Remove(result);
                _dbContext.SaveChanges();
            }
        }

        public List<Result> GetAll()
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Results.Include("Registration").ToList();
            }
        }

        public Result GetByKey(int key)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Results.Include("Registration").SingleOrDefault(x => x.tagID == key);
            }
        }

        public Result Insert(Result entity)
        {
            ValidateResult(entity);
            if (GetByKey(entity.tagID) != null)
                throw new DuplicateEntityException($"Result for this id already exists");
            using (var _dbContext = new RaceCenterDbContext())
            {
                if (_dbContext.Results.Include("Registration").Where(x => x.Registration.Race_raceID == entity.Registration.Race_raceID && x.Registration.Runner_email == entity.Registration.Runner_email) != null)
                    throw new InvalidModelException($"Result already exists for this registration.");

            
                entity.Registration = _dbContext.Registrations.SingleOrDefault(x => x.Runner_email == entity.Registration.Runner_email
                && entity.Registration.Race_raceID == x.Race_raceID);
                Result newResult = _dbContext.Results.Add(entity);
                _dbContext.SaveChanges();
                return newResult;
            }
        }

        private void ValidateResult(Result entity)
        {
            if(entity.Registration == null || _registrationService.GetByKey( entity.Registration.Runner_email, entity.Registration.Race_raceID) == null)
                throw new InvalidModelException($"Result must be attached to a valid registration.");

            if(entity.hour <=0 && entity.minute <= 0 && entity.second <= 0)
                throw new InvalidModelException($"Result time must be non negative and non zero.");

            if ( entity.minute > 59 || entity.second > 59)
                throw new InvalidModelException($"Result minutes and seconds must be <= 59.");
        }

        public Result Update(Result entity)
        {
            ValidateResult(entity);
            using (var _dbContext = new RaceCenterDbContext())
            {
                Result result = _dbContext.Results.Include("Registration").SingleOrDefault(x => x.tagID == entity.tagID);
                if (result == null)
                    throw new DuplicateEntityException($"Result for this id already exists");
                result.hour = entity.hour;
                result.minute = entity.minute;
                result.second = entity.second;

                _dbContext.SaveChanges();

                return result;
            }
        }
    }
}
