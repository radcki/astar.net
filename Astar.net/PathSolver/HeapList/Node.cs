using Astar.net.PathSolver.Parameters;


namespace Astar.net.PathSolver.HeapList
{
    public class HeapNode
    {
        /// <summary>
        /// Następny na stosie węzeł
        /// </summary>
        public HeapNode NextNode { get; set; }


        public float EstimatedCost { get; }
        public Position Position { get; }

        public HeapNode(Position position, float estimatedCost)
        {
            EstimatedCost = estimatedCost;
            Position = position;
        }
    }
}
