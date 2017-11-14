using System.Linq;
using UnityEngine.Experimental.Rendering;

public class GameSimulation : IExecuteSystem
{
    private readonly Config _config;
    private readonly GameState _gameState;
    private readonly IControl _control;
    private readonly IFigureFactory _figureFactory;
    private readonly FigureViewManager _figureViewManager;

    public bool IsGameOver;

    public GameSimulation(Config config, IFigureFactory figureFactory, FigureViewManager figureViewManager, GameState gameState, IControl control)
    {
        _config = config;
        _figureFactory = figureFactory;
        _figureViewManager = figureViewManager;
        _gameState = gameState;
        _control = control;
    }

    public void Tick()
    {
        if (_control.CurrentFigure == null)
        {
            var currentFigureDesc = _figureFactory.NewFigureDescription;
            var currentFigure = new Figure(currentFigureDesc);
            
            if (!_gameState.CanMoveTo(currentFigure, (int) _config.SpawnPosition.x, (int) _config.SpawnPosition.y))
            {
                IsGameOver = true;
                _config.LoseScreen.gameObject.SetActive(true);
            }
            else
            {
                var figureView = _figureFactory.SpawnFigure(currentFigureDesc);
                figureView.FigureDesc = currentFigure;
                _gameState.MoveTo(currentFigure, (int) _config.SpawnPosition.x, (int) _config.SpawnPosition.y);
                _figureViewManager.AddFigure(figureView);

                _control.CurrentFigure = currentFigure;
            }
        }
    }
}