using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Point
{
    public int X;   
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;  
    }
    
    
    public float DistanceTo(Point other)
    {
        return  Mathf.Sqrt((X - other.X) * (X - other.X) + (Y - other.Y) * (Y - other.Y));
    }
}


