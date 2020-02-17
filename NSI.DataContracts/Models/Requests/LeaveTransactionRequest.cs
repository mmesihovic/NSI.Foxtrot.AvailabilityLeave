using System.ComponentModel.DataAnnotations;

namespace NSI.DataContracts.Models.Requests
{
    public class LeaveTransactionRequest
    {
        [Required]
        public long Days { get; set; }
        [Required]
        public long LeaveBalanceID { get; set; }
        [Required]
        public long ApprovedByID { get; set; }
    }
}
