using System.ComponentModel.DataAnnotations;

namespace NSI.DataContracts.Models.Requests
{
    public class EventRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public EventTypeRequest Type { get; set; }
    }
}
