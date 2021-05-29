using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class "GameManagement" manages the entire "Schach dem Schlaukopf" game.
public class GameManagement : MonoBehaviour
{
    // Variables for the current mouse position.
    public int selectionX=-1;
    public int selectionY=-1;

    // Constants for the field sizes.
    public int TILE_SIZE;
    public float TILE_OFFSET;

    // List of all figures that can be initialized.
    [SerializeField]
    public GameObject Cam;

    // Two dimensional array to store the GameFigures depending on their positions on the board.
    public GameFigures[,] FigurePositions { get; set; }
    
    // Variable to store the currently selected GameFigure.
    private GameFigures _selectedFigure;
    
    // List of all figures that can be initialized.
    [SerializeField]
    public List<GameObject> GameFigures;
    
    // List of all active figures on the board.
    private List<GameObject> _activeFigures = new List<GameObject>();

    // Two dimensional array to store the allowed moves of a figure.
    private bool[,] _allowedMoves { get; set; }

    // Array in which the allowed move directions depending on the position on the board are defined.
    // From the point of view of the white player: {Forward, Backward, Left, Right, Diagonal forward left, Diagonal forward right, Diagonal backward left, Diagonal backward right}
    // For the black player the direct opposite direction holds: Forward for White = Backward for Black; Left for White = Right for Black and so on...  
    // Important: The origin of coordinates is the upper left corner of the board -> The first two dimensions of this array represent the figure position on the board.
    private bool[, ,] _allowed_directions_position_dependent = new bool[8,7,8]{
        {{false, true, false, true, false, false, false, false}, {false, true, true, true, false, false, false, false}, {false, true, true, true, false, false, false, false},
         {false, true, true, true, false, false, false, false}, {false, true, true, true, false, false, false, false}, {false, true, true, true, false, false, false, false},
         {false, true, false, false, false, false, false, false}},        
        {{true, true, false, false, false, false, false, false}, {false, true, false, true, true, false, false, false}, {true, true, true, true, false, false, false, false},
         {false, true, true, true, false, false, false, false}, {true, true, true, true, false, false, false, false}, {false, true, true, false, false, false, false, false},
         {true, true, false, false, false, false, false, false}},
        {{true, true, false, true, false, false, false, false}, {true, true, true, true, false, false, false, false}, {false, false, false, false, true, true, true, true},
         {false, false, true, true, false, false, false, false}, {false, false, false, false, true, true, true, true}, {true, false, true, true, false, false, false, false},
         {true, true, true, false, false, false, false, false}},
        {{false, false, false, true, false, true, false, true}, {false, false, false, true, false, false, false, false}, {true, true, false, true, false, false, false, false},
         {true, true, true, true, true, true, true, true}, {true, true, false, true, false, false, false, false}, {true, true, true, true, false, false, false, false},
         {true, true, false, false, false, false, false, false}},
        {{true, true, false, false, false, false, false, false}, {true, true, true, true, false, false, false, false}, {true, true, true, false, false, false, false, false},
         {true, true, true, true, true, true, true, true}, {true, true, true, false, false, false, false, false}, {false, false, true, false, false, false, false, false},
         {false, false, true, false, true, false, true, false}},  
        {{true, true, false, true, false, false, false, false}, {false, true, true, true, false, false, false, false}, {false, false, false, false, true, true, true, true},
         {false, false, true, true, false, false, false, false}, {false, false, false, false, true, true, true, true}, {true, true, true, true, false, false, false, false},
         {true, true, true, false, false, false, false, false}},  
        {{true, true, false, false, false, false, false, false}, {true, false, false, true, false, false, false, false}, {true, true, true, true, false, false, false, false},
         {true, false, true, true, false, false, false, false}, {true, true, true, true, false, false, false, false}, {true, false, true, false, false, false, false, true},
         {true, true, false, false, false, false, false, false}},
        {{true, false, false, false, false, false, false, false}, {true, false, true, true, false, false, false, false}, {true, false, true, true, false, false, false, false},
         {true, false, true, true, false, false, false, false}, {true, false, true, true, false, false, false, false}, {true, false, true, true, false, false, false, false},
         {true, false, true, false, false, false, false, false}}};

    // Boolean to store which player's turn it is.
    public bool isWhiteTurn = true;

    public static GameManagement Instance { get; set; }

    // Boolean to store whether a figure is allowed to conduct a movement.
    private bool _hasAtLeastOneMove;

    // Function to initialize a match.
    void Start()
    {
        Instance = this;
        FigurePositions = new GameFigures[8, 7];
        SpawnAllFigures();
    }

    // Function to place all figures on the board.
    private void SpawnAllFigures()
    {
        // Team Black
        SpawnFigure(0, 1, 0);
        SpawnFigure(0, 1, 1);
        SpawnFigure(0, 1, 2);
        SpawnFigure(0, 1, 3);
        SpawnFigure(0, 1, 4);
        SpawnFigure(0, 1, 5);
        SpawnFigure(0, 1, 6);

        SpawnFigure(1, 0, 1);
        SpawnFigure(1, 0, 2);
        SpawnFigure(1, 0, 4);
        SpawnFigure(1, 0, 5);

        SpawnFigure(2, 0, 3);

        // Team White
        SpawnFigure(3, 6, 0);
        SpawnFigure(3, 6, 1);
        SpawnFigure(3, 6, 2);
        SpawnFigure(3, 6, 3);
        SpawnFigure(3, 6, 4);
        SpawnFigure(3, 6, 5);
        SpawnFigure(3, 6, 6);

        SpawnFigure(4, 7, 1);
        SpawnFigure(4, 7, 2);
        SpawnFigure(4, 7, 4);
        SpawnFigure(4, 7, 5);

        SpawnFigure(5, 7, 3);
    }

    // Function to place one figure on the board.
    private void SpawnFigure(int index, int x, int y)
    {
        GameObject brand_new_figure = Instantiate(GameFigures[index], GetTileCenter(x, y), GameFigures[index].transform.rotation) as GameObject;
        
        // The new figure is added to the two dimensional array in which the figure positions are stored.
        FigurePositions[x, y] = brand_new_figure.GetComponent<GameFigures>();
        
        // The new figure is placed on the board.
        FigurePositions[x, y].SetPosition(x, y);

        // The new figure is added to the list of active figures.
        _activeFigures.Add(brand_new_figure);
    }

    // Function to determine the current position of a figure in the Unity coordinate system.
    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        origin.y += 0.5f;
        return origin;
    }

    // The "Update" function is called once per frame.
    void Update()
    {
        DrawBoard();

        // Determination and visualization of the mouse position in the scene-view
        UpdateSelection();

        // Reaction to a "Click"-event of the left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Check whether the mouse position is inside the board boundaries.
            if (selectionX >= 0 && selectionY >= 0)
            {
                // Only if no other figure is currently selected, a new figure can be selected.
                if (_selectedFigure == null)
                {
                    SelectFigure(selectionX, selectionY);
                }
                // Otherwise, the current figure is moved.
                else
                {
                    MoveFigure(selectionX, selectionY);
                }
            }
        }
    }

    // Function to visualize the board.
    private void DrawBoard()
    {
        // Define the height and width of the board.
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 7;

        // Draw lines to obtain a board in the scene-view.
        for (int i = 0; i < 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j < 9; j++)                    
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine);
            }
        }
        
        // If the mouse is inside the board, a cross is displayed in the scene view.
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * selectionX, Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
            Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * (selectionX + 1), Vector3.forward * (selectionY + 1) + Vector3.right * selectionX);
        }
    }

    // Function to update the mouse position.
    private void UpdateSelection()
    {
        RaycastHit hit;
        float raycastDistance = 25.0f;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, raycastDistance, LayerMask.GetMask("BoardLayer")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }
    
    // Function to select a figure on the board.
    private void SelectFigure(int x, int y)
    {
        // Do not conduct the function if there is no figure on the selected field.
        if (FigurePositions[x, y] == null) return;

        // Do not conduct the function if the selected figure does not belong to current player.
        if (FigurePositions[x, y].isWhite != isWhiteTurn) return;

        // Flag to store the information whether there exists at least one possible move for the current figure. 
        _hasAtLeastOneMove = false;

        // Determine the allowed directions at the current position of the figure.
        bool[] allowed_directions_on_current_position = new bool [8];
        for (int i = 0; i < 8; i++)
        {
            allowed_directions_on_current_position[i] = _allowed_directions_position_dependent[x, y, i];
        }

        // The possible moves of the selected figure are determined.
        _allowedMoves = FigurePositions[x, y].PossibleMove(allowed_directions_on_current_position);

        // Loop over the returned two dimensional array "_allowedMoves". Break the loop if at least one move can be found and set the flag "_hasAtLeastOneMove" to true.
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (_allowedMoves[i, j])
                {
                    _hasAtLeastOneMove = true;
                    // Break outer loop
                    i = 7;
                    // Break inner loop
                    break;
                }
            }
        }

        // If there exists no possible moves, the function is interrupted.
        if (!_hasAtLeastOneMove) return;

        // The "_selectedFigure" is set. 
        _selectedFigure = FigurePositions[x, y]; 
        // The possible moves of the selected figure are highlighted on the board.
        BoardHighlighting.Instance.HighlightAllowedMoves(_allowedMoves);
    }

    // Function to move a figure.
    private void MoveFigure(int x, int y)
    {
        // Check whether the target field is a possible one according to the allowed moves of the figure.
        if (_allowedMoves[x, y])
        {
            // Check whether there is a figure of the enemy placed on the target field.
            GameFigures PossibleEnemy = FigurePositions[x, y];
            if (PossibleEnemy != null && PossibleEnemy.isWhite != isWhiteTurn)
            {
                _activeFigures.Remove(PossibleEnemy.gameObject);
                Destroy(PossibleEnemy.gameObject);

                // If the enemy's figure was the "Schlaukopf" end the game.
                if (PossibleEnemy.GetType() == typeof(SchlaukopfWhite) || PossibleEnemy.GetType() == typeof(SchlaukopfBlack))
                {
                    EndGame();
                    return;
                }
            }
            // Remove the current figure from its stored previous position.
            FigurePositions[_selectedFigure.CurrentX, _selectedFigure.CurrentY] = null;

            // The figure is moved to the selected target position.
            _selectedFigure.transform.position = GetTileCenter(x, y);
            _selectedFigure.SetPosition(x, y);
            
            // Store the new position of the current figure.
            FigurePositions[x, y] = _selectedFigure;

            // Change the active player after the move was conducted.
            isWhiteTurn = !isWhiteTurn;

            if (isWhiteTurn == true)
            {
                Cam.transform.position = new Vector3(8.5f, 4.75f, 3.32f);
                Cam.transform.rotation = Quaternion.Euler(50f, 270f, 0f);
            }
            else if (isWhiteTurn == false)
            { 
                Cam.transform.position = new Vector3(-1.0f, 4.75f, 3.32f);
                Cam.transform.rotation = Quaternion.Euler(130f, 270f, 180f);
            }
        }

        // Hide the previous shown highlights.
        BoardHighlighting.Instance.HideHighlights();

        // The "_selectedFigure" variable is cleared.
        _selectedFigure = null;
    }
   
    // Function to end the game if a "Schlaukopf" is beaten.
    private void EndGame()
    {
        if (isWhiteTurn)
            Debug.Log("White team won!");
        else
            Debug.Log("Black team won!");

        // Destruction of all remaining figures on the board.
        foreach (GameObject Remainder in _activeFigures)
            Destroy(Remainder);

        // Team White can start a new game.
        isWhiteTurn = true;

        // All board highlights are resettet.
        BoardHighlighting.Instance.HideHighlights();

        // All figures are placed on the board for a new game. 
        SpawnAllFigures();

        // Notification to start a new game.
        Debug.Log("White turn: " + isWhiteTurn);
    }
}