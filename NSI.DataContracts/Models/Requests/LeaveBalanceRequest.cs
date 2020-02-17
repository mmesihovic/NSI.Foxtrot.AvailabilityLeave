using System.ComponentModel.DataAnnotations;

namespace NSI.DataContracts.Models.Requests
{
    public class LeaveBalanceRequest
    {
        [Required]
        public long ResourceID { get; set; }
        public long? AvailableDays { get; set; }
        public long? MonthlyGain { get; set; }
        public long? YearlyGain { get; set; }
        
    }
}
