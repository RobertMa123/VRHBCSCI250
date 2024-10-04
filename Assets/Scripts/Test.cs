using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract test class for TestManager monobehaviour to polymorphically call functions of for any type of test derived from this class
public abstract class Test : MonoBehaviour
{
    protected float score;

    public Test() {
        score = 0f;
    }

    public Test(float score) {
        setScore(score);
    }

    // what should be done to set up the test at the time the test is set active
    public abstract void InitializeTest();

    // what should be called each frame while test is active
    public abstract void UpdateTest();

    // destroys all objects created for test and returns the score
    public abstract float EndTest();

    protected void setScore(float score) {
        this.score = score;
    }

    public float getScore() {
        return score;
    }
}
