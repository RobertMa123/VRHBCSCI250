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

    private List<GameObject> textEntries;
    private void Start()
    {
        textEntries = new List<GameObject>();

        AppendResults();
    }

    public void AppendResults()
    {
        entries = FileHandler.ReadListFromJSON<InputEntry>(filename);
        int textEntriesCount = textEntries.Count;
        for (int i = 0; i< textEntriesCount; i++)
        {
            Destroy(textEntries[i]);
        }
        textEntries.Clear();
        for (int i = 0; i < entries.Count; i++)
        {
            GameObject textEntry = Instantiate(textPrefab, entriesParent.transform);
            textEntry.GetComponent<TextMeshProUGUI>().text = entries[entries.Count - i - 1].testName + "| " + entries[entries.Count - i - 1].playerName + ": " + entries[entries.Count - i - 1].points + "\n";
            textEntries.Add(textEntry);
        }
    }  
    
}
