using System.Linq;
using UnityEngine;

public class FigureFactory : IFigureFactory
{
    private readonly Config _config;

    public FigureFactory(Config config)
    {
        _config = config;
    }

    public Figure NewFigureDescription
    {
        get
        {
            var random = Random.Range(0, _config.FigureViews.Length);
            var item = _config.FigureViews[random];
            return item.FigureDesc;
        }
    }

    public FigureView SpawnFigure(Figure figure)
    {
        var prefab =  _config.FigureViews.First(x => x.FigureDesc == figure);
        var instance = Object.Instantiate(prefab, _config.FigureRoot);
        instance.Transform = instance.transform as RectTransform;
        return instance;
    }
}