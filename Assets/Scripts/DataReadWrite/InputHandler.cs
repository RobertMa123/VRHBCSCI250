using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour {
    [SerializeField] InputField nameInput;
    [SerializeField] string filename;
    public float score;

    List<InputEntry> entries = new List<InputEntry> ();

    private void Start () {
        entries = FileHandler.ReadListFromJSON<InputEntry> (filename);
    }

    public void AddNameToList () {

        if (nameInput == null) entries.Add(new InputEntry("player", score));
        else
        {
            entries.Add(new InputEntry(nameInput.text, score));
            nameInput.text = "";
        }

        FileHandler.SaveToJSON<InputEntry> (entries, filename);
    }
}