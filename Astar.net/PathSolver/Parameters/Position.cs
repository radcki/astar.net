using System;

namespace Astar.net.PathSolver.Parameters
{
    public class Position : IEquatable<Position>
    {
        public short X, Y;
        public float TraverseCost { get;}
        public int CostFromStart { get; set; }
        public int CostEstimation { get; set; }
        public Position Parent;

        public Position(short x, short y, float traverseCost)
        {
            X = x;
            Y = y;
            TraverseCost = traverseCost;
        }

        public Position(int x, int y, float traverseCost)
        {
            X = (short)x;
            Y = (short)y;
            TraverseCost = (float)traverseCost;
        }

        public float ManhattanDistanceTo (Position other)
        {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
        }

        public float EuclidianDistanceTo(Position other)
        {
            return (float) (Math.Pow(Math.Abs(X - other.X),2) + Math.Pow(Math.Abs(Y - other.Y),2));
        }

        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public bool Equals(short x, short y)
        {
            return X == x && Y == y;
        }

        public bool Equals(int x, int y)
        {
            return X == x && Y == y;
        }

        public static bool operator ==(Position a, Position b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Position a, Position b)
        {
            return !a.Equals(b);
        }
    }
}
