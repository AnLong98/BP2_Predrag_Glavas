using Service.Exceptions;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Service.Services
{
    public class OrganizersService
    {
        private readonly UsersService _usersService;

        public OrganizersService(UsersService usersService)
        {
            _usersService = usersService;
        }

        public void Delete(Organizer entity)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                Organizer user = _dbContext.Organizers.Find(entity.email);
                if (user == null)
                    throw new EntityDoesNotExistException($"Organizer with email {entity.email} does not exist");
                _dbContext.Organizers.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        public List<Organizer> GetAll()
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Organizers.Include("Events").Include("Employees").Include("Supervisor").ToList();
            }
        }

        public Organizer GetByKey(string key)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                Organizer o = _dbContext.Organizers.Include("Employees")
                                                .Include("Events")
                                                .Include("Supervisor")
                                                .SingleOrDefault(x => x.email == key); 
                return o;
            }
        }

        public Organizer Insert(Organizer entity)
        {
            ValidateOrganizer(entity);
            if (_usersService.GetByKey(entity.email) != null)
                throw new DuplicateEntityException($"Organizer with email {entity.email} already exists");
            using (var _dbContext = new RaceCenterDbContext())
            {
                Organizer newOrganizer = _dbContext.Organizers.Add(entity);
                _dbContext.SaveChanges();
                return newOrganizer;
            }
        }

        private void ValidateOrganizer(Organizer entity)
        {
            _usersService.ValidateUser(entity);
            if(!string.IsNullOrWhiteSpace(entity.phoneNumber))
            {
                foreach (char c in entity.phoneNumber)
                {
                    if (c < '0' || c > '9')
                        throw new InvalidModelException($"Phone number must contain numbers only.");
                }

            }

            if(!string.IsNullOrWhiteSpace(entity.supervisorEmail) && GetByKey(entity.supervisorEmail) ==null)
                      throw new InvalidModelException($"Organizer must have a valid supervisor.");

            if (entity.supervisorEmail == entity.email)
                throw new InvalidModelException($"Organizer can't supervise himself.");

        }

        public Organizer Update(Organizer entity)
        {
            ValidateOrganizer(entity);
            using (var _dbContext = new RaceCenterDbContext())
            {
                Organizer user = _dbContext.Organizers.Include("Employees")
                                                .Include("Events")
                                                .Include("Supervisor")
                                                .SingleOrDefault(x => x.email == entity.email);

                if (user == null)
                    throw new EntityDoesNotExistException($"Organizer with email {entity.email} does not exist");
                user.address = entity.address;
                user.firstName = entity.firstName;
                user.lastName = entity.lastName;
                user.password = entity.password;
                user.isAdmin = entity.isAdmin;
                user.supervisorEmail = entity.supervisorEmail;
                if (user.supervisorEmail == "")
                {
                    user.Supervisor = null;
                    user.supervisorEmail = null;
                }
                    

                _dbContext.SaveChanges();

                return user;
            }
        }
    }
}
