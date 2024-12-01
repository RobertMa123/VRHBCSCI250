using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour {
    [SerializeField] string filename;
    [SerializeField] VRKeyboard keyboard;

    List<InputEntry> entries = new List<InputEntry> ();

    private void Start () {
        entries = FileHandler.ReadListFromJSON<InputEntry> (filename);
    }

    public void AddNameToList (string testName, float score) {

        if (keyboard.curString == null) entries.Add(new InputEntry("player",testName, score));
        else
        {
            entries.Add(new InputEntry(keyboard.curString, testName, score));
        }

        FileHandler.SaveToJSON<InputEntry> (entries, filename);
    }
}