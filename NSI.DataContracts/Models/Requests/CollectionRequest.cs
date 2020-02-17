using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NSI.DataContracts.Models.Requests
{
    public class CollectionRequest 
    {   
        [Required]
        public IEnumerable<long> IDs { get; set; }
        [Required]
        public DateTime From { get; set; }
        [Required]
        public DateTime Until { get; set; }
    }
}
