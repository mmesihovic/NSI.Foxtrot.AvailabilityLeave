using System.ComponentModel.DataAnnotations.Schema;

namespace NSI.DAL.Entities
{
    [Table("LeaveTransaction", Schema = "availabilityleave")]
    public class LeaveTransaction : ExtendedBase
    {
        public long ID { get; set; }
        public long Days { get; set; }
        public LeaveBalance LeaveBalance { get; set; }
        public long ApprovedByID { get; set; }
    }
}
