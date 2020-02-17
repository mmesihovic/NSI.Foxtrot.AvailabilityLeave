using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NSI.DAL.Entities
{
    [Table("ResourceSchedule", Schema = "availabilityleave")]
    public class ResourceSchedule : ExtendedBase
    {
        public long ID { get; set; }
        public long ResourceID { get; set; }
        [Required]
        public bool Approved { get; set; }
        public long? ApprovedByID { get; set; }
        [Required]
        public long RequestByID { get; set; }
        [Required]
        [ForeignKey("EventID")]
        virtual public Event Event { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime FromDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UntilDate { get; set; }
    }
}
