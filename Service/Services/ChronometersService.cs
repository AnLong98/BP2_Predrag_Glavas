using Service.Exceptions;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ChronometersService
    {
        private readonly UsersService _usersService;

        public ChronometersService(UsersService usersService)
        {
            _usersService = usersService;
        }

        public void Delete(Chronometer entity)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                Chronometer user = _dbContext.Chronometers.Find(entity.email);
                if (user == null)
                    throw new EntityDoesNotExistException($"User with email {entity.email} does not exist");
                _dbContext.Chronometers.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        public List<Chronometer> GetAll()
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Chronometers.Include("Races").ToList();
            }
        }

        public Chronometer GetByKey(string key)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Chronometers.Include("Races").SingleOrDefault(x => x.email == key);
            }
        }

        public Chronometer Insert(Chronometer entity)
        {
            ValidateChronometer(entity);
            if (_usersService.GetByKey(entity.email) != null)
                throw new DuplicateEntityException($"User with email {entity.email} already exists");
            using (var _dbContext = new RaceCenterDbContext())
            {
                Chronometer newChronometer = _dbContext.Chronometers.Add(entity);
                _dbContext.SaveChanges();
                return newChronometer;
            }
        }

        private void ValidateChronometer(Chronometer entity)
        {
            _usersService.ValidateUser(entity);
            if (string.IsNullOrEmpty(entity.companyName))
                throw new InvalidModelException($"Chronometer's company cannot be empty");
        }

        public Chronometer Update(Chronometer entity)
        {
            ValidateChronometer(entity);
            using (var _dbContext = new RaceCenterDbContext())
            {
                Chronometer user = _dbContext.Chronometers.Include("Races").SingleOrDefault(x => x.email == entity.email);
                if (user == null)
                    throw new EntityDoesNotExistException($"User with email {entity.email} does not exist");
                user.address = entity.address;
                user.firstName = entity.firstName;
                user.lastName = entity.lastName;
                user.password = entity.password;
                user.companyName = entity.companyName;

                _dbContext.SaveChanges();


                return user;
            }
        }
    }
}
