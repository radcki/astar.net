using System.Collections.Generic;
using AstarNet.Solver.Model;
using AstarNet.Solver.ValueObjects;


public class GridTravelVertex : ITravelVertex<GridCoord>
{
    public GridTravelVertex(GridCoord position)
    {
        Position = position;
        if (position.X > 0 && position.Y > 0)
        {
            _neighbors.Add(new GridCoord(position.X - 1, position.Y - 1));
        }

        if (position.X > 0)
        {
            _neighbors.Add(new GridCoord(position.X - 1, position.Y));
            _neighbors.Add(new GridCoord(position.X - 1, position.Y + 1));
        }

        if (position.Y > 0)
        {
            _neighbors.Add(new GridCoord(position.X, position.Y - 1));
            _neighbors.Add(new GridCoord(position.X + 1, position.Y - 1));
        }

        _neighbors.Add(new GridCoord(position.X + 1, position.Y));
        _neighbors.Add(new GridCoord(position.X, position.Y + 1));
        _neighbors.Add(new GridCoord(position.X + 1, position.Y + 1));
    }

    /// <inheritdoc />
    public GridCoord Position { get; }


    /// <inheritdoc />
    public IEnumerable<GridCoord> Neighbors => _neighbors;

    private readonly HashSet<GridCoord> _neighbors = new HashSet<GridCoord>();

 
    /// <inheritdoc />
    public bool CanMoveDirectlyTo(GridCoord other)
    {
        return _neighbors.Contains(other);
    }
}