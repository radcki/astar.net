using System;

namespace AstarNet.Solver.ValueObjects
{
    public interface ICoord<T> : IEquatable<T>
    {
        float DistanceTo(T other);
    }
}