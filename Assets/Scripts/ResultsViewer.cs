using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] TextMeshProUGUI sortText;
    public enum SortType
    {
        None = 0,
        ByRecent = 1,
        LowToHigh = 2,
        HighToLow = 3,
    }

    public SortType currentSortType;
    private List<GameObject> textEntries;
    private void Start()
    {
        textEntries = new List<GameObject>();

        SortResults();
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

    public void SortResults()
    {
        sortText.text = $"Sorting by\n {currentSortType}";
        switch (currentSortType)
        {
            case SortType.ByRecent:
                SortByRecent();
                currentSortType = SortType.HighToLow;
                break;
            case SortType.HighToLow:
                SortHighLow();
                currentSortType = SortType.LowToHigh;
                break;
            case SortType.LowToHigh:
                SortLowHigh();
                currentSortType = SortType.ByRecent;
                break;
        }
    }

    private void SortByRecent()
    {
        AppendResults();
    }
    private void SortHighLow()
    {
        List<InputEntry> sortedEntries = entries.OrderBy(entry => entry.points).ToList();
        for (int i = 0; i < sortedEntries.Count;i++)
        {
            textEntries[i].GetComponent<TextMeshProUGUI>().text = sortedEntries[sortedEntries.Count - i - 1].testName + "| " + sortedEntries[sortedEntries.Count - i - 1].playerName + ": " + sortedEntries[sortedEntries.Count - i - 1].points + "\n";
        }
    }
    private void SortLowHigh()
    {
        List<InputEntry> sortedEntries = entries.OrderBy(entry => entry.points).Reverse().ToList();
        for (int i = 0; i < sortedEntries.Count; i++)
        {
            textEntries[i].GetComponent<TextMeshProUGUI>().text = sortedEntries[sortedEntries.Count - i - 1].testName + "| " + sortedEntries[sortedEntries.Count - i - 1].playerName + ": " + sortedEntries[sortedEntries.Count - i - 1].points + "\n";
        }
    }
    
}
