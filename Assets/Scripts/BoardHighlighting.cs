using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The class "BoardHighlighting" is responsible for several types of highlighting on the board.
public class BoardHighlighting : MonoBehaviour 
{
    public static BoardHighlighting Instance { get; set; }
    
    [SerializeField]
    public GameObject highlightPrefab;

    // List for all created highlights on the board 
    private List<GameObject> highlights;

    private void Start()
    {
        Instance = this;
        // List for all existing highlights per field
        highlights = new List<GameObject>();
    }

    // Function to highlight possible movements of a selected figure on the board.
    public void HighlightAllowedMoves(bool[,] moves)
    {
        // Loop over all rows
        for (int i = 0; i < 8; i++)
        {
            // Loop over all coloumns
            for (int j = 0; j < 7; j++)
            {
                // Comparison with the passed array "moves" 
                if (moves[i, j])
                {
                    // Create a "HighlightObject"
                    GameObject HighlightObject = GetHighlightObject();
                    // Activate the object
                    HighlightObject.SetActive(true);
                    // Place the object
                    HighlightObject.transform.position = new Vector3(i + 0.5f, 0, j + 0.5f);
                }
            }
        }
    }

    // Function to create "HighlightObjects" 
    private GameObject GetHighlightObject()
    {
        // Find the next inactive highlight field item in "highlights"
        GameObject HighlightOnField = highlights.Find(g => !g.activeSelf);
        // If there is no inactive highlight field item, a new one will be created.
        if (HighlightOnField == null)
        {
            HighlightOnField = Instantiate(highlightPrefab);
            highlights.Add(HighlightOnField);
        }
        return HighlightOnField;
    }

    // Function to hide/ deactivate all created highlights
    public void HideHighlights()
    {
        foreach (GameObject go in highlights) go.SetActive(false);
    }
}