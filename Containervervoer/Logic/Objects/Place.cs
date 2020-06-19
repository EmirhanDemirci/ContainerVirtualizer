using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVirtualizer
{
    public class Place
    {
        public int positionOnShip;
        public List<Container> stacks;
        public bool isCoolableRow;
        public bool isValuableRow;

        public Place(int length, bool coolable, bool valuable, int position)
        {
            this.positionOnShip = position;
            this.isValuableRow = valuable;
            this.isCoolableRow = coolable;
            this.stacks = new List<Container>();

            // We vullen de rij met containers en geven ze een bool waarde om te bepalen of het waardevolle of koelbare stapels zijn.
            for (int i = 0; i < length; i++)
            {
                if (this.isCoolableRow == true)
                {
                    stacks.Add(new Container(true, true));
                }
                else if(this.isValuableRow == true && this.isCoolableRow == false)
                {
                    stacks.Add(new Container(false, true));
                }
                else
                {
                    stacks.Add(new Container(false, false));
                }
            }
        }

        public int GetTotalRowWeight()
        {
            int Weight = 0;
            foreach(Container stack in stacks)
            {
                Weight += stack.GetTotalStackWeight();
            }
            return Weight;
        }
    }
}
