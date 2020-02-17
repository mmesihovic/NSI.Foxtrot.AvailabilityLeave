using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSI.DataContracts.Models.Requests;
using NSI.DataContracts.Models.Responses;
using NSI.BLL.Services.Interfaces;

namespace NSI.Controllers
{
    /// <summary>
    /// Api controller for Availability Module
    /// </summary>
    [Produces("application/json")]
    [Route("api/availability")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {

        private readonly IAvailabilityService _availabilityService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="availabilityService"></param>
        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }
        /// <summary>
        /// Get all scheduled absences for resource by ID
        /// </summary>
        /// <param name="id">Resource ID</param>
        /// <returns> See model: Timetable Response </returns>
        [HttpGet]
        [Route("resource/{id}")]
        public async Task<ActionResult<TimetableResponse>> GetResourceAvailability(long id)
        {
            var myTask = Task.Run(() => _availabilityService.GetById(id));
            var result = await myTask;
            return Ok(result);
        }
        /// <summary>
        /// Get all scheduled absences for multiple resources (by ID)
        /// </summary>
        /// <param name="idList">List of Resource IDs</param>
        /// <returns> 
        /// List of Timetable Responses, for each resource
        /// See model: Timetable Response 
        /// </returns>
        [HttpGet]
        [Route("resource/list")]
        public async Task<ActionResult<TimetableResponse>> GetResourceAvailability([FromBody] IdListRequest idList)
        {
            var myTask = Task.Run(() => _availabilityService.GetById(idList.IDs));
            var result = await myTask;
            return Ok(result);
        }
        /// <summary>
        /// Get all scheduled absences for resource in specific time range
        /// </summary>
        /// <param name="request"></param>
        /// <returns> See model: Timetable Response </returns>
        [HttpGet]
        [Route("timetable")]
        public async Task<ActionResult<TimetableResponse>> GetTimetable([FromBody] SingletonRequest request)
        {
            var myTask = Task.Run(() => _availabilityService.GetByIdAndTimeRange(request));
            var result = await myTask;
            return Ok(result);
        }
        /// <summary>
        /// Get all scheduled absences for list of resources in specific time range
        /// </summary>
        /// <param name="requestList"></param>
        /// <returns> 
        /// List of Timetable Responses, for each resource
        /// See model: Timetable Response 
        /// </returns>
        [HttpGet]
        [Route("timetable/list")]
        public async Task<ActionResult<TimetableResponse>> GetTimetableList([FromBody] CollectionRequest requestList)
        {
            var myTask = Task.Run(() => _availabilityService.GetByIdAndTimeRange(requestList));
            var result = await myTask;
            return Ok(result);
        }
     
    }
}