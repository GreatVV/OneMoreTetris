using System;

[Serializable]
public class Figure
{
    public readonly Block[] Blocks;

    public int X;
    public int Y;
    public int CurrentSet;

    public RotationSet[] RotationSets;
    
    public Figure()
    {
        
    }
    
    public Figure(Block[] blocks)
    {
        Blocks = blocks;
    }

    public void Rotate(int nextSetIndex)
    {
        var set = RotationSets[nextSetIndex];
        for (var index = 0; index < Blocks.Length; index++)
        {
            var block = Blocks[index];
            block.Position = set[index];
        }
    }
}