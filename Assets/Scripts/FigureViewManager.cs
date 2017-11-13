using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FigureViewManager : IUpdateable
{
    private readonly Config _config;
    public List<FigureView> FigureViews = new List<FigureView>();
    private float anchorSizeX;
    private float anchorSizeY;  

    public FigureViewManager(Config config)
    {
        _config = config;

        var aspectRatioFitter = _config.FigureRoot.GetComponent<AspectRatioFitter>() ??
                                _config.FigureRoot.gameObject.AddComponent<AspectRatioFitter>();
        aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
        aspectRatioFitter.aspectRatio = _config.Width / (float) _config.Height;

    }
	
    public void AddFigure(FigureView figureView)
    {
        FigureViews.Add(figureView);
    }

    public void Tick()
    {
        anchorSizeX = 1f / _config.Width;
        anchorSizeY = 1f / _config.Height;
        
        for (var i = 0; i < FigureViews.Count; i++)
        {
            var figureView = FigureViews[i];
            figureView.Tick();

            var figure = figureView.FigureDesc;
            var rotationSet = figure.RotationSets[figure.CurrentSet];
            var thereisAlive = false;
            for (var index = 0; index < figure.Blocks.Length; index++)
            {
                var block = figure.Blocks[index];

                if (!block.IsDestroyed)
                {
                    thereisAlive = true;
                    var rectTransform = figureView.BlockViews[index].transform as RectTransform;
                    var minPositionX = (figure.X + block.X) / (float) _config.Width;
                    var minPositionY = (figure.Y + block.Y) / (float) _config.Height;
                    rectTransform.anchoredPosition = Vector2.zero;
                    rectTransform.sizeDelta = Vector2.zero;
                    rectTransform.anchorMin = new Vector2(minPositionX, minPositionY);
                    rectTransform.anchorMax = new Vector2(minPositionX + anchorSizeX, minPositionY + anchorSizeY);
                }
            }
            if (!thereisAlive)
            {
                Object.Destroy(figureView);
                i--;
            }
        }
    }
}