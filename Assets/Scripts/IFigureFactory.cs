public interface IFigureFactory
{
    Figure NewFigureDescription { get; }
    FigureView SpawnFigure(Figure figure);
}