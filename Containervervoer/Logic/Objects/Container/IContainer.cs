using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVirtualizer
{
    public interface IContainer
    {
        bool AllowTopStack { get; }
        int MaxTopStackWeight{ get; }
        int Weight { get; set; }
        int MaxWeight { get; }
        List<Place> AllowedRows { get; set; }
    }
}
