using ComedyEvents.Context;
using ComedyEvents.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComedyEvents.Services
{
    public class EventRepository : IEventRepository
    {
        private readonly EventContext _eventContext;
        private readonly ILogger<EventRepository> _logger;

        public EventRepository(EventContext eventContext,ILogger<EventRepository> logger)
        {
            _eventContext = eventContext;
            _logger = logger;
        }
        
        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding object of type {entity.GetType()}");
            _eventContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Deleting object of type {entity.GetType()}");
            _eventContext.Remove(entity);
        }

        public async Task<Comedian> GetComedian(int comedianID)
        {
            _logger.LogInformation($"Getting comedian for ID {comedianID}");
            var query = _eventContext.comedians
                .Where(c => c.ComedianID == comedianID);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Comedian[]> GetComedians()
        {
            _logger.LogInformation($"Getting all comedians");
            var query = _eventContext.comedians
                .OrderBy(c => c.LastName);
            return await query.ToArrayAsync();
        }

        public async Task<Comedian[]> GetComediansByEvent(int eventID)
        {
            _logger.LogInformation($"Getting all comedians for event {eventID}");
            var query = _eventContext.gigs
                .Where(c => c.Event.EventID == eventID)
                .Select(c => c.Comedian)
                .OrderBy(c => c.LastName);
            return await query.ToArrayAsync();
        }

        public async Task<Event> GetEvent(int eventID, bool includeGigs = false)
        {
            _logger.LogInformation($"Getting event for event id {eventID}");
            IQueryable<Event> query = _eventContext.events
                .Include(v => v.Venue);
            if (includeGigs)
            {
                query = query.Include(g => g.Gigs)
                    .ThenInclude(c => c.Comedian);
            }
            query = query.Where(e => e.EventID == eventID);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Event[]> GetEventByDate(DateTime dateTime, bool includeGigs = false)
        {
            _logger.LogInformation($"Getting events for date {dateTime}");
            IQueryable<Event> query = _eventContext.events
                .Include(v => v.Venue);

            if (includeGigs)
            {
                query = query.Include(g => g.Gigs)
                    .ThenInclude(c => c.Comedian);
            }
            query = query.OrderBy(e => e.EventDate)
                .Where(e => e.EventDate == dateTime);

            return await query.ToArrayAsync();
        }

        public async Task<Event[]> GetEvents(bool includeGigs = false)
        {
            _logger.LogInformation($"Getting all events");
            IQueryable<Event> query = _eventContext.events
                .Include(v => v.Venue);
            if (includeGigs)
            {
                query = query.Include(g => g.Gigs)
                    .ThenInclude(c => c.Comedian);
            }
            query = query.OrderBy(e => e.EventDate);

            return await query.ToArrayAsync();
        }

        public async Task<Gig[]> GetGigsByEvent(int eventID, bool includeComedians = false)
        {
            _logger.LogInformation($"Getting gig with event id {eventID}");
            IQueryable<Gig> query = _eventContext.gigs;
            if (includeComedians)
            {
                query = query.Include(c => c.Comedian);
            }
            query = query.OrderBy(e => e.Event.EventID == eventID)
                        .Include(e => e.Event)
                        .OrderByDescending(g => g.GigHeadline);

            return await query.ToArrayAsync();
        }

        public async Task<Gig> GetGig(int gigID, bool includeComedians = false)
        {
            _logger.LogInformation($"Getting gig with id {gigID}");
            IQueryable<Gig> query = _eventContext.gigs;
            if (includeComedians)
            {
                query = query.Include(c => c.Comedian);
            }
            query = query.OrderBy(g => g.GigID == gigID).Include(e => e.Event);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Gig[]> GetGigsByVenue(int venueID, bool includeComedians = false)
        {
            _logger.LogInformation($"Getting gig with venue id {venueID}");
            IQueryable<Gig> query = _eventContext.gigs;
            if (includeComedians)
            {
                query = query.Include(c => c.Comedian);
            }
            query = query.OrderBy(v => v.Event.Venue.VenueID == venueID)
                        .Include(v => v.Event.Venue)
                        .OrderByDescending(g => g.GigHeadline);

            return await query.ToArrayAsync();
        }

        public async Task<bool> Save()
        {
            _logger.LogInformation("Saving changes");
            return (await _eventContext.SaveChangesAsync()) >= 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _logger.LogInformation($"Updating object of type {entity.GetType()}");
            _eventContext.Update(entity);
        }
    }
}
