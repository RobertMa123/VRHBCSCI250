using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PastResult
{
    public float score;
    public string name;

    public PastResult(float score, string name)
    {
        this.score = score;
        this.name = name;
    }
}
