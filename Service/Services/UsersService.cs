using Service.Exceptions;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UsersService
    {
        public void Delete(User entity)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                User user = _dbContext.Users.Find(entity.email);
                if (user == null)
                    throw new EntityDoesNotExistException($"User with email {entity.email} does not exist");
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        public List<User> GetAll()
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Users.ToList();
            }
        }

        public User GetByKey(string key)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Users.Find(key);
            }
        }

        public User Insert(User entity)
        {
            ValidateUser(entity);
            if (GetByKey(entity.email) != null)
                throw new DuplicateEntityException($"User with email {entity.email} already exists");
            using (var _dbContext = new RaceCenterDbContext())
            {
                User newUser = _dbContext.Users.Add(entity);
                _dbContext.SaveChanges();
                return newUser;
            }
        }

        public User Update(User entity)
        {
            ValidateUser(entity);
            using (var _dbContext = new RaceCenterDbContext())
            {
                User user = _dbContext.Users.Find(entity.email);
                if (user == null)
                    throw new EntityDoesNotExistException($"User with email {entity.email} does not exist");
                user.address = entity.address;
                user.firstName = entity.firstName;
                user.lastName = entity.lastName;
                user.password = entity.password;

                _dbContext.SaveChanges();

                return user;
            }
        }

        public void  ValidateUser(User user)
        {
            try
            {
                MailAddress m = new MailAddress(user.email);
            }
            catch (FormatException)
            {
                throw new InvalidModelException($"User email is not in a valid format.");
            }

            if (string.IsNullOrEmpty(user.firstName))
                throw new InvalidModelException($"First name cannot be empty.");

            if (string.IsNullOrEmpty(user.lastName ))
                throw new InvalidModelException($"Last name cannot be empty.");

            if (string.IsNullOrEmpty(user.password))
                throw new InvalidModelException($"Password cannot be empty.");


        }
    }
}
