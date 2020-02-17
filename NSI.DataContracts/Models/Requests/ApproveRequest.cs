using System.ComponentModel.DataAnnotations;

namespace NSI.DataContracts.Models.Requests
{
    public class ApproveRequest
    {
        [Required]
        public long ScheduleID { get; set; }
        [Required]
        public long ApprovedByID { get; set; }
    }
}
