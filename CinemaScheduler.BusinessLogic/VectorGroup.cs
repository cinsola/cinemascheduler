using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaScheduler.BusinessLogic
{
    [Serializable]
    public class VectorGroup : List<Vector>, ICloneable
    {
        public float TurnCostAggregator(Vector vector)
        {
            VectorGroup clonedGroup = (this.Clone() as VectorGroup);
            clonedGroup.Add(vector);
            return clonedGroup.Cost;
        }

        public float LinesForActor(string actor)
        {
            return this.Where(x => x.Actor == actor).Sum(x => x.Lines);
        }

        public override string ToString()
        {
            return "Group with " + this.Count() + " vectors";
        }

        public float Lines
        {
            get { return this.Sum(x => x.Lines); }
        }
        public int Actors
        {
            get { return this.Select(x => x.Actor).Distinct().Count(); }
        }
        public float ReadingTimeMinutes
        {
            get { return this.Sum(x => x.ReadingTimeMinutes); }
        }
        public float Cost
        {
            get
            {
                IEnumerable<string> actors = this.Select(x => x.Actor).Distinct().ToList();
                float coinsPrice = (float)this.Select(x => x.Actor).Distinct().Count() * PriceSettings.PresenceCoin;
                float linesPrice = this.Sum(x => x.Lines) * PriceSettings.LinePrice;

                float tooManyLines = 0;
                foreach (string actor in actors)
                {
                    bool sumReached = false;
                    float actorLines = LinesForActor(actor);
                    if (actorLines >= PriceSettings.MaxLinesPerTurnPerActor)
                    {
                        tooManyLines += PriceSettings.PresenceCoin;
                        sumReached = true;
                    }
                    if (sumReached == false && actorLines >= PriceSettings.SeparatedColumnLimit && actorLines >= PriceSettings.MaxLinesPerTurn / 2f)
                    {
                        tooManyLines += PriceSettings.PresenceCoin;
                    }
                }

                return coinsPrice + linesPrice + tooManyLines;
            }
        }
        public void AddAndRemoveForAvailable(Vector vector, SummaryPruner pruner)
        {
            pruner.Remove(vector);
            this.Add(vector);
        }

        public bool AddBest(SummaryPruner pruner)
        {
            float minCost = float.PositiveInfinity;
            Vector vector = null;
            foreach (Vector currentVector in pruner)
            {
                if (this.IsFillableWith(currentVector))
                {
                    float currentCost = this.TurnCostAggregator(currentVector);
                    if (this.TurnCostAggregator(currentVector) < minCost)
                    {
                        minCost = currentCost;
                        vector = currentVector;
                    }
                }
            }

            if (vector == null) { return false; }
            else
            {
                this.AddAndRemoveForAvailable(vector, pruner);
                return true;
            }
        }

        public bool IsFull
        {
            get
            {
                if (this.Sum(x => x.Lines) >= PriceSettings.MaxLinesPerTurn) return true;
                if (this.Sum(x => x.ReadingTimeMinutes) >= PriceSettings.MaxTurnDuration) return true;
                return false;
            }
        }

        public bool IsFillableWith(Vector newVector)
        {
            if ((this.Sum(x => x.Lines) + newVector.Lines <= PriceSettings.MaxLinesPerTurn)
                &&
                (this.Sum(x => x.ReadingTimeMinutes) + newVector.ReadingTimeMinutes <= PriceSettings.MaxTurnDuration))
                return true;
            return false;
        }

        public object Clone()
        {
            VectorGroup clonedGroup = new VectorGroup();
            clonedGroup.AddRange(this);
            return clonedGroup;
        }
    }

    [Serializable]
    public class VectorGroupCollection : List<VectorGroup>
    {
    }

}
