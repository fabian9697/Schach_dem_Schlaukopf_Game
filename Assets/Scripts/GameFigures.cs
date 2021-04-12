using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class "GameFigures" is the parent class for the different types of game figures to provide basic properties.
public abstract class GameFigures : MonoBehaviour
{
    public int CurrentX { get; set; }
    public int CurrentY { get; set; }
    public bool isWhite;

    public void SetPosition(int x, int y)
    {
        CurrentX = x;
        CurrentY = y;
    }

    public virtual bool[,] PossibleMove()
    {
        return new bool[8, 7];
    }
}