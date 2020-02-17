using NSI.DAL.Entities;
using System.Collections.Generic;

namespace NSI.DAL.Repository.Interfaces
{
    public interface IAvailabilityRuleRepository : IGenericRepository<AvailabilityRule>
    {
        ICollection<AvailabilityRule> GetAllByResourceId(long id);
    }
}