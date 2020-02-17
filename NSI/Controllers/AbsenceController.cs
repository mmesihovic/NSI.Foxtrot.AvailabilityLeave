using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSI.BLL.Services.Interfaces;
using NSI.DataContracts.Models.Requests;
using NSI.DataContracts.Models.Responses;

namespace NSI.Controllers
{
    /// <summary>
    /// Api controler for Absence Module
    /// </summary>
    [Produces("application/json")]
    [Route("api/absence")]
    [ApiController]
    public class AbsenceController : ControllerBase
    {
        private readonly IAbsenceService _absenceService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="absenceService"></param>
        public AbsenceController(IAbsenceService absenceService)
        {
            _absenceService = absenceService;
        }

        /// <summary>
        /// Get all scheduled absences for resource by ID
        /// </summary>
        /// <param name="id">Resource ID</param>
        /// <returns> See model: Timetable Response (If Available property is 'false', resource is absent) </returns>
        [HttpGet]
        [Route("resource/{id}")]
        public async Task<ActionResult<TimetableResponse>> GetResourceAbsence(long id)
        {
            var myTask = Task.Run(() => _absenceService.GetById(id)) ;
            var result = await myTask;
            return Ok(result);
        }
        /// <summary>
        /// Get all scheduled absences for multiple resources (by ID)
        /// </summary>
        /// <param name="idList">List of Resource IDs</param>
        /// <returns> 
        /// List of Timetable Responses, for each resource
        /// See model: Timetable Response (If Available property is 'false', resource is absent)
        /// </returns>
        [HttpGet]
        [Route("resource/list")]
        public async Task<ActionResult<TimetableResponse>> GetResourceAbsence([FromBody] IdListRequest idList)
        {
            var myTask = Task.Run(() => _absenceService.GetById(idList.IDs));
            var result = await myTask;
            return Ok(result);
        }
        /// <summary>
        /// Get all scheduled absences for resource in specific time range
        /// </summary>
        /// <param name="request"></param>
        /// <returns> See model: Timetable Response (If Available property is 'false', resource is absent) </returns>
        [HttpGet]
        [Route("timetable")]
        public async Task<ActionResult<TimetableResponse>> GetTimetable([FromBody] SingletonRequest request)
        {
            var myTask = Task.Run(() => _absenceService.GetByIdAndTimeRange(request));
            var result = await myTask;
            return Ok(result);
        }
        /// <summary>
        /// Get all scheduled absences for list of resources in specific time range
        /// </summary>
        /// <param name="requestList"></param>
        /// <returns> 
        /// List of Timetable Responses, for each resource
        /// See model: Timetable Response (If Available property is 'false', resource is absent)
        /// </returns>
        [HttpGet]
        [Route("timetable/list")]
        public async Task<ActionResult<TimetableResponse>> GetTimetableList([FromBody] CollectionRequest requestList)
        {
            var myTask = Task.Run(() => _absenceService.GetByIdAndTimeRange(requestList));
            var result = await myTask;
            return Ok(result);
        }
    }
}