using NSI.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.Helpers.Interfaces
{
    public interface ISortable
    {
        IList<SortCriteria> SortCriteria { get; set; }
    }
}
