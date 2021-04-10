using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlighting : MonoBehaviour {

    //Instanzierung der BoardHighlightigs
    public static BoardHighlighting Instance { get; set; }
    
    //Einbinden eines Prefabs aus Unity
    [SerializeField]
    public GameObject highlightPrefab;

    //Initialisieren einer Liste für alle erstellten Board-Highlights 
    private List<GameObject> highlights;

    private void Start()
    {
        Instance = this;
        //Liste aller vorhanden "Highlights" pro Feld
        highlights = new List<GameObject>();
    }

    //Die möglichen Bewegungen einer selektierten Figur werden auf dem Spielbrett hervorgehoben
    public void HighlightAllowedMoves(bool[,] moves)
    {
        //Durchlaufen aller Spalten
        for (int i = 0; i < 8; i++)
        {
            //Durchlaufen aller Zeilen
            for (int j = 0; j < 7; j++)
            {
                //Abgleichen mit dem übergebenem Array "allowedMoves" des GameManagements
                if (moves[i, j])
                {
                    //Auswahl/Erstellung eines freien highlight Objects
                    GameObject HighlightObject = GetHighlightObject();
                    //Aktivieren des ausgewählten Objects
                    HighlightObject.SetActive(true);
                    //Positionieren des ausgewählten Objects
                    HighlightObject.transform.position = new Vector3(i + 0.5f, 0, j + 0.5f);
                }
            }
        }
    }

    //Instanzieren oder aktivieren eines Highlightfeldes für jede Position auf dem Spielfeld
    private GameObject GetHighlightObject()
    {
        //Finde das nächste inaktive highlight-field Element
        GameObject HighlightOnField = highlights.Find(g => !g.activeSelf);
        //Falls kein inaktives existiert wird einees erzeugt
        if (HighlightOnField == null)
        {
            //instanzieren eines neuen Feldes
            HighlightOnField = Instantiate(highlightPrefab);

            //hinzufügen zur Liste verfügbarer Feld-Elemente
            highlights.Add(HighlightOnField);
        }
        return HighlightOnField;
    }

    //Hier können alle bereits ezeugten highlights auf einmal inaktiviert werden.
    
    public void HideHighlights()
    {
        foreach (GameObject go in highlights) go.SetActive(false);
    }

    //Es gäbe verschiedene Lösungen für das highlighting, allerdings finde ich diese am besten, da weder sofort alle
    //56 Elemente erzeugt werden müssen, noch jedes mal jedes Element nach der Nutzung zerstört werden muss.
}
