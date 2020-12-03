using ComedyEvents.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComedyEvents.Services
{
    public interface IEventRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        Task<bool> Save();

        //events
        Task<Event[]> GetEvents(bool includeGigs = false);
        Task<Event> GetEvent(int eventID, bool includeGigs = false);
        Task<Event[]> GetEventByDate(DateTime dateTime, bool includeGigs = false);

        //gigs
        Task<Gig[]> GetGigsByEvent(int eventID, bool includeComedians = false);
        Task<Gig> GetGig(int gigID, bool includeComedians = false);
        Task<Gig[]> GetGigsByVenue(int venueID, bool includeComedians = false);

        //comedians
        Task<Comedian[]> GetComedians();
        Task<Comedian[]> GetComediansByEvent(int eventID);
        Task<Comedian> GetComedian(int comedianID);
    }
}
