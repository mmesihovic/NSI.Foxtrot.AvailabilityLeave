using System;
using System.ComponentModel.DataAnnotations;

namespace NSI.DataContracts.Models.Requests
{
    public class ScheduleQuery
    {
        [Required]
        public long ResourceID { get; set; }
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime Until { get; set; }
    }
}
