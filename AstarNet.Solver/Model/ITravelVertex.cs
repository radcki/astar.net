using System.Collections.Generic;
using AstarNet.Solver.ValueObjects;

namespace AstarNet.Solver.Model
{
    public interface ITravelVertex<T> where T : ICoord<T>
    {
        T Position { get; }
        IEnumerable<T> Neighbors { get; }

        bool CanMoveDirectlyTo(T other);
    }
}