public class GameSimulation : IUpdateable
{
    private readonly Config _config;
    private readonly GameState _gameState;
    private readonly FigureControl _control;
    private readonly IFigureFactory _figureFactory;

    public bool IsGameOver;

    public GameSimulation(Config config, IFigureFactory figureFactory, GameState gameState, FigureControl control)
    {
        _config = config;
        _figureFactory = figureFactory;
        _gameState = gameState;
        _control = control;
    }

    public void Tick()
    {
        if (_control.CurrentFigure == null)
        {
            var currentFigure = _figureFactory.NewFigureDescription;
            if (!_gameState.CanMoveTo(currentFigure, (int) _config.SpawnPosition.x, (int) _config.SpawnPosition.y))
            {
                IsGameOver = true;
            }
            else
            {
                _figureFactory.SpawnFigure(currentFigure);
                _gameState.MoveTo(currentFigure, (int) _config.SpawnPosition.x, (int) _config.SpawnPosition.y);
            }
        }
        else
        {
            var currentFigure = _control.CurrentFigure;

            if (_gameState.CanMoveTo(currentFigure, currentFigure.X, currentFigure.Y - 1))
            {
                _gameState.MoveTo(currentFigure, currentFigure.X, currentFigure.Y - 1);
            }
            else
            {
                currentFigure = null;
            }
        }
    }
}