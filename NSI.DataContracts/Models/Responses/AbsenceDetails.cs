using System;

namespace NSI.DataContracts.Models.Responses
{
    public class AbsenceDetails 
    {
        /// <summary>
        /// Unique Identifier for Scheduled 
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Who requested this event that caused resource not to be available? (ID)
        /// </summary>
        public long RequestByID { get; set; }
        /// <summary>
        /// Who approved it?
        /// </summary>
        public long? ApprovedByID { get; set; }
        /// <summary>
        /// Is Event approved?
        /// </summary>
        public Boolean Approved { get; set; }
        /// <summary>
        /// Start Time
        /// </summary>
        public DateTime From { get; set; }
        /// <summary>
        /// End Time
        /// </summary>
        public DateTime Until { get; set; }
        /// <summary>
        /// Event Details: Name and Type
        /// </summary>
        public EventResponse Event { get; set; }
    }
}
