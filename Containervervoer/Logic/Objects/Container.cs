using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVirtualizer
{
    public class Container
    {
        readonly int maxWeight = 150;
        public List<IContainer> containersInStack;
        public bool valuableContainerInStack;
        public bool isCoolableStack;
        public bool isValuableStack;

        public Container(bool coolable, bool valuable)
        {
            this.isValuableStack = valuable;
            this.isCoolableStack = coolable;
            this.containersInStack = new List<IContainer>();
        }

        public void PlaceContainer(IContainer container)
        {
            if (CanContainerBePlaced(container))
            {
                if(container is ValuableContainer)
                {
                    this.valuableContainerInStack = true;
                }
                this.containersInStack.Add(container);
                SortStack();
            }
        }

        public int GetTotalStackWeight()
        {
            int Weight = 0;
            foreach(IContainer container in containersInStack)
            {
                Weight += container.Weight;
            }
            return Weight;
        }

        // Parent method of private checks
        public bool CanContainerBePlaced(IContainer container)
        {
            if(StackWontOverload(container) && ContainerIsSuitable(container))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ContainerIsSuitable(IContainer container)
        {
            if(container is CoolableContainer)
            {
                if(isCoolableStack == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            else if(container is ValuableContainer)
            {
                if (valuableContainerInStack)
                {
                    return false;
                }
                else
                {
                    if (isCoolableStack == true)
                    {
                        return true;
                    }
                    else if (isValuableStack == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            else
            {
                return true;
            }
        }

        // Stack won't exceed 150 tons
        private bool StackWontOverload(IContainer container)
        {
            int currentWeight= GetTotalStackWeight();
            if(currentWeight + container.Weight <= this.maxWeight && NotTooMuchWeightOnTop(container))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Check if a container doensn't have over 120 stacked on top
        private bool NotTooMuchWeightOnTop(IContainer container)
        {
            for(int i = 0; i < containersInStack.Count; i++)
            {
                int topWeight = 0;
                List<IContainer> containersOnTop = containersInStack.GetRange(i + 1, containersInStack.Count - i - 1);
                containersOnTop.Add(container);
                foreach(IContainer stackedContainer in containersOnTop)
                {
                    topWeight += stackedContainer.Weight;
                }
                if(topWeight > 120)
                {
                    return false;
                }
            }
            return true;
        }

        // If there is a valuable container in the stack we put it on top
        private void SortStack()
        {
            if (valuableContainerInStack)
            {
                ValuableContainer vContainer = (ValuableContainer)containersInStack.Find(v => v.AllowTopStack == false);
                this.containersInStack.Remove(vContainer);
                this.containersInStack.Add(vContainer);
            }
        }
    }
}
