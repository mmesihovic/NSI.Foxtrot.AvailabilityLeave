using System.Collections.Generic;

namespace NSI.DataContracts.Models.Requests
{
    public class IdListRequest
    {
        public IEnumerable<long> IDs { get; set; }
        
    }
}
