using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstarNet.Solver.Model;
using AstarNet.Solver.ValueObjects;
using Priority_Queue;

namespace AstarNet.Solver
{
    public class PathSolver
    {
        public PathSolvingResult<T> FindPath<T>(ITravelVertex<T> from, ITravelVertex<T> to, Dictionary<T, ITravelVertex<T>> travelVertices) where T : ICoord<T>
        {
            var openList = new FastPriorityQueue<TravelStep<T>>(1000);
            var addedToOpenList = new Dictionary<T, TravelStep<T>>();

            var current = new TravelStep<T>() { Position = from.Position };
            addedToOpenList.Add(current.Position, current);

            openList.Enqueue(current, float.MaxValue);
            while (openList.Count > 0)
            {
                current = openList.Dequeue();

                if (current.Position.Equals(to.Position)) break;

                if (!travelVertices.TryGetValue(current.Position, out var currentTravelVertex))
                {
                    break;
                }

                foreach (var candidatePosition in currentTravelVertex.Neighbors)
                {
                    var costFromStart = current.CostFromStart + candidatePosition.DistanceTo(current.Position);
                    var predictedCostToEnd = costFromStart + (candidatePosition.DistanceTo(to.Position) * 1.3f);

                    if (!addedToOpenList.TryGetValue(candidatePosition, out var candidateTravelStep))
                    {
                        candidateTravelStep = new TravelStep<T>()
                                              {
                                                  CostFromStart = costFromStart,
                                                  Parent = current,
                                                  EstimatedTotalCost = predictedCostToEnd,
                                                  Position = candidatePosition
                                              };

                        addedToOpenList.Add(candidatePosition, candidateTravelStep);
                        if (openList.MaxSize == openList.Count)
                        {
                            openList.Resize(openList.MaxSize + 1000);
                        }

                        openList.Enqueue(candidateTravelStep, predictedCostToEnd);
                    }
                    else if (predictedCostToEnd < candidateTravelStep.EstimatedTotalCost)
                    {
                        openList.UpdatePriority(candidateTravelStep, predictedCostToEnd);
                    }
                }
            }

            var success = current.Position.Equals(to.Position);
            if (success)
            {
                return new PathSolvingResult<T>()
                       {
                           Success = success,
                           Cost = current.CostFromStart,
                           Steps = current.ReconstructPositions().Reverse().ToList()
                       };
            }

            return new PathSolvingResult<T>()
                   {
                       Success = success,
                   };
        }
    }

    public class TravelStep<T> : FastPriorityQueueNode
    {
        public T Position { get; set; }
        public float CostFromStart { get; set; }
        public float EstimatedTotalCost { get; set; }
        public TravelStep<T> Parent { get; set; }

        public IEnumerable<T> ReconstructPositions()
        {
            var current = this;
            do
            {
                yield return current.Position;
            }
            while ((current = current.Parent) != null);
        }
    }

    public class PathSolvingResult<T>
    {
        public float Cost { get; set; }
        public bool Success { get; set; }
        public List<T> Steps { get; set; }
    }
}