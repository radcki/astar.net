
using System;

namespace AstarNet.Solver.ValueObjects
{
    public readonly struct Coord : ICoord<Coord>
    {
        public Coord(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; }
        public float Y { get; }

        public float DistanceTo(Coord other)
        {
            return (float)Math.Sqrt((X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y));
        }

        /// <inheritdoc />
        public bool Equals(Coord other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is Coord other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(Coord left, Coord right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Coord left, Coord right)
        {
            return !left.Equals(right);
        }
    }
}