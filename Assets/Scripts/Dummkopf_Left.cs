using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class "Dummkopf_Left" controls the actions and movements of "Dummkopf" figures of the left player.
public class Dummkopf_Left : GameFigures
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 7];
        GameFigures c;

        if (isWhite)
        {
            // Forward
            if (CurrentX != 0)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX - 1, CurrentY];
                if (c == null || !c.isWhite)
                {
                    r[CurrentX - 1, CurrentY] = true;
                }
            }

            // Backward
            if (CurrentX != 7)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX + 1, CurrentY];
                if (c == null || !c.isWhite)
                {
                    r[CurrentX + 1, CurrentY] = true;
                }
            }

            // Left
            if (CurrentY != 0)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX, CurrentY - 1];
                if (c == null || !c.isWhite)
                {
                    r[CurrentX, CurrentY - 1] = true;
                }
            }

            // Right
            if (CurrentY != 6)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX, CurrentY + 1];
                if (c == null || !c.isWhite)
                {
                    r[CurrentX, CurrentY + 1] = true;
                }
            }

            // Diagonal forward right
            if (CurrentX != 0 && CurrentY != 6)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX-1, CurrentY + 1];
                if (c == null || !c.isWhite)
                {
                    r[CurrentX - 1, CurrentY + 1] = true;
                }
            }

            // Diagonal forward left
            if (CurrentX != 0 && CurrentY != 0)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX - 1, CurrentY - 1];
                if (c == null || !c.isWhite)
                {
                    r[CurrentX - 1, CurrentY - 1] = true;
                }
            }

            // Diagonal backward right
            if (CurrentX != 7 && CurrentY != 6)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX + 1, CurrentY + 1];
                if (c == null || !c.isWhite)
                {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }
            }

            // Diagonal backward left
            if (CurrentX != 7 && CurrentY != 0)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX + 1, CurrentY - 1];
                if (c == null || !c.isWhite)
                {
                    r[CurrentX + 1, CurrentY - 1] = true;
                }
            }
        }
        return r;
    }
}
