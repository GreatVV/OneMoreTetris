using System;
using UnityEngine;

[Serializable]
public class Config
{
    public int Width = 10;
    public int Height = 22;
    public FigureView[] FigureViews;
    public Vector2 SpawnPosition;
}