using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schlitzohr_Left : GameFigures
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 7];

        GameFigures c;
        int i, j;

        // forward
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            
            
            if (i < 0) break;
            c = GameManagement.Instance.FigurePositions[i, j];
            if (c == null) r[i, j] = true;
            else
            {
                if (c.isWhite != isWhite) r[i, j] = true;
                break;
            }
        }

        // backward
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            

            if (i >= 8) break;
            c = GameManagement.Instance.FigurePositions[i, j];
            if (c == null) r[i, j] = true;
            else
            {
                if (c.isWhite != isWhite) r[i, j] = true;
                break;
            }
        }

        // left
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            j--;
            

            if (j < 0) break;
            c = GameManagement.Instance.FigurePositions[i, j];
            if (c == null) r[i, j] = true;
            else
            {
                if (c.isWhite != isWhite) r[i, j] = true;
                break;
            }
        }

        // right
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            j++;
           

            if (j >= 7) break;
            c = GameManagement.Instance.FigurePositions[i, j];
            if (c == null) r[i, j] = true;
            else
            {
                if (c.isWhite != isWhite) r[i, j] = true;
                break;
            }
        }

        // Top Left
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j++;
            if (i < 0 || j >= 7) break;
            c = GameManagement.Instance.FigurePositions[i, j];
            if (c == null) r[i, j] = true;
            else
            {
                if (c.isWhite != isWhite) r[i, j] = true;
                break;
            }
        }

        // Top Right
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j++;
            if (i >= 8 || j >= 7) break;
            c = GameManagement.Instance.FigurePositions[i, j];
            if (c == null) r[i, j] = true;
            else
            {
                if (c.isWhite != isWhite) r[i, j] = true;
                break;
            }
        }

        // Bottom Left
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i--;
            j--;
            if (i < 0 || j < 0) break;
            c = GameManagement.Instance.FigurePositions[i, j];
            if (c == null) r[i, j] = true;
            else
            {
                if (c.isWhite != isWhite) r[i, j] = true;
                break;
            }
        }

        // Bottom Right
        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            i++;
            j--;
            if (i >= 8 || j < 0) break;
            c = GameManagement.Instance.FigurePositions[i, j];
            if (c == null) r[i, j] = true;
            else
            {
                if (c.isWhite != isWhite) r[i, j] = true;
                break;
            }
        }

        return r;
    }
}
