using Service.Exceptions;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class RunnersService
    {
        private readonly UsersService _usersService;

        public RunnersService(UsersService usersService)
        {
            _usersService = usersService;
        }

        public double GetRunnerExpenses(Runner r)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                ObjectParameter totalExpense =  new ObjectParameter("Total_Expense", typeof(decimal));
                _dbContext.GetRunnerExpenses(r.email, totalExpense);
                return Convert.ToDouble(totalExpense.Value);
            }
        }

        public void Delete(Runner entity)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                Runner user = _dbContext.Runners.Find(entity.email);
                if (user == null)
                    throw new EntityDoesNotExistException($"User with email {entity.email} does not exist");
                _dbContext.Runners.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        public List<Runner> GetAll()
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Runners.Include("Registrations").ToList();
            }
        }

        public Runner GetByKey(string key)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Runners.Find(key);
            }
        }

        public Runner Insert(Runner entity)
        {
            ValidateRunner(entity);
            if (_usersService.GetByKey(entity.email) != null)
                throw new DuplicateEntityException($"User with email {entity.email} already exists");
            using (var _dbContext = new RaceCenterDbContext())
            {
                Runner newRunner = _dbContext.Runners.Add(entity);
                _dbContext.SaveChanges();
                return newRunner;
            }
        }

        public Runner Update(Runner entity)
        {
            ValidateRunner(entity);
            using (var _dbContext = new RaceCenterDbContext())
            {
                Runner user = _dbContext.Runners.Find(entity.email);
                if (user == null)
                    throw new EntityDoesNotExistException($"User with email {entity.email} does not exist");
                user.address = entity.address;
                user.firstName = entity.firstName;
                user.lastName = entity.lastName;
                user.password = entity.password;
                user.club = entity.club;

                _dbContext.SaveChanges();

                return user;
            }
        }

        public void ValidateRunner(Runner user)
        {
            _usersService.ValidateUser(user);


        }
    }
}

