using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVirtualizer
{
    public class ValuableContainer: IContainer
    {
        int _Weight= 4;
        public bool AllowTopStack { get { return false; } }
        public int MaxTopStackWeight { get { return 120; } }
        public int Weight { get { return this._Weight; } set { this._Weight += value; } }
        public int MaxWeight { get { return 30; } }
        public List<Place> AllowedRows { get; set; }
    }
}
