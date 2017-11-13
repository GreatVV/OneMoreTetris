public class CellState
{
    public bool IsTaken
    {
        get { return Figure != null; }
    }

    public Figure Figure;
    public Block Block;
}