
using System;
using System.ComponentModel.DataAnnotations;


namespace NSI.DataContracts.Models.Requests
{
    public class ScheduleRequestPost
    {
        [Required]
        public long ResourceID { get; set; }
        [Required]
        public long RequestByID { get; set; }
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime Until { get; set; }
        [Required]
        public long EventID { get; set; }
    }
}
