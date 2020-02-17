using System.Runtime.Serialization;

namespace NSI.DataContracts.Models.Responses
{
    [DataContract]
    public class AvailabilityRuleResponse
    {   
        /// <summary>
        /// Unique Identifier for Availability Rule
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Which resource does the rule apply to?
        /// </summary>
        public long ResourceID { get; set; }
        /// <summary>
        /// Which day?
        /// </summary>
        public string WeekDay { get; set; }
        /// <summary>
        /// Start Time
        /// </summary>
        public string FromTime { get; set; }
        /// <summary>
        /// End Time
        /// </summary>
        public string ToTime { get; set; }
        /// <summary>
        /// Is resource available in this period or not? (Boolean)
        /// </summary>
        public bool Available { get; set; }
    }
}
