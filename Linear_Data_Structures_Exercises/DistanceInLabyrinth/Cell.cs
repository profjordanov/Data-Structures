namespace DistanceInLabyrinth
{
    public class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int Moves { get; set; }
        public bool Visited { get; set; }

        public Cell(int row, int col, bool visited, int moves)
        {
            this.Row = row;
            this.Col = col;
            this.Visited = visited;
            this.Moves = moves;
        }
    }
}