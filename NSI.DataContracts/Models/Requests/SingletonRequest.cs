using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace NSI.DataContracts.Models.Requests
{
    [DataContract]
    public class SingletonRequest
    {
        [DataMember]
        [Required]
        public long ResourceID { get; set; }
        [DataMember]
        [Required]
        public DateTime From { get; set; }
        [DataMember]
        [Required]
        public DateTime Until { get; set; }
    }
}
