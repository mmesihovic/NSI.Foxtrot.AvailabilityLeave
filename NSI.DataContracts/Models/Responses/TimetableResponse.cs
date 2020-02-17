using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NSI.DataContracts.Models.Responses
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class TimetableResponse
    {
        /// <summary>
        /// Resource ID
        /// </summary>
        [DataMember]
        public long ResourceID { get; set; }
        /// <summary>
        /// Is Resource Available? Boolean
        /// </summary>
        [DataMember]
        public bool Available { get; set; }
        /// <summary>
        /// If Resource is not available, details for each event are included in the response
        /// </summary>
        [DataMember]
        public ICollection<AbsenceDetails> AbsenceDetails { get; set; } 
    }
}
