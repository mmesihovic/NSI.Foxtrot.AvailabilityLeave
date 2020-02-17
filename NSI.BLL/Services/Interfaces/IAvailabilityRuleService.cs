using System.Collections.Generic;
using NSI.DAL.Entities;

namespace NSI.BLL.Services.Interfaces
{
    public interface IAvailabilityRuleService
    {
        ICollection<AvailabilityRule> GetAll();
        ICollection<AvailabilityRule> GetAllByResourceId(long id);

        AvailabilityRule Create(AvailabilityRule availabilityRuleRequest);
        bool Delete(long id);
    }
}
