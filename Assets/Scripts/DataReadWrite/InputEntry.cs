using System;

[Serializable]
public class InputEntry {
    public string playerName;
    public string testName;
    public float points;

    public InputEntry (string name, string testName, float points) {
        playerName = name;
        this.testName = testName;
        this.points = points;
    }
}



