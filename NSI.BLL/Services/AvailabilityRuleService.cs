using NSI.BLL.Services.Interfaces;
using NSI.DAL.Entities;
using NSI.DAL.Repository.Interfaces;
using System.Collections.Generic;

namespace NSI.BLL.Services
{
    /// <summary>
    /// Service used by AvailabilityRule Controller for API, manages data in DB via repositories and database context
    /// </summary>
    public class AvailabilityRuleService : IAvailabilityRuleService
    {

        private readonly IAvailabilityRuleRepository availabilityRuleRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="availabilityRuleRepository"></param>
        public AvailabilityRuleService(IAvailabilityRuleRepository availabilityRuleRepository){
            this.availabilityRuleRepository = availabilityRuleRepository;
        }
        /// <summary>
        /// Get all avilability rules in Database
        /// </summary>
        /// <returns></returns>
        public ICollection<AvailabilityRule> GetAll(){
            return availabilityRuleRepository.GetAll();
        }
        /// <summary>
        /// Get all availability rules for given resource
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICollection<AvailabilityRule> GetAllByResourceId(long id){
            return availabilityRuleRepository.GetAllByResourceId(id);
        }
        /// <summary>
        /// Creates new Availability Rule in database
        /// </summary>
        /// <param name="availabilityRule"></param>
        /// <returns></returns>
        public AvailabilityRule Create(AvailabilityRule availabilityRule){
            return availabilityRuleRepository.Create(availabilityRule);
        }
        /// <summary>
        /// Deletes existing availability rule with given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(long id){
            return availabilityRuleRepository.Delete(id);
        }
    }
}
