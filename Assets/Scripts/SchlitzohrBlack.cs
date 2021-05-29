using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class "SchlitzohrBlack" controls the actions and movements of the black "Schlitzohr" figures.
public class SchlitzohrBlack : GameFigures
{
    public override bool[,] PossibleMove(bool[] allowed_directions_on_current_position)
    {
        bool[,] r = new bool[8, 7];
        GameFigures c;

        if (!isWhite)
        {
            // Forward
            int i = CurrentX;
            int j = CurrentY;
            while (true)
            {
                i++;
                if (i > 7) break;

                c = GameManagement.Instance.FigurePositions[i, j];

                if(allowed_directions_on_current_position[1])
                {
                    if (c == null) r[i, j] = true;
                    else
                    {
                        if (c.isWhite) r[i, j] = true;
                        break;
                    }
                }
            }

            // Backward
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                i--;
                if (i < 0) break;

                c = GameManagement.Instance.FigurePositions[i, j];

                if(allowed_directions_on_current_position[0])
                {
                    if (c == null) r[i, j] = true;
                    else
                    {
                        if (c.isWhite) r[i, j] = true;
                        break;
                    }
                }
            }

            // Left
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                j++;
                if (j > 6) break;

                c = GameManagement.Instance.FigurePositions[i, j];

                if(allowed_directions_on_current_position[3])
                {
                    if (c == null) r[i, j] = true;
                    else
                    {
                        if (c.isWhite) r[i, j] = true;
                        break;
                    }
                }
            }

            // Right
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                j--;
                if (j < 0) break;

                c = GameManagement.Instance.FigurePositions[i, j];

                if(allowed_directions_on_current_position[2])
                {
                    if (c == null) r[i, j] = true;
                    else
                    {
                        if (c.isWhite) r[i, j] = true;
                        break;
                    }
                }
            }

            // Diagonal forward left
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                i++;
                j++;
                if (i > 7 || j > 6) break;

                c = GameManagement.Instance.FigurePositions[i, j];

                if(allowed_directions_on_current_position[7])
                {
                    if (c == null) r[i, j] = true;
                    else
                    {
                        if (c.isWhite) r[i, j] = true;
                        break;
                    }
                }
            }

            // Diagonal forward right
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                i++;
                j--;
                if (i > 7 || j < 0) break;

                c = GameManagement.Instance.FigurePositions[i, j];

                if(allowed_directions_on_current_position[6])
                {
                    if (c == null) r[i, j] = true;
                    else
                    {
                        if (c.isWhite) r[i, j] = true;
                        break;
                    }
                }
            }

            // Diagonal backward left
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                i--;
                j++;
                if (i < 0 || j > 6) break;

                c = GameManagement.Instance.FigurePositions[i, j];

                if(allowed_directions_on_current_position[5])
                {
                    if (c == null) r[i, j] = true;
                    else
                    {
                        if (c.isWhite) r[i, j] = true;
                        break;
                    }
                }
            }

            // Diagonal backward right
            i = CurrentX;
            j = CurrentY;
            while (true)
            {
                i--;
                j--;
                if (i < 0 || j < 0) break;

                c = GameManagement.Instance.FigurePositions[i, j];

                if(allowed_directions_on_current_position[4])
                {
                    if (c == null) r[i, j] = true;
                    else
                    {
                        if (c.isWhite) r[i, j] = true;
                        break;
                    }
                }
            }
        }
        return r;
    }
}