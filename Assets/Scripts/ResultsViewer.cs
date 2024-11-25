using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NewBehaviourScript : MonoBehaviour
{
    List<InputEntry> entries = new List<InputEntry>();
    [SerializeField] string filename;
    public TextMeshProUGUI[] text;

    private void Update()
    {
        entries = FileHandler.ReadListFromJSON<InputEntry>(filename);
        for(int i = 0; i< text.Length; i++)
        {
            text[i].text = entries[entries.Count - i - 1].testName + "| " + entries[entries.Count - i - 1].playerName + ": " + entries[entries.Count - i - 1].points + "\n";
        }
    }
}
