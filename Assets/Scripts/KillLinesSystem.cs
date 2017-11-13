using System.Collections.Generic;
using UnityEngine;

public class KillLinesSystem : IUpdateable
{
    private readonly GameState _gameState;
    private readonly IControl _control;

    public KillLinesSystem(GameState gameState, IControl control)
    {
        _gameState = gameState;
        _control = control;
    }
    
    public void Tick()
    {
        _gameState.UpdateFullLines();
        if (_gameState.FullLines.Count > 0)
        {
            foreach (var gameStateFullLine in _gameState.FullLines)
            {
                KillLine(gameStateFullLine);
            }
        }
    }
    
    public void KillLine(int y)
    {
        for (int i = 0; i < _gameState.Width; i++)
        {
            var index = _gameState.GetIndex(i, y);
            var cellState = _gameState.CellStates[index];
            if (cellState.Block != null)
            {
                cellState.Block.IsDestroyed = true;
                cellState.Block = null;
            }
            cellState.Figure = null;

            for (var indexY = y + 1; indexY < _gameState.Height; indexY++)
            {
                var newIndex = _gameState.GetIndex(i, indexY);
                var prevIndex = _gameState.GetIndex(i, indexY - 1);
                var oldState = _gameState.CellStates[prevIndex];
                var newState = _gameState.CellStates[newIndex];
                oldState.Block = newState.Block;
                oldState.Figure = newState.Figure;
            }
        }
        
    }
}