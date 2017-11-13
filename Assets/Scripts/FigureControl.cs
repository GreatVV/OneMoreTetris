public class FigureControl : IControl
{
    public Figure CurrentFigure;
	
    private readonly GameState _gameState;

    public FigureControl(GameState gameState)
    {
        _gameState = gameState;
    }

    public void RotateClockWise()
    {
        if (CurrentFigure != null) _gameState.RotateCounterClockwise(CurrentFigure);
    }

    public void RotateCounterClockWise()
    {
        if (CurrentFigure != null) _gameState.RotateCounterClockwise(CurrentFigure);
    }

    public void MoveLeft()
    {
        if (CurrentFigure != null && _gameState.CanMoveTo(CurrentFigure, CurrentFigure.X - 1, CurrentFigure.Y))
        {
            _gameState.MoveTo(CurrentFigure, CurrentFigure.X - 1, CurrentFigure.Y);
        }
    }

    public void MoveRight()
    {
        if (CurrentFigure != null && _gameState.CanMoveTo(CurrentFigure, CurrentFigure.X + 1, CurrentFigure.Y))
        {
            _gameState.MoveTo(CurrentFigure, CurrentFigure.X + 1, CurrentFigure.Y);
        }
    }

    public void MoveDown()
    { 
        if (CurrentFigure != null && _gameState.CanMoveTo(CurrentFigure, CurrentFigure.X, CurrentFigure.Y-1))
        {
            _gameState.MoveTo(CurrentFigure, CurrentFigure.X, CurrentFigure.Y-1);
        }
    }
}