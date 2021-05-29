using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class "DummkopfBlack" controls the actions and movements of the black "Dummkopf" figures.
public class DummkopfBlack : GameFigures
{
    public override bool[,] PossibleMove(bool[] allowed_directions_on_current_position)
    {
        bool[,] r = new bool[8, 7];
        GameFigures c;

        if (!isWhite)
        {
            // Forward
            if (CurrentX != 7)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX + 1, CurrentY];

                if (allowed_directions_on_current_position[1] && (c == null || c.isWhite))
                {
                    r[CurrentX + 1, CurrentY] = true;
                }
            }

            // Backward
            if (CurrentX != 0)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX - 1, CurrentY];

                if (allowed_directions_on_current_position[0] && (c == null || c.isWhite))
                {
                    r[CurrentX - 1, CurrentY] = true;
                }
            }

            // Left
            if (CurrentY != 6)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX, CurrentY + 1];

                if (allowed_directions_on_current_position[3] && (c == null || c.isWhite))
                {
                    r[CurrentX, CurrentY + 1] = true;
                }
            }

            // Right
            if (CurrentY != 0)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX, CurrentY - 1];

                if (allowed_directions_on_current_position[2] && (c == null || c.isWhite))
                {
                    r[CurrentX, CurrentY - 1] = true;
                }
            }

            // Diagonal forward left
            if (CurrentX != 7 && CurrentY != 6)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX + 1, CurrentY + 1];

                if (allowed_directions_on_current_position[7] && (c == null || c.isWhite))
                {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }
            }

            // Diagonal forward right
            if (CurrentX != 7 && CurrentY != 0)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX + 1, CurrentY - 1];

                if (allowed_directions_on_current_position[6] && (c == null || c.isWhite))
                {
                    r[CurrentX + 1, CurrentY - 1] = true;
                }
            }

            // Diagonal backward left
            if (CurrentX != 0 && CurrentY != 6)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX - 1, CurrentY + 1];

                if (allowed_directions_on_current_position[5] && (c == null || c.isWhite))
                {
                    r[CurrentX - 1, CurrentY + 1] = true;
                }
            }

            // Diagonal backward right
            if (CurrentX != 0 && CurrentY != 0)
            {
                c = GameManagement.Instance.FigurePositions[CurrentX - 1, CurrentY - 1];
                
                if (allowed_directions_on_current_position[4] && (c == null || c.isWhite))
                {
                    r[CurrentX - 1, CurrentY - 1] = true;
                }
            }
        }
        return r;
    }
}