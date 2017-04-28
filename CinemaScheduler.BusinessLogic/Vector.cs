using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaScheduler.BusinessLogic
{
    [Serializable]
    public class Vector
    {
        public string Episode { get; private set; }

        public string Actor { get; private set; }
        public float Ring { get; private set; }
        public float Lines { get; private set; }
        public float ReadingTimeMinutes { get { return Lines * PriceSettings.LinesPerMinute; } }
        public bool IsIterable { get; set; }
        public SummaryPruner Pruner { get; set; }
        public Vector(string actor, float ring, float lines, string episode)
        {
            this.Actor = actor;
            this.Ring = ring;
            this.Lines = lines;
            this.Episode = episode;
        }

        public override string ToString()
        {
            return $"({Episode},{Actor}, {Ring}, {Lines})";
        }

        public float CostCompare(Vector other)
        {
            float r1, r2;

            //ATTORE E ATTORE
            r1 = actorCost(other);
            //FRA RIGA E RIGA
            r2 = PriceSettings.LinePrice * (this.Lines + other.Lines);
            return r1 + r2;
        }

        private float actorCost(Vector other)
        {
            float r1;
            if (other.Actor == this.Actor)
            {
                r1 = PriceSettings.PresenceCoin;
            }
            else
            {
                r1 = 2 * PriceSettings.PresenceCoin;
            }

            return r1;
        }
    }

    public struct CostVector
    {
        public Vector Vector { get; set; }
        public float Cost { get; set; }
        public CostVector(Vector vector, float cost)
        {
            this.Vector = vector;
            this.Cost = cost;
        }
    }

}
