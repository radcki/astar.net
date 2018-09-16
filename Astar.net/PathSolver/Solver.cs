using System;
using System.Diagnostics;
using Astar.net.PathSolver.HeapList;
using Astar.net.PathSolver.Parameters;

namespace Astar.net.PathSolver
{
    public class Solver
    {
        #region Privates

        private readonly Grid _grid;

        #endregion

        #region Constructors

        public Solver(Grid grid)
        {
            _grid = grid;
        }

        #endregion

        #region Methods

        public PathFindingResult FindPath(Position startPosition, Position endPosition, bool traverse)
        {
            var result = new PathFindingResult
                         {
                             PathCoordinates = new byte[_grid.SizeX + 1, _grid.SizeY + 1],
                             CheckedCoordinates = new byte[_grid.SizeX + 1, _grid.SizeY + 1],
                         };

            if (!_grid.IsPositionAvailable(startPosition))
            {
                throw new Exception("Start position unavailable");
            }

            if (!_grid.IsPositionAvailable(endPosition))
            {
                throw new Exception("End position unavailable");
            }

            var openList = new Heap();

            // lista zamkniętych lokalizacji w postaci tablicy bitów diametralnie skraca czas wykonania
            // algorytmu względem List<Position> (3 min -> 0,3 sec !!)
            var closedList = new byte[_grid.SizeX + 1, _grid.SizeY + 1];
            var addedToOpenList = new int[_grid.SizeX + 1, _grid.SizeY + 1];

            var currentPosition = startPosition;
            openList.Add(new HeapNode(currentPosition, currentPosition.TraverseCost * currentPosition.EuclidianDistanceTo(endPosition)));

            var stepCount = 0;
            while (openList.HasMore())
            {
                stepCount++;
                currentPosition = openList.TakeHeapHeadPosition();
                if (currentPosition == endPosition) // sukces!
                {
                    break;
                }
                closedList[currentPosition.X, currentPosition.Y] = 1;

                // wyszukaj możliwe i niewykorzystane możliwości ruchów
                var movementOptions = _grid.GetAvailableNeighbours(currentPosition, traverse);

                // dodaj wyszukane do stosu
                foreach (var position in movementOptions)
                {
                    result.CheckedCoordinates[position.X, position.Y] = 1;

                    if (closedList[position.X, position.Y] == 1)
                    {
                        continue;
                    }


                    var tentativeGScore = currentPosition.CostFromStart + currentPosition.EuclidianDistanceTo(position);
                    var gScore = currentPosition.CostFromStart + position.TraverseCost;
                    var cost = gScore + position.TraverseCost * position.EuclidianDistanceTo(endPosition);
                    position.Parent = currentPosition;
                    position.CostFromStart = (int)gScore;

                    if (addedToOpenList[position.X, position.Y] == 0)
                    {                        
                        openList.Add(new HeapNode(position, cost));
                        addedToOpenList[position.X, position.Y] = (int) cost;
                    }
                    else if (addedToOpenList[position.X, position.Y] != 0 && addedToOpenList[position.X, position.Y] > tentativeGScore)
                    {
                        openList.Reinsert(new HeapNode(position, cost));
                    }
                }
            }

            result.Success = currentPosition == endPosition;
            result.FinalCost = currentPosition.CostFromStart;

            Debug.WriteLine("Wykonano krokow: " + stepCount + ". Wynik końcowy: " + (currentPosition == endPosition ? "POWODZENIE" : "NIEPOWODZENIE"));
            
            // powrót po śladach
            while (currentPosition != startPosition)
            {
                var nextPosition = currentPosition.Parent;
                currentPosition.Parent = null;
                result.PathCoordinates[currentPosition.X, currentPosition.Y] = 1;

                currentPosition = nextPosition;
            }

            return result;
        }

        #endregion
    }
}