
using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using NSI.DataContracts.Models.Requests;
using NSI.DataContracts.Models.Responses;
using NSI.DAL.Entities;
using Microsoft.AspNetCore.Http;

namespace NSI.Controllers {
    
    /// <summary>
    /// Api controller for handling scheduled leave, absence or other events that prevent resource from being available
    /// </summary>
    [Route("api/schedule")]
    public class ScheduleController : ControllerBase {
        
        private readonly IScheduleService scheduleService;
        private readonly IEventService eventService;
        private readonly AutoMapper.IMapper mapper;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="scheduleService"></param>
        /// <param name="eventService"></param>
        /// <param name="mapper"></param>
        public ScheduleController(IScheduleService scheduleService, IEventService eventService, AutoMapper.IMapper mapper){
            this.scheduleService = scheduleService;
            this.eventService = eventService;
            this.mapper = mapper;
        }
        /// <summary>
        /// Returns all schedules in database
        /// </summary>
        /// <returns> Collection of Schedule Response (See Model) </returns>
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<ICollection<ScheduleResponse>>> GetAll(){
            var task = Task.Run(() => scheduleService.GetAll());
            var result = await task;
            return Ok(mapper.Map<ICollection<ScheduleResponse>>(result));
        }
        /// <summary>
        /// Creates a schedule in database
        /// </summary>
        /// <param name="scheduleRequestPost"></param>
        /// <returns> Collection of Schedule Response (See Model) </returns>
        [HttpPost("request")]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ScheduleResponse>> Create([FromBody] ScheduleRequestPost scheduleRequestPost){
           
            if(scheduleRequestPost.From >= scheduleRequestPost.Until){
                return BadRequest("From timestamp must be earlier then Until timestamp.");
            }

            Event _event = eventService.GetById(scheduleRequestPost.EventID);
            
            if(_event == null){
                return BadRequest("Event with ID " + scheduleRequestPost.EventID + " does not exist.");
            }

            if(scheduleRequestPost.From >= scheduleRequestPost.Until){
                return BadRequest("From date must be before Until date.");
            }
            ResourceSchedule resourceSchedule =  mapper.Map<ResourceSchedule>(scheduleRequestPost);
            // Fill Additional data for request
            resourceSchedule.Approved = false;
            resourceSchedule.ApprovedByID = null;
            resourceSchedule.Event = _event;
            
            var task = Task.Run(() => scheduleService.Create(resourceSchedule));
            var result = await task;
            
            return Ok(mapper.Map<ScheduleResponse>(result));    
        }
        /// <summary>
        /// Approves schedule by ID
        /// </summary>
        /// <param name="approveRequest"></param>
        /// <returns> See Model: Schedule Response </returns>
        [HttpPost("approve")]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ScheduleResponse>> Approve([FromBody] ApproveRequest approveRequest){
            
            var task = Task.Run(() => scheduleService.Approve(approveRequest));
            var result = await task;
            if(result == null){
                return NotFound("The schedule entry you wanted to approve does'nt exist.");
            }
            return Ok(mapper.Map<ScheduleResponse>(result));    
        }
        /// <summary>
        /// Deletes schedule by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] long id){
            var task = Task.Run(() => scheduleService.Delete(id));
            var result = await task;
            if(result == false)
                return NotFound();
            else 
                return Ok();

        }
    }
}