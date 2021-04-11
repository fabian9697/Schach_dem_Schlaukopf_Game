using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class "Schlaukopf_right" controls the actions and movements of "Schlaukopf" figures of the right player.
public class Schlaukopf_right : GameFigures
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 7];
        GameFigures c;

        if (!isWhite)
        {
            // Forward
            if (CurrentX != 7)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX + 1, CurrentY];
                if (c == null || c.isWhite)
                {
                    r[CurrentX + 1, CurrentY] = true;
                }
            }

            // Backward
            if (CurrentX != 0)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX - 1, CurrentY];
                if (c == null || c.isWhite)
                {
                    r[CurrentX - 1, CurrentY] = true;
                }
            }

            // Left
            if (CurrentY != 6)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX, CurrentY + 1];
                if (c == null || c.isWhite)
                {
                    r[CurrentX, CurrentY + 1] = true;
                }
            }

            // Right
            if (CurrentY != 0)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX, CurrentY - 1];
                if (c == null || c.isWhite)
                {
                    r[CurrentX, CurrentY - 1] = true;
                }
            }

            // Diagonal forward left
            if (CurrentX != 7 && CurrentY != 6)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX + 1, CurrentY + 1];
                if (c == null || c.isWhite)
                {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }
            }

            // Diagonal forward right
            if (CurrentX != 7 && CurrentY != 0)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX + 1, CurrentY - 1];
                if (c == null || c.isWhite)
                {
                    r[CurrentX + 1, CurrentY - 1] = true;
                }
            }

            // Diagonal backward left
            if (CurrentX != 0 && CurrentY != 6)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX - 1, CurrentY + 1];
                if (c == null || c.isWhite)
                {
                    r[CurrentX - 1, CurrentY + 1] = true;
                }
            }

            // Diagonal backward right
            if (CurrentX != 0 && CurrentY != 0)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX - 1, CurrentY - 1];
                if (c == null || c.isWhite)
                {
                    r[CurrentX - 1, CurrentY - 1] = true;
                }
            }
        }
        return r;
    }
}