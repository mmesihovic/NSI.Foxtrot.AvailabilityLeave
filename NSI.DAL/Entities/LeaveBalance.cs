using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace NSI.DAL.Entities
{
    [Table("LeaveBalance", Schema = "availabilityleave")]
    public class LeaveBalance : Base
    {
        public long ID { get; set; }
        public long ResourceID { get; set; }
        [Required]
        public long AvailableDays { get; set; }
        public long MonthlyGain { get; set; }
        public long YearlyGain { get; set; }
        
    }
}