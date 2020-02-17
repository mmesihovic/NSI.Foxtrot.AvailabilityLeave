
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NSI.BLL.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using NSI.DataContracts.Models.Requests;
using NSI.DataContracts.Models.Responses;
using NSI.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace NSI.Controllers {
    
    /// <summary>
    /// Api controller for Events in all modules (Absence/Availability/Leave)
    /// </summary>
    [Route("api/event")]
    public class EventController : ControllerBase {
        
        private readonly IEventService eventService;
        private readonly IEventTypeService eventTypeService;
        private readonly AutoMapper.IMapper mapper;
    
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="eventService"></param>
        /// <param name="eventTypeService"></param>
        /// <param name="mapper"></param>
        public EventController(IEventService eventService, IEventTypeService eventTypeService, AutoMapper.IMapper mapper){
            this.eventService = eventService;
            this.eventTypeService = eventTypeService;
            this.mapper = mapper;
        }
        /// <summary>
        /// Returns all events in database
        /// </summary>
        /// <returns> Collection of model Event Response </returns>
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<ICollection<EventResponse>>> GetAll(){
            var task = Task.Run(() => eventService.GetAll());
            var result = await task;
            return Ok(mapper.Map<ICollection<EventResponse>>(result));
        }
        /// <summary>
        /// Get event by ID
        /// </summary>
        /// <param name="id">Event ID</param>
        /// <returns> See Model: Event Response</returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventResponse>> GetById([FromRoute] long id){
            var task = Task.Run(() => eventService.GetById(id));
            var result = await task;
            if(result == null)
                return NotFound();
            return Ok(mapper.Map<EventResponse>(result));
        }
        /// <summary>
        /// Returns all Event Types in database
        /// </summary>
        /// <returns> Collection of model Event Type Response</returns>
        [HttpGet]
        [Route("types")]
        public async Task<ActionResult<ICollection<EventTypeResponse>>> GetTypes(){
            var task = Task.Run(() => eventTypeService.GetAll());
            var result = await task;
            return Ok(mapper.Map<ICollection<EventTypeResponse>>(result));
        }
        /// <summary>
        /// Creates a new Event in database
        /// </summary>
        /// <param name="eventRequest"></param>
        /// <returns> Created event object </returns>
        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
        public async Task<ActionResult<EventResponse>> Create([FromBody] EventRequest eventRequest){
             
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // TODO: Move validation to model state
            if(eventRequest.Type.ID == null && string.IsNullOrEmpty(eventRequest.Type.Name))
                return BadRequest("The Event Type Name and event Type Id can't be null.");

            Event _event = mapper.Map<Event>(eventRequest);
            var task = Task.Run(() => eventService.Create(_event, eventRequest.Type.ID, eventRequest.Type.Name));
            Event savedEvent;
            try {
                savedEvent = await task;
            } catch(KeyNotFoundException e){
                return BadRequest(e.Message);
            } catch(DbUpdateException e){
                // TODO: pull detailed error message from e and return it
                System.Console.WriteLine(e.ToString());
                return Conflict("Unique constraint validation on field 'Name' - " + _event.Name + " already exists.");
            }
            
            return Ok(mapper.Map<EventResponse>(savedEvent));
        }

    }
}