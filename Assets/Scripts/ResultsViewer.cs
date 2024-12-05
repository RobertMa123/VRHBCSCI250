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
    List<InputEntry> curPlayerEntries = new List<InputEntry>();
    [SerializeField] string filename;
    public GameObject entriesParent;
    [SerializeField] GameObject textPrefab;

    private bool onlyShowCurUserScores = false;
    [SerializeField] private VRKeyboard keyboard;
    [SerializeField] private ToggleKeyLook buttonToggle;

    private const float WAIT_TIME = 0.25f;
    private float elapsedTime;

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

    public void Update()
    {
        if (elapsedTime < WAIT_TIME)
        {
            elapsedTime += Time.deltaTime;
        }
    }

    public void AppendResults()
    {
        entries = FileHandler.ReadListFromJSON<InputEntry>(filename);
        curPlayerEntries.Clear();
        int textEntriesCount = textEntries.Count;
        for (int i = 0; i< textEntriesCount; i++)
        {
            Destroy(textEntries[i]);
        }
        textEntries.Clear();
        for (int i = 0; i < entries.Count; i++)
        {
            if (!onlyShowCurUserScores || entries[entries.Count - i - 1].playerName == keyboard.curString)
            {
                GameObject textEntry = Instantiate(textPrefab, entriesParent.transform);
                textEntry.GetComponent<TextMeshProUGUI>().text = entries[entries.Count - i - 1].testName + "| " + entries[entries.Count - i - 1].playerName + ": " + entries[entries.Count - i - 1].points + "\n";
                textEntries.Add(textEntry);
            }
            if (entries[i].playerName == keyboard.curString)
            {
                curPlayerEntries.Add(entries[i]);
            }
        }

       
    }

    public void ToggleOnlyShowCurUserScores()
    {
        if (elapsedTime >= WAIT_TIME)
        {
            if (onlyShowCurUserScores == true)
            {
                onlyShowCurUserScores = false;
                ReSortResultsBySameMethod();
            }
            else
            {
                onlyShowCurUserScores = true;
                ReSortResultsBySameMethod();
            }

            buttonToggle.ToggleLook();

            elapsedTime = 0.0f;
        }
    }

    public void UpdateCurPlayerEntries()
    {
        curPlayerEntries.Clear();

        for (int i = 0; i < entries.Count; i++)
        {
            if (entries[i].playerName == keyboard.curString)
            {
                curPlayerEntries.Add(entries[i]);
            }
        }

        ReSortResultsBySameMethod();
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

    public void ReSortResultsBySameMethod()
    {
        SortType sameMethod = getPreviousSortType();
        switch (sameMethod)
        {
            case SortType.ByRecent:
                SortByRecent();
                break;
            case SortType.HighToLow:
                SortHighLow();
                break;
            case SortType.LowToHigh:
                SortLowHigh();
                break;
        }
    }

    public SortType getPreviousSortType()
    {
        switch (currentSortType)
        {
            case SortType.ByRecent:
                return SortType.LowToHigh;
                break;
            case SortType.HighToLow:
                SortHighLow();
                return SortType.ByRecent;
                break;
            case SortType.LowToHigh:
                SortLowHigh();
                return SortType.HighToLow;
                break;
            default:
                return SortType.ByRecent;
        }
    }

    public void SortResultsWithoutSwitching()
    {
        sortText.text = $"Sorting by\n {currentSortType}";
        switch (currentSortType)
        {
            case SortType.ByRecent:
                SortByRecent();
                break;
            case SortType.HighToLow:
                SortHighLow();
                break;
            case SortType.LowToHigh:
                SortLowHigh();
                break;
        }
    }

    private void SortByRecent()
    {
        AppendResults();
    }
    private void SortHighLow()
    {
        List<InputEntry> sortedEntries = new List<InputEntry>();
        if (!onlyShowCurUserScores) sortedEntries = entries.OrderBy(entry => entry.points).ToList();
        else sortedEntries = curPlayerEntries.OrderBy(entry => entry.points).ToList();

        if (sortedEntries.Count < textEntries.Count)
        {
            for (int i = 0; i < textEntries.Count; i++)
            {
                Destroy(textEntries[i]);
            }
            textEntries.Clear();
        }

        for (int i = 0; i < sortedEntries.Count; i++)
        {
            if (i >= textEntries.Count)
            {
                GameObject textEntry = Instantiate(textPrefab, entriesParent.transform);
                textEntries.Add(textEntry);
            }
            textEntries[i].GetComponent<TextMeshProUGUI>().text = sortedEntries[sortedEntries.Count - i - 1].testName + " | " + sortedEntries[sortedEntries.Count - i - 1].playerName + ": " + sortedEntries[sortedEntries.Count - i - 1].points + "\n";
        }

        if (sortedEntries.Count < textEntries.Count)
        {
            for (int i = sortedEntries.Count; i < textEntries.Count; i++)
            {
                Destroy(textEntries[i]);
                textEntries.RemoveAt(i);
            }
        }
    }
    private void SortLowHigh()
    {
        List<InputEntry> sortedEntries = new List<InputEntry>();
        if (!onlyShowCurUserScores) sortedEntries = entries.OrderBy(entry => entry.points).Reverse().ToList();
        else sortedEntries = curPlayerEntries.OrderBy(entry => entry.points).Reverse().ToList();


        if (sortedEntries.Count < textEntries.Count)
        {
            for (int i = 0; i < textEntries.Count; i++)
            {
                Destroy(textEntries[i]);
            }
            textEntries.Clear();
        }

        for (int i = 0; i < sortedEntries.Count; i++)
        {
            if (i >= textEntries.Count)
            {
                GameObject textEntry = Instantiate(textPrefab, entriesParent.transform);
                textEntries.Add(textEntry);
            }
            textEntries[i].GetComponent<TextMeshProUGUI>().text = sortedEntries[sortedEntries.Count - i - 1].testName + " | " + sortedEntries[sortedEntries.Count - i - 1].playerName + ": " + sortedEntries[sortedEntries.Count - i - 1].points + "\n";
        }
    }
    
}
