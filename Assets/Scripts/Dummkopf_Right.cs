using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class "Dummkopf_Right" controls the actions and movements of "Dummkopf" figures of the right player.
public class Dummkopf_Right : GameFigures
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 7];
        GameFigures c, c2;

        if (!isWhite)
        {
            // Diagonal left
            if (CurrentX != 7 && CurrentY != 6)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX + 1, CurrentY + 1];
                if (c != null && c.isWhite)
                {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }
            }

            // Diagonal right
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
            
            // Two steps forward
            if (CurrentX == 1)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX+1, CurrentY];
                c2 = GameManagement.Instance.FigurePositions[CurrentX+2, CurrentY];
                if (c == null && c2 == null)
                {
                    r[CurrentX + 2, CurrentY] = true;
                }
            }
        }
        return r;
    }
}