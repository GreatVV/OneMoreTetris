using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class GameState
{
    public readonly int Width;
    public readonly int Height;

    public readonly CellState[] CellStates;

    public List<int> FullLines = new List<int>();

    public GameState(int width, int height)
    {
        Width = width;
        Height = height;
        CellStates = new CellState[width * height];
        for (int i = 0; i < width * height; i++)
        {
            CellStates[i] = new CellState();
        }
    }

    public bool IsTaken(int x, int y)
    {
        var index = GetIndex(x, y);

        if (index < 0 || index >= CellStates.Length)
        {
            return true;
        }
        
        return CellStates[index].IsTaken;
    }

    public int GetIndex(int x, int y)
    {
        return x * Height + y;
    }

    public Figure GetFigureAt(int x, int y)
    {
        var index = GetIndex(x, y);
        if (index < 0 || index > CellStates.Length - 1)
        {
            return null;
        }
        return CellStates[index].Figure;
    }

    public void MoveTo(Figure figure, int x, int y)
    {
        foreach (var cellState in CellStates)
        {
            if (cellState.Figure == figure)
            {
                cellState.Figure = null;
                cellState.Block = null;
            }
        }
        
        figure.X = x;
        figure.Y = y;
        foreach (var block in figure.Blocks)
        {
            var blockX = block.X + x;
            var blockY = block.Y + y;
            var index = GetIndex(blockX, blockY);
            var cellState = CellStates[index];
            cellState.Block = block;
            cellState.Figure = figure;
        }
        
    }


    public bool CanMoveTo(Figure figure, int x, int y)
    {
        if (y < 0 || x < 0 || x > Width - 1)
        {
            return false;
        }
        
        foreach (var block in figure.Blocks)
        {
            var globalPositionX = block.X + x;
            var globalPositionY = block.Y + y;
            if (!CheckPosition(globalPositionX, globalPositionY, figure)) return false;
        }
        return true;
    }

    private bool CheckPosition(int globalPositionX, int globalPositionY, Figure figure)
    {
        if (globalPositionX < 0 || globalPositionY < 0 || globalPositionX >= Width || globalPositionY >= Height)
        {
            return false;
        }

        if (GetFigureAt(globalPositionX, globalPositionY) == figure)
        {
            return true;
        }

        if (IsTaken(globalPositionX, globalPositionY))
        {
            return false;
        }
        return true;
    }


    public void RotateCounterClockwise(Figure figure)
    {
        var nextSetIndex = (figure.CurrentSet == figure.RotationSets.Length - 1) ? 0 : (figure.CurrentSet + 1);
        TryRotateToSet(figure,  nextSetIndex);
    }

    private void TryRotateToSet(Figure figure, int nextSetIndex)
    {
        var newSet = figure.RotationSets[nextSetIndex];
        foreach (var newPosition in newSet)
        {
            var globalPositionX = figure.X + (int) newPosition.x;
            var globalPositionY = figure.Y + (int) newPosition.y;

            if (!CheckPosition(globalPositionX, globalPositionY, figure))
                return;
        }

        figure.Rotate(nextSetIndex);

        MoveTo(figure, figure.X, figure.Y);
    }

    public void UpdateFullLines()
    {
        FullLines.Clear();        
        for (int i = 0; i < Height; i++)
        {
            var isFull = true;
            for (int x = 0; x < Width; x++)
            {
                if (!IsTaken(x, i))
                {
                    isFull = false;
                    break;
                }
            }

            if (isFull)
            {
                FullLines.Add(i);
            }
        }
    }

    public void RotateClockwise(Figure figure)
    {
        var nextSetIndex = (figure.CurrentSet == 0) ? (figure.RotationSets.Length - 1) : (figure.CurrentSet - 1);
        TryRotateToSet(figure, nextSetIndex);
    }

    
    
}


