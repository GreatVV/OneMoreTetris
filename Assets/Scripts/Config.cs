using System;
using UnityEngine;

[Serializable]
public class Config
{
    public int Width = 10;
    public int Height = 22;
    public FigureView[] FigureViews;
    public Vector2 SpawnPosition;
    public int PixelPerUnit = 30;
    public Transform FigureRoot;
    public float AutoMoveTime = 1;
}