
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NSI.BLL.Services.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using NSI.DataContracts.Models.Requests;
using NSI.DataContracts.Models.Responses;
using Microsoft.Extensions.Configuration;
using System;

namespace NSI.Controllers {
    
    /// <summary>
    /// Api controller for Leave Module
    /// </summary>
    [ApiController]
    [Route("api/leave")]
    public class LeaveController : ControllerBase {
        

        
        private readonly AutoMapper.IMapper mapper;
        private readonly ILeaveService leaveService;
    
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="leaveService"></param>
        /// <param name="configuration"></param>
        /// <param name="mapper"></param>
        public LeaveController(ILeaveService leaveService, IConfiguration configuration, AutoMapper.IMapper mapper){
            this.leaveService = leaveService;
            this.mapper = mapper;
        }
        /// <summary>
        /// Retrieves all Leave balances in database
        /// </summary>
        /// <returns> See Model: Leave Balance Response</returns>
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<ICollection<LeaveBalanceResponse>>> GetAll(){
            var task = Task.Run(() => leaveService.GetAll());
            var result = await task;
            return Ok(mapper.Map<ICollection<LeaveBalanceResponse>>(result));
        }
        /// <summary>
        /// Retrieves all leave balance modifications for given balance
        /// </summary>
        /// <param name="id">Balance ID</param>
        /// <returns> See Model: Leave Transaction Response</returns>
        [HttpGet]
        [Route("{id}/transactions")]
        public async Task<ActionResult<ICollection<LeaveBalanceResponse>>> GetAllTransactionsByLeaveBalanceId([FromRoute] long id){
            var task = Task.Run(() => leaveService.GetAllTransactionsByLeaveBalanceId(id));
            var result = await task;
            return Ok(mapper.Map<ICollection<LeaveTransactionResponse>>(result));
        }
        /// <summary>
        /// Creates a leave balance request for resource
        /// </summary>
        /// <param name="leaveBalanceRequest"></param>
        /// <returns> See Model: Leave Balance Response </returns>
        [HttpPost]
        public async Task<ActionResult<LeaveBalanceResponse>> Create([FromBody] LeaveBalanceRequest leaveBalanceRequest){    

            var task = Task.Run(() => leaveService.CreateOrUpdate(leaveBalanceRequest));
            var result = await task;
            
            return Ok(mapper.Map<LeaveBalanceResponse>(result));
        }
        /// <summary>
        /// Retrieves leave balance for given resource
        /// </summary>
        /// <param name="id">Resource ID</param>
        /// <returns> See Model: Leave Balance Response</returns>
        [HttpGet]
        [Route("resource/{id}")]
        public async Task<ActionResult<LeaveBalanceResponse>> GetByResourceId([FromRoute] long id){
            var task = Task.Run(() => leaveService.GetByResourceId(id));
            var result = await task;
            return Ok(mapper.Map<LeaveBalanceResponse>(result));
        }
        /// <summary>
        /// Updates already existant leave transaction
        /// </summary>
        /// <param name="leaveTransactionRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("transaction")]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LeaveBalanceResponse>> Change([FromBody] LeaveTransactionRequest leaveTransactionRequest){
            var task = Task.Run(() => leaveService.Change(leaveTransactionRequest));
            try {
                var result = await task;
                return Ok(mapper.Map<LeaveBalanceResponse>(result));
            } catch(ArgumentOutOfRangeException e){
                return BadRequest(e.Message);
            } catch(ArgumentException e){
                return NotFound(e.Message);
            }
        }
    }
}