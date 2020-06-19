using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVirtualizer
{
    public class VisualizerURLBuilder
    {
        public static string BuildURL(Ship ship)
        {
            string url = "https://i872272core.venus.fhict.nl/ContainerVisualizer/index.html?";
            string dimensions = "length=" + ship.rows.Count + "&width=" + ship.rows[0].stacks.Count;
            string stacks = "&stacks=";
            string Weights = "&Weights=";

            for(int i = 0; i < ship.rows[0].stacks.Count; i++)
            {
                for(int j = 0; j < ship.rows.Count; j++)
                {
                    foreach(IContainer container in ship.rows[j].stacks[i].containersInStack)
                    {
                        if(container is NormalContainer)
                        {
                            stacks += "1";
                        }
                        else if(container is ValuableContainer)
                        {
                            stacks += "2";
                        }
                        else if(container is CoolableContainer)
                        {
                            stacks += "3";
                        }
                        if (container != ship.rows[j].stacks[i].containersInStack.Last())
                        {
                            Weights += container.Weight + "-";
                        }
                        else
                        {
                            Weights += container.Weight;
                        }
                    }
                    if (j != ship.rows.Count - 1)
                    {
                        stacks += ",";
                        Weights += ",";
                    }
                }
                if (i != ship.rows[0].stacks.Count - 1)
                {
                    stacks += "/";
                    Weights += "/";
                }
            }
            url += dimensions + stacks + Weights;
            return url;
        }
    }
}
