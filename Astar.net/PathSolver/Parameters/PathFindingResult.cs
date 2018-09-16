
namespace Astar.net.PathSolver.Parameters
{
    public class PathFindingResult 
    {
        public float FinalCost { get; set; }
        public byte[,] PathCoordinates { get; set; }
        public byte[,] CheckedCoordinates { get; set; }
        public bool Success { get; set; }
    }
}
