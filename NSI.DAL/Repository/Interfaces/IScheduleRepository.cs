using NSI.DAL.Entities;
using System;
using System.Collections.Generic;

namespace NSI.DAL.Repository.Interfaces
{
    public interface IScheduleRepository : IGenericRepository<ResourceSchedule>
    {
        ICollection<ResourceSchedule> GetAllByResourceID(long id);
        ICollection<ResourceSchedule> GetByIdAndTimeRange(long id, DateTime _from, DateTime _until);

    }
}