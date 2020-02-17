using System.ComponentModel.DataAnnotations;

namespace NSI.DataContracts.Models.Requests
{
    public class AvailabilityRuleRequest
    {   
        [Required]
        public long ResourceID { get; set; }
        [Required]
        public string WeekDay { get; set; }
        [Required]
        [RegularExpression("^(20|21|22|23|[01]\\d|\\d)([:][0-5]\\d)$")]
        public string FromTime { get; set; }
        [Required]
        [RegularExpression("^(20|21|22|23|[01]\\d|\\d)([:][0-5]\\d)$")]
        public string ToTime { get; set; }
        [Required]
        public bool Available { get; set; }
    }
}
