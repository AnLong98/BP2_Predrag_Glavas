using Service.Exceptions;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class EventService
    {
        private readonly OrganizersService _organizerService;

        public EventService( OrganizersService organizerService)
        {
            _organizerService = organizerService;
        }

        public List<string> GetEventChronometers(Event ev)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return  _dbContext.GetEventChronometers(ev.eventID).ToList();

            }
        }

        public void Delete(Event entity)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {


                Event event1 = _dbContext.Events.Find(entity.eventID);
                if (event1 == null)
                    throw new EntityDoesNotExistException($"Event with id {entity.eventID} does not exist.");
                _dbContext.Events.Remove(event1);
                _dbContext.SaveChanges();
            }
        }

        public List<Event> GetAll()
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Events.Include("Organizer").Include("Races").ToList();
            }
            
        }

        public Event GetByKey(int key)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Events.Include("Organizer").Include("Races").SingleOrDefault(x => x.eventID == key);
            }
            
        }

        public Event Insert(Event entity)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {


                ValidateEvent(entity);
                entity.eventID = 0;
                Event newEvent = _dbContext.Events.Add(entity);
                _dbContext.SaveChanges();
                return newEvent;
            }
        }

        private void ValidateEvent(Event entity)
        {
            if(string.IsNullOrEmpty(entity.bankAccount))
                throw new InvalidModelException($"Event must have a bank account.");
            if (string.IsNullOrEmpty(entity.name))
                throw new InvalidModelException($"Event must have a name.");
            if (entity.startDate != null && entity.endDate != null && entity.startDate > entity.endDate)
                throw new InvalidModelException($"Event start date must be before end date.");
            if(entity.startDate < DateTime.Now)
                throw new InvalidModelException($"Event start date must be in the future.");
            if (_organizerService.GetByKey(entity.Organizer_email) == null)
                throw new InvalidModelException($"Event must have a valid organizer.");
        }

        public Event Update(Event entity)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {


                ValidateEvent(entity);
                Event event1 = _dbContext.Events.Include("Organizer").Include("Races").SingleOrDefault(x => x.eventID == entity.eventID);
                if (event1 == null)
                    throw new EntityDoesNotExistException($"Event with id {entity.eventID} does not exist.");
                event1.bankAccount = entity.bankAccount;
                event1.endDate = entity.endDate;
                event1.name = entity.name;
                event1.Organizer_email = entity.Organizer_email;
                event1.startDate = entity.startDate;
                event1.website = entity.website;

                _dbContext.SaveChanges();
                return event1;
            }

            
        }
    }
}
