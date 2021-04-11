using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class "GameManagement" manages the entire "Schach dem Schlaukopf" game.
public class GameManagement : MonoBehaviour
{
    // Variables for the current mouse position
    public int selectionX=-1;
    public int selectionY=-1;

    // Constants for the field sizes.
    public int TILE_SIZE;
    public float TILE_OFFSET;
 
    // Two dimensional array to save the GameFigure positions on the board
    public GameFigures[,] FigurePositions { get; set; }
    
    // Variable to save the selected GameFigure
    private GameFigures _selectedFigure;
    
    // List of all figures that can be initialized
    [SerializeField]
    public List<GameObject> GameFigures;
    
    // List of all active figures on the board
    private List<GameObject> _activeFigures = new List<GameObject>();

    // Two dimensional array to save the allowed movements of a figure
    private bool[,] _allowedMoves { get; set; }

    // Boolean to save which player's turn it is
    public bool isWhiteTurn = true;

    public static GameManagement Instance { get; set; }

    // Boolean to save whether a figure is allowed to conduct a movement
    private bool _hasAtLeastOneMove;

    void Start()
    {
        Instance = this;
        FigurePositions = new GameFigures[8, 7];
        SpawnAllFigures();
    }

    // Function to place all figures on the board.
    private void SpawnAllFigures()
    {
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

    private void SpawnFigure(int index, int x, int y)
    {
        //index erwartet einen Index der Liste "Game Figures"
        //x ist die Position auf den Zeilen des Spielbretts 
        //y ist die Position auf den Spalten des Spielbretts 
        //Einen neue Figure wird instanziert mittels "Game Figure List Index".
        //Die Berechnung der Position auf dem Spielbrett erfolgt �ber die Funktion "GetTileCenter"
        //Eine Rotation der Figuren ist nicht notwendig, da bereits in den Prefabs ber�cksichtigt
        GameObject brand_new_figure = Instantiate(GameFigures[index], GetTileCenter(x, y), GameFigures[index].transform.rotation) as GameObject;
        
        //Die neue Figur wird in das zweidimensionale Array des Spielfelds an Position x,y aufgenommen.
        FigurePositions[x, y] = brand_new_figure.GetComponent<GameFigures>();
        
        //Die neue Figur wird �ber die Methode dessen �bergeordneter Klasse "GameFigures" auf dem Spielbrett positioniert.
        FigurePositions[x, y].SetPosition(x, y);

        //Die neue Figur wird in die Liste aktiver Figuren aufgenommen.
        _activeFigures.Add(brand_new_figure);
    }
    //Get tile Center ermittelt die eigentlich Positionierung im Unity Koordinantensystem der Figuren 
    //in Abh�ngigkeit der Skalierungsfaktoren (aktuell Konstanten)
    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        origin.y += 0.5f;
        return origin;
    }
    // Update is called once per frame
    void Update()
    {
        //Aufzeichnung eines Spielbretts in der scene view �ber Debug-Methoden
        //N�tzlicch zur Ausrichtung des Spielfeld - Grids zum darunterliegenden "Bild" des Spielfelds
        //Aktuell kommentiert, da nicht mehr notwendig
        DrawBoard();

        //Berechnung und Darstellung der Maus-Position in der Scene-View
        UpdateSelection();

        //Reagieren auf "Klick"-Event der linken Maustaste
        if (Input.GetMouseButtonDown(0))
        {
            //Nur falls Maus-Position innerhalb des Boards 
            if (selectionX >= 0 && selectionY >= 0)
            {
                //Nur falls aktuell keine andere Figur angew�hlt ist kann eine andere Figur ausgew�hlt werden
                if (_selectedFigure == null)
                {
                    //Figur selektieren
                    SelectFigure(selectionX, selectionY);
                }
                else
                {
                    //Figur bewegen
                    MoveFigure(selectionX, selectionY);
                }
            }
        }
    }

    private void DrawBoard()
    {
        //Einstellen der L�nge und H�he des Grids f�r das Spielfeld
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 7;

        //Linien ziehen um ein Spielfeld in der Scene-View zu erzeugen
        for (int i = 0; i <= 7; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);
            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine);
            }
        }
        //Falls sich der Mauszeiger innerhalb eines Spielfelds befindet wird in der Scene-View ein Kreuz eingeblendet.
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));
            Debug.DrawLine(Vector3.forward * selectionY + Vector3.right * (selectionX + 1),
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX);
        }
    }
    //Methode zum Updaten der Mausposition
    private void UpdateSelection()
    {
        //Ein "Raycast" wird von der Kamera-Position durch die Maus auf das Spielfeld "geschossen".
        //Das "BoardLayer" muss auf jedenfall innerhalb des Spielfelds platziert werden, damit die Ausrichtung passt.
        //Raycast-Distance muss mindestens lang genug sein, um am ���ersten Rand des Spielfelds noch auf das BoardLayer zu treffen.
        RaycastHit hit;
        float raycastDistance = 25.0f;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, raycastDistance, LayerMask.GetMask("BoardLayer")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        //N�tig um Aktionen im GameManagement zu unterbinden
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }
    
    //Methode zum Ausw�hlen einer Figur auf dem Spielfeld
    private void SelectFigure(int x, int y)
    {
        //Funktion nicht ausf�hren, wenn sich auf einem ausgew�hlten Spielfeld keine Figur, also im 2D-Array "FigurePositions" null, befindet
        if (FigurePositions[x, y] == null) return;

        //Funktion nicht ausf�hren, wenn sich die Figurfarbe von der des aktuellen Speielers unterscheidet.
        if (FigurePositions[x, y].isWhite != isWhiteTurn) return;

        //Der Merker wird zur�ckgesetzt
        _hasAtLeastOneMove = false;

        //Die m�glichen Bewegungen der individuellen Spielfigur an Position [x,y] werden in deren Methode "PossibleMove" ermittelt
        _allowedMoves = FigurePositions[x, y].PossibleMove();

        //Das zur�ckgegebene, boolsche 2D-Array "_allowedMoves" wird durchlaufen
        //Sollte im Array ein "true" gefunden werden, werden die Schleifen abgebrochen
        //Der Merker "_hasAtLeastOneMove" wird auf "true" gesetzt
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (_allowedMoves[i, j])
                {
                    _hasAtLeastOneMove = true;
                    
                    // break outer loop
                    i = 7;

                    // break inner loop
                    break;
                }
            }
        }

        //Sollten keine Z�ge m�glich sein, wird die Methode hier abgebrochen
        if (!_hasAtLeastOneMove) return;
        
        //Da die Figur m�gliche Z�ge hat, wird diese nun auch angew�lt
        _selectedFigure = FigurePositions[x, y]; 
        //Die Methode HighlightAllowedMoves wird ausgef�hrt und alle m�glichen Z�ge werden auf dem Spielbrett hervorgehoben
        BoardHighlighting.Instance.HighlightAllowedMoves(_allowedMoves);
    }

    //Methode zum Bewegen einer Figur
    private void MoveFigure(int x, int y)
    {
        //Nur ausf�hren, wenn auf ein Spielfeld mit den Koordinaten geklickt wurde, auf dem sich im "_allowedMoves"-Array ebenfalls ein "True"-val
        //befindent.
        if (_allowedMoves[x, y])
        {
            //Abfrage ob auf dem ausgew�hltem Feld eine gegnerische Figur steht.
            GameFigures PossibleEnemy = FigurePositions[x, y];
            if (PossibleEnemy != null && PossibleEnemy.isWhite != isWhiteTurn)
            {
                _activeFigures.Remove(PossibleEnemy.gameObject);
                Destroy(PossibleEnemy.gameObject);

                //Falls die Figur auch noch der generische "Schlaukopf" war, wird das Spiel beendet.
                if (PossibleEnemy.GetType() == typeof(Schlaukopf_left) || PossibleEnemy.GetType() == typeof(Schlaukopf_right))
                {
                    EndGame();
                    return;
                }
            }
            //Die zur�ckgelassene Position nach der bald folgenden Bewegung wird zuvor auf "null" gesetzt
            FigurePositions[_selectedFigure.CurrentX, _selectedFigure.CurrentY] = null;

            //Die Figur wird auf die neue, ausgew�hlte Position auf dem Array "_allowedMoves" positioniert
            _selectedFigure.transform.position = GetTileCenter(x, y);
            _selectedFigure.SetPosition(x, y);
            
            //Das array "FigurePositions" wird ebenfalls mit der neuen Position der aktuellen Figur aktulaisiert
            FigurePositions[x, y] = _selectedFigure;

            //Der Spielzug des aktuellen Spielers wird beendet
            isWhiteTurn = !isWhiteTurn;
        }

        //Die zuvor eingeblendeten Board Highlights werden wieder zur�ckgesetzt
        BoardHighlighting.Instance.HideHighlights();

        //Eine neue Figur kann nun wieder angew�hlt werden
        _selectedFigure = null;
    }
   
    //Das Spiel wird beendet, sobald einer der beiden Schlauk�pfe geschlagen wurde
    private void EndGame()
    {
        if (isWhiteTurn)
            Debug.Log("White team won!");
        else
            Debug.Log("Black team won!");

        //Zerst�rung aller �brigen Elemente im Register "_activeFigures"
        foreach (GameObject Remeinders in _activeFigures)
            Destroy(Remeinders);

        //Wei� ist wieder am Zug
        isWhiteTurn = true;

        //Alle Board-highlights werden zur�ckgesetzt
        BoardHighlighting.Instance.HideHighlights();

        //Alle Figuren werden gespawnt
        SpawnAllFigures();

        //Meldung zum erneuten Spielstart
        Debug.Log("White turn: " + isWhiteTurn);
    }
}






