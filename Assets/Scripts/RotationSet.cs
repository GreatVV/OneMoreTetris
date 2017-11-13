using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RotationSet : IEnumerable<Vector2>
{
    public List<Vector2> Size = new List<Vector2>();
    
    public void Add(Vector2 vector2)
    {
        Size.Add(vector2);
    }

    public IEnumerator<Vector2> GetEnumerator()
    {
        return Size.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public Vector2 this[int index]
    {
        get { return Size[index]; }
    }
}