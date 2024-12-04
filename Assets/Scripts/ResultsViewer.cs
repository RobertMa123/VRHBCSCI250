using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
public class ResultsViewer : MonoBehaviour
{
    List<InputEntry> entries = new List<InputEntry>();
    [SerializeField] string filename;
    public GameObject entriesParent;
    [SerializeField] GameObject textPrefab;
    private void Start()
    {
        entries = FileHandler.ReadListFromJSON<InputEntry>(filename);
        AppendResults();
    }

    public void AppendResults()
    {

        for (int i = 0; i < entries.Count; i++)
        {
            GameObject textEntry = Instantiate(textPrefab, entriesParent.transform);
            textEntry.GetComponent<TextMeshProUGUI>().text = entries[entries.Count - i - 1].testName + "| " + entries[entries.Count - i - 1].playerName + ": " + entries[entries.Count - i - 1].points + "\n";
        }
    }  
    
    public void AddResult()
    {
        entries = FileHandler.ReadListFromJSON<InputEntry>(filename);

        GameObject textEntry = Instantiate(textPrefab, entriesParent.transform);
        textEntry.GetComponent<TextMeshProUGUI>().text = entries[entries.Count - 1].testName + "| " + entries[entries.Count - 1].playerName + ": " + entries[entries.Count - 1].points + "\n";
    }
}
