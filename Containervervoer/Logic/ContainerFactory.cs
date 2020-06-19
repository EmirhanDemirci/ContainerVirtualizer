using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVirtualizer
{
    public class ContainerFactory
    {
        public static List<IContainer> MakeContainers (int value, int cool, int normal)
        {
            List<IContainer> containers = new List<IContainer>();
            for (int v = 0; v < value; v++)
            {
                containers.Add(new ValuableContainer() { Weight = 26 });
            }
            for (int c = 0; c < cool; c++)
            {
                containers.Add(new CoolableContainer() { Weight = 26 });
            }
            for (int n = 0; n < normal; n++)
            {
                containers.Add(new NormalContainer() { Weight = 26 });
            }
            return containers;
        }
        public static List<IContainer> MakeContainersEmpty(int value, int cool, int normal)
        {
            List<IContainer> containers = new List<IContainer>();
            for (int v = 0; v < value; v++)
            {
                containers.Add(new ValuableContainer());
            }
            for (int c = 0; c < cool; c++)
            {
                containers.Add(new CoolableContainer());
            }
            for (int n = 0; n < normal; n++)
            {
                containers.Add(new NormalContainer());
            }
            return containers;
        }
    }
}
