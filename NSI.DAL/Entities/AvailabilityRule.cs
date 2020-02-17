using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NSI.DAL.Entities
{
    [Table("AvailabilityRule", Schema = "availabilityleave")]
    public class AvailabilityRule : ExtendedBase
    {
        public long ID { get; set; }
        [Required]
        public long ResourceID { get; set; }
        [Required]
        public string WeekDay { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime FromTime { get; set; }
        [Required]
        [DataType(DataType.Time)]
        public DateTime ToTime { get; set; }
        [Required]
        public bool Available { get; set; }
        
    }
}