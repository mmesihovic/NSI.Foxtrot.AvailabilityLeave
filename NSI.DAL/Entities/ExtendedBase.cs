using System;

namespace NSI.DAL.Entities
{
    public class ExtendedBase : Base
    {
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
    }
}
