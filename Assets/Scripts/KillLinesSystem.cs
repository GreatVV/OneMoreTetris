using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class KillLinesSystem : IExecuteSystem
{
    private readonly Config _config;
    private readonly GameState _gameState;
    private readonly IScoreSystem _scoreSystem;
    private readonly IControl _control;

    public KillLinesSystem(Config config, GameState gameState, IScoreSystem scoreSystem,  IControl control)
    {
        _config = config;
        _gameState = gameState;
        _scoreSystem = scoreSystem;
        _control = control;
    }
    
    public void Tick()
    {
        if (_control.CurrentFigure == null)
        {
            _gameState.UpdateFullLines();
            if (_gameState.FullLines.Count > 0)
            {
                for (var index = _gameState.FullLines.Count - 1; index >= 0; index--)
                {
                    var targetY = _gameState.FullLines[index];
                    KillLine(targetY);
                    _scoreSystem.CountKilledLine();
                }
            }

            if (_config.MagicMode)
            {
                MoveAllBlocksWithoutNeigbours();
            }
        }
    }

    private void MoveAllBlocksWithoutNeigbours()    
    {
        for (int x = 0; x < _gameState.Width; x++)
        {
            for (int y = _gameState.Height - 1; y >= 1; y--)
            {
                var cellIndex = _gameState.GetIndex(x, y);
                var cell = _gameState.CellStates[cellIndex];

                if (cell.Block == null)
                {
                    continue;
                }
                
                var hasNeighbour = false;
                
                if (y > 0)
                {
                    var bottomindex = _gameState.GetIndex(x, y - 1);
                    var bottomCell = _gameState.CellStates[bottomindex];
                    hasNeighbour |= bottomCell.Block != null;
                }
                if (x > 0)
                {
                    var leftIndex = _gameState.GetIndex(x - 1, y);
                    var leftCell = _gameState.CellStates[leftIndex];
                    hasNeighbour |= leftCell.Block != null;
                }
                if (x < _gameState.Width - 1)
                {
                    var rightIndex = _gameState.GetIndex(x + 1, y);
                    var rightCell = _gameState.CellStates[rightIndex];
                    hasNeighbour |= rightCell.Block != null;
                }

                if (!hasNeighbour)
                {
                    MoveDown(x, y - 1);
                } 
                
            }
        }
    }

    private void KillLine(int targetY)
    {
        for (int x = 0; x < _gameState.Width; x++)
        {
            var index = _gameState.GetIndex(x, targetY);
            var cellState = _gameState.CellStates[index];
            if (cellState.Block != null)
            {
                cellState.Block.IsDestroyed = true;
                cellState.Block = null;
            }
            cellState.Figure = null;

            MoveDown(x, targetY);
        }
        
    }

    private void MoveDown(int x, int targetY)
    {
        for (var indexY = targetY; indexY < _gameState.Height - 1; indexY++)
        {
            var newIndex = _gameState.GetIndex(x, indexY + 1);
            var prevIndex = _gameState.GetIndex(x, indexY);
            var prevState = _gameState.CellStates[prevIndex];
            var newState = _gameState.CellStates[newIndex];
            prevState.Block = newState.Block;
            if (newState.Block != null)
            {
                newState.Block.Position = new Vector2(newState.Block.X, newState.Block.Y - 1);
            }
            prevState.Figure = newState.Figure;
        }

        var lastCell = _gameState.CellStates[_gameState.GetIndex(x, _gameState.Height - 1)];
        lastCell.Block = null;
        lastCell.Figure = null;
    }
}