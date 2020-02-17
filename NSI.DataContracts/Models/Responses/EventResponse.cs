
namespace NSI.DataContracts.Models.Responses
{
    public class EventResponse
    {
        /// <summary>
        /// Unique identifier for Event
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// Name of the Event
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Event type (See Model: EventTypeResponse)
        /// </summary>        
        public EventTypeResponse Type { get; set; }
    }
}
