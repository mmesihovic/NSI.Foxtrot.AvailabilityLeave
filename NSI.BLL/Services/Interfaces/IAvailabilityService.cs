using NSI.DataContracts.Models.Requests;
using NSI.DataContracts.Models.Responses;
using System.Collections.Generic;

namespace NSI.BLL.Services.Interfaces
{
    public interface IAvailabilityService
    {
        // 15.12.2019 Updated logic to only Read, rest of CRUD operations are handled by ScheduleService
        //Read
        TimetableResponse GetById(long id);
        ICollection<TimetableResponse> GetById(IEnumerable<long> idList);
        TimetableResponse GetByIdAndTimeRange(SingletonRequest request);
        ICollection<TimetableResponse> GetByIdAndTimeRange(CollectionRequest request);
        
    }
}
