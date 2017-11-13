using System;

[Serializable]
public class Figure
{
    public readonly Block[] Blocks;

    public int X = -1;
    public int Y = -1;
    public int CurrentSet;

    public RotationSet[] RotationSets;
    
    public Figure()
    {
        
    }
    
    public Figure(Block[] blocks)
    {
        Blocks = blocks;
    }

    //copy from desc
    public Figure(Figure currentFigureDesc)
    {
        RotationSets = currentFigureDesc.RotationSets;
        var rotationSet = RotationSets[0];
        Blocks = new Block[rotationSet.Size.Count];
        for (int i = 0; i < Blocks.Length; i++)
        {
            var block = new Block();
            Blocks[i] = block;
            block.Position = rotationSet[i];
        }
    }

    public void Rotate(int nextSetIndex)
    {
        var set = RotationSets[nextSetIndex];
        for (var index = 0; index < Blocks.Length; index++)
        {
            var block = Blocks[index];
            block.Position = set[index];
        }
        CurrentSet = nextSetIndex;
    }
}