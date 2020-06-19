using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerVirtualizer
{
    public class Ship
    {
        public List<Place> rows;
        public int maxWeight;
        public int minWeight;

        public Ship(int length, int width)
        {
            this.rows = new List<Place>();
            this.maxWeight = (length * width) * 150;
            this.minWeight = maxWeight / 2;

            // Voor de lengte maken we rijen. De lengte van de rij wordt bepaald door de breedte van het schip.
            // De eerste rij is altijd koel en waardevol. De laatste rij is altijd waardevol.
            for (int i = 0; i < length; i++)
            {
                if (i == 0)
                {
                    rows.Add(new Place(width, true, true, i));
                }
                else if (i == length - 1)
                {
                    rows.Add(new Place(width, false, true, i));
                }
                else
                {
                    rows.Add(new Place(width, false, false, i));
                }
            }
        }

        public void PlaceAllContainers(List<IContainer> containers)
        {
            // Controleer of het totale gewicht van de containers binnen de vereiste grenzen valt
            if (TotalContainerWeightPasses(containers))
            {
                // Voor elke container gaan we een beschikbare stapel zoeken. Gebaseerd op balans, beschikbaarheid en type container.
                // Als er geen stapel beschikbaar is, negeren we de container 
                foreach (IContainer container in containers)
                {
                    Container stack = GetAvailableStack(container);
                    if (stack == null)
                    {
                        Console.WriteLine("Ignored a " + container.GetType().ToString() + " | Weight: " + container.Weight + " | No spot available");
                    }
                    else
                    {
                        stack.PlaceContainer(container);
                    }
                }
            }
        }

        private bool TotalContainerWeightPasses(List<IContainer> containers)
        {
            int Weight = 0;
            foreach (IContainer container in containers)
            {
                Weight += container.Weight;
            }
            if (Weight >= minWeight && Weight <= maxWeight)
            {
                return true;
            }
            else
            {
                throw new Exception("The total Weight of containers is not within requirement bounds");
            }
        }

        private Container GetAvailableStack(IContainer container)
        {
            // Als de container koelbaar is, negeren we balanceren omdat ze altijd op de eerste rij staan.
            // Als het schip momenteel in balans is, zoeken we een plekje voor de container in het voorste rij.
            if (container is CoolableContainer || HalfWeightsWithinBounds(GetHalfWeights()))
            {
                foreach (Place row in rows)
                {
                    foreach (Container stack in row.stacks)
                    {
                        if (stack.CanContainerBePlaced(container))
                        {
                            return stack;
                        }
                    }

                }
            }
            // Als het schip uit balans is
            else
            {
                //Zoek naar een container op de andere helft van het schip.
                for (int i = rows.Count - 1; i >= 0; i--)
                {
                    for (int j = rows[i].stacks.Count - 1; j >= 0; j--)
                    {
                        if (rows[i].stacks[j].CanContainerBePlaced(container))
                        {
                            return rows[i].stacks[j];
                        }
                    }
                }
            }

            // Als er geen plaats beschikbaar is op het schip, keren we null terug.
            return null;
        }

        public int GetTotalWeight()
        {
            int Weight = 0;
            foreach (Place row in rows)
            {
                Weight += row.GetTotalRowWeight();
            }
            return Weight;
        }

        // Deze methode retourneert true als de gewichtsverschillen binnen de vereiste grenzen liggen.
        // Het neemt de berekende gewichten van elke helft als parameter
        public bool HalfWeightsWithinBounds(int[] halfWeights)
        {
            int frontWeight = halfWeights[0];
            int backWeight = halfWeights[1];

            double frontPercentage = (double)frontWeight / (double)GetTotalWeight() * 100.0;
            double backPercentage = (double)backWeight / (double)GetTotalWeight() * 100.0;

            if (frontPercentage > 60 || frontPercentage < 40 || backPercentage > 60 || backPercentage < 40)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int[] GetHalfWeights()
        {
            int frontWeight = 0;
            int backWeight = 0;

            double rowsDivBy2 = rows.Count / 2.0;
            // Als het aantal rijen een even aantal is, kunnen we gemakkelijk de gewichten van elke helft krijgen.
            if (rowsDivBy2 % 1 == 0)
            {
                for (int i = 0; i < rowsDivBy2; i++)
                {
                    frontWeight += rows[i].GetTotalRowWeight();
                    backWeight += rows[i + (int)rowsDivBy2].GetTotalRowWeight();
                }
            }

            // Als het aantal rijen geen even aantal is, verdelen we het totale gewicht van de middelste rij en geven we elke helft van het schip een helft van het gewicht van de middelste rij.
            else
            {
                int middlerow = rows.Count / 2;
                int halfWeightOfMiddleRow = rows[middlerow].GetTotalRowWeight() / 2;

                for (int i = 0; i < rows.Count; i++)
                {
                    if (i == middlerow)
                    {
                        frontWeight += halfWeightOfMiddleRow;
                        backWeight += halfWeightOfMiddleRow;
                    }
                    else if (i < middlerow)
                    {
                        frontWeight += rows[i].GetTotalRowWeight();
                    }
                    else
                    {
                        backWeight += rows[i].GetTotalRowWeight();
                    }
                }
            }

            //Geven de gewichten terug zodat we ze kunnen gebruiken in onze methode die controleert of het schip in balans is.
            int[] Weights = { frontWeight, backWeight };
            return Weights;
        }
    }
}
