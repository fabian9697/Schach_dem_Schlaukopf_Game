using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class "Schlitzohr_Right" controls the actions and movements of "Schlitzohr" figures of the right player.
public class Schlitzohr_Right : GameFigures
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 7];

        GameFigures c;
        int i, j;

        // Diagonal forward left
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

        // Diagonal forward right
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

        // Diagonal backward left
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

        // Diagonal backward right
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