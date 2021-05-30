using Service.Exceptions;
using Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class RaceService
    {
        private ChronometersService _chronometersService;
        private EventService _eventService;

        public RaceService( ChronometersService chronometersService, EventService eventService)
        {
            _chronometersService = chronometersService;
            _eventService = eventService;
        }

        public void ConsolidateStartersList(int raceID)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                _dbContext.ConsolidateStartersList(raceID);
            }
        }

        public void Delete(Race entity)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                Race race = _dbContext.Races.Find(entity.raceID);
                if (race == null)
                    throw new EntityDoesNotExistException($"Race with id {entity.raceID} does not exist.");
                _dbContext.Races.Remove(race);
                _dbContext.SaveChanges();
            }
        }

        public List<Race> GetAll()
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Races.Include("Registrations").Include("Chronometer").Include("Event").ToList();
            }
        }

        public Race GetByKey(int key)
        {
            using (var _dbContext = new RaceCenterDbContext())
            {
                return _dbContext.Races.Include("Registrations").Include("Event").Include("Chronometer").SingleOrDefault(x => x.raceID == key);
            }
        }

        public Race Insert(Race entity)
        {
            ValidateRace(entity);
            if (GetByKey(entity.raceID) != null)
                throw new DuplicateEntityException($"Race for this id already exists");
            using (var _dbContext = new RaceCenterDbContext())
            {
                Race newRace = _dbContext.Races.Add(entity);
                _dbContext.SaveChanges();
                return newRace;
            }
        }

        private void ValidateRace(Race entity)
        {
            if(entity.date < DateTime.Now)
                throw new InvalidModelException($"Race date cannot be in the past.");
            if (entity.Event_eventID == 0)
                throw new InvalidModelException($"Race must be attached to an event.");
            Event @event = _eventService.GetByKey(entity.Event_eventID);
            if(@event == null)
                throw new InvalidModelException($"Race must be attached to an existing event.");
            if (entity.date < @event.startDate || entity.date > @event.endDate)
                throw new InvalidModelException($"Race date must be between event start and end dates.");
            if (entity.distance <= 0)
                throw new InvalidModelException($"Race distance must be positive value .");
            if(_chronometersService.GetByKey(entity.Chronometer_email) == null)
                throw new InvalidModelException($"Race must have an assigned chronometer.");
            if (entity.minStartNumber >= entity.maxStartNumber)
                throw new InvalidModelException($"Race min start number cannot be same or higher than max start number.");
            if (string.IsNullOrEmpty(entity.name))
                throw new InvalidModelException($"Race must have a name.");
            if (entity.startFarePrice < 0)
                throw new InvalidModelException($"Race start fare must be 0 or positive.");
            if (entity.type != "MARATHON" && entity.type != "HALFMARATHON" && entity.type != "RELAY" && entity.type != "OBSTACLE")
                throw new InvalidModelException($"Race must of one of the following types : MARATHON, RELAY, HALFMARATHON OBSTACLE.");
        }

        public Race Update(Race entity)
        {
            ValidateRace(entity);
            using (var _dbContext = new RaceCenterDbContext())
            {
                Race race = _dbContext.Races.Include("Registrations").Include("Event").SingleOrDefault(x => x.raceID == entity.raceID);
                if (race == null)
                    throw new EntityDoesNotExistException($"Race with id {entity.raceID} does not exist.");
                race.Chronometer_email = entity.Chronometer_email;
                race.date = entity.date;
                race.distance = entity.distance;
                race.startFarePrice = entity.startFarePrice;
                race.minStartNumber = entity.minStartNumber;
                race.maxStartNumber = entity.maxStartNumber;
                race.name = entity.name;
                race.Event_eventID = entity.Event_eventID;

                _dbContext.SaveChanges();

                return race;
            }
        }
    }
}
