using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class FigureViewManager : IExecuteSystem
{
    private readonly Config _config;
    public List<FigureView> FigureViews = new List<FigureView>();
    private float anchorSizeX;
    private float anchorSizeY;  

    public FigureViewManager(Config config)
    {
        _config = config;
    }
	
    public void AddFigure(FigureView figureView)
    {
        FigureViews.Add(figureView);
    }

    private List<FigureView> _destroyList = new List<FigureView>();

    public void Tick()
    {
        var fieldDescription = _config.FieldDescription;
        var offsetXFloat = fieldDescription.OffsetX / (float) fieldDescription.TotalWidth;
        var offsetYFloat = fieldDescription.OffsetY / (float) fieldDescription.TotalHeight;
        
        var spacingXFloat = fieldDescription.SpacingX / (float) fieldDescription.TotalWidth;
        var spacingYFloat = fieldDescription.SpacingY / (float) fieldDescription.TotalHeight;

        anchorSizeX = fieldDescription.CellSizeX / (float) fieldDescription.TotalWidth;
        anchorSizeY = fieldDescription.CellSizeY / (float) fieldDescription.TotalHeight;
        
        _destroyList.Clear();
        
        for (var i = 0; i < FigureViews.Count; i++)
        {
            var figureView = FigureViews[i];
            figureView.Tick();

            var figure = figureView.FigureDesc;
            var thereisAlive = false;
            for (var index = 0; index < figure.Blocks.Length; index++)
            {
                var block = figure.Blocks[index];

                if (!block.IsDestroyed)
                {
                    thereisAlive = true;
                    var rectTransform = figureView.BlockViews[index].transform as RectTransform;
                    rectTransform.anchoredPosition = Vector2.zero;
                    rectTransform.sizeDelta = Vector2.zero;
                    
                    var x = figure.X + block.X;
                    var y = figure.Y + block.Y;
                    var minPositionX = offsetXFloat + x * spacingXFloat + x * anchorSizeX;
                    var minPositionY = offsetYFloat + y * spacingYFloat + y * anchorSizeY;
                    
                    rectTransform.anchorMin = new Vector2(minPositionX, minPositionY);
                    rectTransform.anchorMax = new Vector2(minPositionX + anchorSizeX, minPositionY + anchorSizeY);
                }
            }
            if (!thereisAlive)
            {
                _destroyList.Add(figureView);
            }
        }

        foreach (var figureView in _destroyList)
        {
            Object.Destroy(figureView.gameObject);
            FigureViews.Remove(figureView);
        }
        
        
    }
}