using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NSI.DAL.Entities
{
    [Table("Event", Schema = "availabilityleave")]
    public class Event : ExtendedBase
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [ForeignKey("EventType")]
        virtual public EventType Type { get; set; }
    }
}
