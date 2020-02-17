using System;

namespace NSI.DataContracts.Models.Responses
{
    public class ScheduleResponse
    {
        /// <summary>
        /// Unique Identifier for Schedule entry
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Which resource does this schedule entry bind to (Unique Identified)
        /// </summary>
        public long ResourceID { get; set; }
        /// <summary>
        /// Who requested this event that caused resource not to be available? (ID)
        /// </summary>
        public long RequestByID { get; set; }
        /// <summary>
        /// Who approved it?
        /// </summary>
        public long? ApprovedByID { get; set; }
        /// <summary>
        /// Is event approved?
        /// </summary>
        public Boolean Approved { get; set; }
        /// <summary>
        /// Start Date and Time
        /// </summary>
        public DateTime From { get; set; }
        /// <summary>
        /// End Date and Time
        /// </summary>
        public DateTime Until { get; set; }
        /// <summary>
        /// Event Details (See Model: EventResponse)
        /// </summary>
        public EventResponse Event { get; set; }
    }
}
