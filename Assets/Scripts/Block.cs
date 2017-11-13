using UnityEngine;

public class Block
{
    public int X
    {
        get { return (int) Position.x; }
    }

    public int Y
    {
        get { return (int) Position.y; }
    }

    public Block()
    {
        
    }
    
    public Block(int x, int y)
    {
        Position = new Vector2(x,y);
        IsDestroyed = false;
    }
    
    public bool IsDestroyed { get; set; }

    public Vector2 Position { get; set; }
}