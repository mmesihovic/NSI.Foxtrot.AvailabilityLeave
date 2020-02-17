using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NSI.DAL.Entities
{
    [Table("EventType", Schema = "availabilityleave")]
    public class EventType : Base
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}
