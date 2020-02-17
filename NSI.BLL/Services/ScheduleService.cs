using NSI.BLL.Services.Interfaces;
using NSI.DAL.Entities;
using NSI.DAL.Repository.Interfaces;
using NSI.DataContracts.Models.Requests;
using System;
using System.Collections.Generic;

namespace NSI.BLL.Services {
    /// <summary>
    /// Service used by Schedule Controller for API, manages data in DB via repositories and database context
    /// </summary>
    public class ScheduleService : IScheduleService {

        private readonly IScheduleRepository scheduleRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="scheduleRepository"></param>
        public ScheduleService(IScheduleRepository scheduleRepository){
            this.scheduleRepository = scheduleRepository;
        }
        /// <summary>
        /// Get all Schedules in Database
        /// </summary>
        /// <returns></returns>
        public ICollection<ResourceSchedule> GetAll(){
            return scheduleRepository.GetAll();
        }
        /// <summary>
        /// Creates new Schedule entry for Resource in Database
        /// </summary>
        /// <param name="resourceSchedule"></param>
        /// <returns></returns>
        public ResourceSchedule Create(ResourceSchedule resourceSchedule){
            return scheduleRepository.Create(resourceSchedule);       
        }
        /// <summary>
        /// Approves existing Scheduled absence/leave or event
        /// </summary>
        /// <param name="approveRequest"></param>
        /// <returns></returns>
        public ResourceSchedule Approve(ApproveRequest approveRequest){
            ResourceSchedule resourceSchedule = scheduleRepository.GetById(approveRequest.ScheduleID);
            if(resourceSchedule == null)
                return null;
            resourceSchedule.ApprovedByID = approveRequest.ApprovedByID;
            resourceSchedule.Approved = true;
            ResourceSchedule updated = scheduleRepository.Update(resourceSchedule);
            return updated;
        }
        /// <summary>
        /// Get schedule by ID from Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResourceSchedule GetById(long id){
            return scheduleRepository.GetById(id);
        }
        /// <summary>
        /// Get schedule from Databse by ID and in given time range
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_fromDate"></param>
        /// <param name="_untilDate"></param>
        /// <returns></returns>
        public ICollection<ResourceSchedule> GetByIdAndTimeRange(long id, DateTime _fromDate, DateTime _untilDate)
        {
            return scheduleRepository.GetByIdAndTimeRange(id, _fromDate, _untilDate);
        }
        /// <summary>
        /// Get all Schedules for given resource (provided ID)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICollection<ResourceSchedule> GetAllByResourceID(long id)
        {
            return scheduleRepository.GetAllByResourceID(id);
        }
        /// <summary>
        /// Deletes existing schedule in database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id){
            return scheduleRepository.Delete(id);
        }

    }
}