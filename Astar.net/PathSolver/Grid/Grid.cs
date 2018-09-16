using Astar.net.PathSolver.Parameters;
using System;
using System.Collections.Generic;

namespace Astar.net.PathSolver
{
    public class Grid
    {
        public byte[,] BlockedPositions { get; }
        public int SizeX { get; }
        public int SizeY { get; }
        public float BaseTraverseCost { get; }
        public float DiagonalTraverseCost { get { return (float) (BaseTraverseCost * 1.414); } }

        public Grid(int sizeX, int sizeY, float traverseCost)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            BaseTraverseCost = traverseCost;
            BlockedPositions = new byte[SizeX+1,SizeY+1];
        }

        public void BlockPosition(Position position)
        {
            BlockedPositions[position.X, position.Y] = 1;
        }
        
        public void BlockPosition(int x, int y)
        {
            BlockedPositions[x, y] = 1;
        }

        public bool IsPositionAvailable(Position position)
        {
            return position.X >= 0 
                && position.Y >= 0 
                && position.X <= SizeX 
                && position.Y <= SizeY
                && BlockedPositions[position.X, position.Y] == 0;
        }

        public bool IsPositionAvailable(int x, int y)
        {
            return x>=0 
                && y>=0 
                && x<=SizeX 
                && y<=SizeY
                && BlockedPositions[x, y] == 0;
        }

        /// <summary>
        /// Sprawdzone zostaje czy sąsiadujące komórki nie są na liście zablokowanych i czy nie
        /// wykraczają poza siatkę
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        public List<Position> GetAvailableNeighbours(Position position, bool traverse)
        {
            var availableNeigbours = new List<Position>();

            //Południe
            if (IsPositionAvailable(position.X, position.Y+1))
            {
                availableNeigbours.Add(new Position(position.X, position.Y + 1, BaseTraverseCost));
            }

            //Zachód
            if (IsPositionAvailable(position.X+1, position.Y))
            {
                availableNeigbours.Add(new Position(position.X+1, position.Y, BaseTraverseCost));
            }

            //Północ
            if (IsPositionAvailable(position.X,position.Y-1))
            {
                availableNeigbours.Add(new Position(position.X, position.Y-1, BaseTraverseCost));
            }
            

            //Wschód
            if (IsPositionAvailable(position.X-1, position.Y))
            {
                availableNeigbours.Add(new Position(position.X-1, position.Y, BaseTraverseCost));
            }            
            
            if (traverse)
            {
                //Północ-Zachód
                if (IsPositionAvailable(position.X-1, position.Y-1))
                {
                    availableNeigbours.Add(new Position(position.X-1, position.Y-1, DiagonalTraverseCost));
                }

                //Południe-Wschód
                if (IsPositionAvailable(position.X+1, position.Y+1))
                {
                    availableNeigbours.Add(new Position(position.X+1, position.Y+1, DiagonalTraverseCost));
                }

                //Południe-Zachód
                if (IsPositionAvailable(position.X-1, position.Y+1))
                {
                    availableNeigbours.Add(new Position(position.X-1, position.Y+1, DiagonalTraverseCost));
                }

                //Północ-Wschód
                if (IsPositionAvailable(position.X+1, position.Y-1))
                {
                    availableNeigbours.Add(new Position(position.X+1, position.Y-1, DiagonalTraverseCost));
                }
            } 
            

            return availableNeigbours;
        }
    }
}
