using System.Collections.Generic;
using AstarNet.Solver.ValueObjects;

namespace AstarNet.Solver.Model
{
    public class GraphTravelVertex : ITravelVertex<Coord>
    {
        public GraphTravelVertex(Coord position)
        {
            Position = position;
   
        }

        /// <inheritdoc />
        public Coord Position { get; }


        /// <inheritdoc />
        public IEnumerable<Coord> Neighbors => _neighbors;

        private readonly HashSet<Coord> _neighbors = new();

 
        /// <inheritdoc />
        public bool CanMoveDirectlyTo(Coord other)
        {
            return _neighbors.Contains(other);
        }

        public void SetNeighbors(IEnumerable<Coord> neighbors)
        {
            foreach (var neighbor in neighbors)
            {
                _neighbors.Add(neighbor);
            }
        }
    }
}