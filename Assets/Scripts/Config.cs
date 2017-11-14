using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Config
{
    public FieldDescription FieldDescription;
    
    public int Width = 10;
    public int Height = 22;
    public FigureView[] FigureViews;
    public Vector2 SpawnPosition;
    public Transform FigureRoot;
    public float AutoMoveTime = 1;
    public GameObject LoseScreen;
    public int PointsPerLine = 100;
    public bool MagicMode;
    public Text ScoreLabel; 
}

[Serializable]
public class FieldDescription
{
    public int OffsetX;
    public int OffsetY;
    public int TotalHeight;
    public int TotalWidth;
    public int SpacingX;
    public int SpacingY;
    public int CellSizeX = 21;
    public int CellSizeY = 21;
}