using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schlaukopf_right : GameFigures
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 7];
        GameFigures c, c2;

        if (!isWhite)
        {

            // Diagonal Left
            if (CurrentX != 7 && CurrentY != 7)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX + 1, CurrentY + 1];
                if (c != null && c.isWhite)
                {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }
            }

            // Diagonal Right
            if (CurrentX != 7 && CurrentY != 0)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isWhite)
                {
                    r[CurrentX + 1, CurrentY - 1] = true;
                }
            }

            // Forward
            if (CurrentX != 7)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX + 1, CurrentY];
                if (c == null)
                {
                    r[CurrentX + 1, CurrentY] = true;
                }
            }


            // Two Steps Forward
            Debug.Log(CurrentX);
            if (CurrentX == 1)
            {
                Debug.Log("Checkmate");
                c = GameManagement.Instance.FigurePositions[CurrentX + 1, CurrentY];
                c2 = GameManagement.Instance.FigurePositions[CurrentX + 2, CurrentY];
                if (c == null && c2 == null)
                {
                    r[CurrentX + 2, CurrentY] = true;
                }
            }

        }

        return r;
    }
}
