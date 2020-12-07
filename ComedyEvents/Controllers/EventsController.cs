using ComedyEvents.DTO;
using ComedyEvents.Model;
using ComedyEvents.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComedyEvents.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventsController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public async Task<ActionResult<EventDTO[]>> Get(bool includeGigs = false)
        {
            try
            {
                var results = await _eventRepository.GetEvents(includeGigs);
                var eventDTO = new EventDTO();
                foreach (var e in results)
                {
                    eventDTO.Venue.City = e.Venue.City;
                    eventDTO.EventDate = e.EventDate;
                    eventDTO.EventName = e.EventName;
                }
                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}
