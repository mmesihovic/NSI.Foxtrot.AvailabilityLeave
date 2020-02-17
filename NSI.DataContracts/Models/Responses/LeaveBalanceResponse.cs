
namespace NSI.DataContracts.Models.Responses
{
    public class LeaveBalanceResponse
    {
        /// <summary>
        /// Unique Identifier for Leave Balance in Database
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Which resource does Leave Balance bind to? (ID) 
        /// </summary>
        public long ResourceID { get; set; }
        /// <summary>
        /// How much days does resource have for Leave?
        /// </summary>
        public long AvailableDays { get; set; }
        /// <summary>
        /// How much additional days does resource gain monthly?
        /// </summary>
        public long MonthlyGain { get; set; }
        /// <summary>
        /// How much additional days does resource gain yearly?
        /// </summary>
        public long YearlyGain { get; set; }
        
    }
}
