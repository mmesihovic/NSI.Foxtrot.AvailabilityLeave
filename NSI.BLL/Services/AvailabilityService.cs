using NSI.BLL.Services.Interfaces;
using NSI.DAL.Entities;
using NSI.DataContracts.Models.Requests;
using NSI.DataContracts.Models.Responses;
using System.Linq;
using System.Collections.Generic;

namespace NSI.BLL.Services
{
    /// <summary>
    /// Service used by Availability Controller for API, manages data in DB via repositories and database context
    /// </summary>
    public class AvailabilityService : IAvailabilityService
    {

        private readonly IScheduleService _scheduleService;
        private readonly AutoMapper.IMapper _mapper;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="scheduleService"></param>
        /// <param name="mapper"></param>
        public AvailabilityService(IScheduleService scheduleService, AutoMapper.IMapper mapper)
        {
            this._scheduleService = scheduleService;
            this._mapper = mapper;
        }
        /// <summary>
        /// Get existing schedules in DB for given resource
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TimetableResponse GetById(long id)
        {
            ICollection<ResourceSchedule> schedules = _scheduleService.GetAllByResourceID(id);
            return MapResourceScheduleToResponse(id, schedules);
        }
        /// <summary>
        /// Get all existing schedules in DB for given list of resources
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public ICollection<TimetableResponse> GetById(IEnumerable<long> idList)
        {
            ICollection<TimetableResponse> response = new List<TimetableResponse>();
            foreach(var _id in idList)
            {
                response.Add(GetById(_id));
            }
            return response;
        }
        /// <summary>
        /// Get all existing schedules in DB for given resource in defined time range
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TimetableResponse GetByIdAndTimeRange(SingletonRequest request)
        {
            ICollection<ResourceSchedule> schedules = _scheduleService.GetByIdAndTimeRange(request.ResourceID, request.From, request.Until);

            return MapResourceScheduleToResponse(request.ResourceID, schedules);
        }
        /// <summary>
        /// Get all existing schedules in DB for list of given resources in defined time range
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ICollection<TimetableResponse> GetByIdAndTimeRange(CollectionRequest request)
        {
            ICollection<TimetableResponse> response = new List<TimetableResponse>();
            request.IDs.ToList().ForEach(id => response.Add(GetByIdAndTimeRange(new SingletonRequest{ ResourceID = id, From = request.From, Until = request.Until})));
            return response;
        }

        private TimetableResponse MapResourceScheduleToResponse(long id, ICollection<ResourceSchedule> _schedule)
        {
            TimetableResponse response = new TimetableResponse();
            response.ResourceID = id;
            response.AbsenceDetails = new List<AbsenceDetails>();
            response.Available = true;
            foreach(var item in _schedule)
            {
                if(item.Approved)
                {
                    response.Available = false;
                }
                AbsenceDetails _absenceDetails = new AbsenceDetails();
                _absenceDetails.ID = item.ID;
                _absenceDetails.From = item.FromDate;
                _absenceDetails.Until = item.UntilDate;
                _absenceDetails.Approved = item.Approved;
                _absenceDetails.ApprovedByID = item.ApprovedByID;
                _absenceDetails.RequestByID = item.RequestByID;

                EventResponse _eventResponse = new EventResponse();
                _eventResponse.ID = item.Event.ID;
                _eventResponse.Name = item.Event.Name;
                
                EventTypeResponse _eventTypeResponse = new EventTypeResponse();
                _eventTypeResponse.ID = item.Event.Type.ID;
                _eventTypeResponse.Name = item.Event.Type.Name;

                _eventResponse.Type = _eventTypeResponse;
                _absenceDetails.Event = _eventResponse;

                response.AbsenceDetails.Add(_absenceDetails);
            }
            return response;
        }

    }
}
