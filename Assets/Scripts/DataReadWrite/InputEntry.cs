using System;

[Serializable]
public class InputEntry {
    public string playerName;
    public float points;

    public InputEntry (string name, float points) {
        playerName = name;
        this.points = points;
    }
}



