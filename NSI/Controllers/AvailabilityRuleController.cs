using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NSI.DataContracts.Models.Requests;
using NSI.DataContracts.Models.Responses;
using NSI.BLL.Services.Interfaces;
using NSI.DAL.Entities;


namespace NSI.Controllers
{
    /// <summary>
    /// Api controller for AvailabilityRules in Availability Module
    /// </summary>
    [Route("api/availabilityrules")]
    [ApiController]
    public class AvailabilityRuleController : ControllerBase
    {
        private readonly IAvailabilityRuleService availabilityRuleService;
        private readonly AutoMapper.IMapper mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="availabilityRuleService"></param>
        /// <param name="mapper"></param>
        public AvailabilityRuleController(IAvailabilityRuleService availabilityRuleService, AutoMapper.IMapper mapper){
            this.availabilityRuleService = availabilityRuleService;
            this.mapper = mapper;
        }
        /// <summary>
        /// Returns all availability rules (Scheduled Work Times etc.)
        /// </summary>
        /// <returns> See Model: Availability Rule Response </returns>
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<ICollection<AvailabilityRuleResponse>>> GetAll(){
            var task = Task.Run(() => availabilityRuleService.GetAll());
            var result = await task;
            return Ok(mapper.Map<ICollection<AvailabilityRuleResponse>>(result));
        }
        /// <summary>
        /// Returns all availability rules for specific resource
        /// </summary>
        /// <param name="id">Resource ID</param>
        /// <returns> See Model: Availability Rule Reponse</returns>
        [HttpGet]
        [Route("resource/{id}/all")]
        public async Task<ActionResult<ICollection<AvailabilityRuleResponse>>> GetAllByResourceId([FromRoute] long id){
            var task = Task.Run(() => availabilityRuleService.GetAllByResourceId(id));
            var result = await task;
            return Ok(mapper.Map<ICollection<AvailabilityRuleResponse>>(result));
        }
        /// <summary>
        /// Creates a availability rule for given resource
        /// </summary>
        /// <param name="availabilityRuleRequest"></param>
        /// <returns> See Model: Availability Rule Response</returns>
        [HttpPost]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AvailabilityRuleResponse>> Create([FromBody] AvailabilityRuleRequest availabilityRuleRequest){
            AvailabilityRule availabilityRule = mapper.Map<AvailabilityRule>(availabilityRuleRequest);
            if(availabilityRule.FromTime >= availabilityRule.ToTime){
                return BadRequest("From time stamp must be before To time stamp.");
            }
            var task = Task.Run(() => availabilityRuleService.Create(availabilityRule));
            var result = await task;

            return Ok(mapper.Map<AvailabilityRuleResponse>(result));
        }
        /// <summary>
        /// Deletes availability rule for given resource
        /// </summary>
        /// <param name="id">Resource ID</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteById([FromRoute] long id){
            var task = Task.Run(() => availabilityRuleService.Delete(id));
            var result = await task;
            
            if(result)
                return Ok();
            else
                return NotFound("The resource you were trying to delete doesn't exist.");
        }
     
    }
}