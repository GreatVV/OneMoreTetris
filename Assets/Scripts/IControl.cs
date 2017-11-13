public interface IControl
{
    void RotateClockWise();
    void RotateCounterClockWise();
    void MoveLeft();
    void MoveRight();
    void MoveDown();
    Figure CurrentFigure { get; set; }
}