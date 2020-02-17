using System;

namespace NSI.DAL.Entities
{
    public class Base
    {
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
