
using System;

namespace AstarNet.Solver.ValueObjects
{
    public readonly struct GridCoord : ICoord<GridCoord>
    {
        public GridCoord(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        public uint X { get; }
        public uint Y { get; }

        #region Equality members

        /// <inheritdoc />
        public bool Equals(GridCoord other)
        {
            return X == other.X && Y == other.Y;
        }

        /// <inheritdoc />
        public float DistanceTo(GridCoord other)
        {
            return (float)Math.Sqrt((X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y));
        }

        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            return obj is GridCoord other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(GridCoord left, GridCoord right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GridCoord left, GridCoord right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}