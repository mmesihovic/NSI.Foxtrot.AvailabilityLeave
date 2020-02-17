using NSI.DAL.Entities;
using System.Collections.Generic;
using NSI.DataContracts.Models.Requests;
using System;

namespace NSI.BLL.Services.Interfaces
{
    public interface IScheduleService {
        ICollection<ResourceSchedule> GetAll();

        ResourceSchedule GetById(long id);
        ResourceSchedule Create(ResourceSchedule resourceSchedule);
        ResourceSchedule Approve(ApproveRequest approveRequest);

        ICollection<ResourceSchedule> GetByIdAndTimeRange(long id, DateTime _fromDate, DateTime _untilDate);
        ICollection<ResourceSchedule> GetAllByResourceID(long id);
        bool Delete(long id);
    }
}