using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract test class for TestManager monobehaviour to polymorphically call functions of for any type of test derived from this class
public abstract class Test : MonoBehaviour
{
    protected List<float> scores = new List<float>();

    public Test() {
        
    }

    public Test(float score) {
        addScore(score);
    }

    public Test(List<float> scores) {
        setScores(scores);
    }

    // what should be done to set up the test at the time the test is set active
    public abstract void InitializeTest();

    // what should be called each frame while test is active
    public abstract void UpdateTest();

    // destroys all objects created for test and returns the score
    public abstract float EndTest();

    protected void clearScores() {
        scores.Clear();
    }

    protected void addScore(float score) {
        scores.Add(score);
    }

    protected void addScores(List<float> scores) {
        for (int i = 0; i < scores.Count; ++i) {
            this.scores.Add(scores[i]);
        }
    }

    protected void setScores(List<float> scores) {
        clearScores();

        for (int i = 0; i < scores.Count; ++i) {
            this.scores.Add(scores[i]);
        }
    }

    public float getScore(int index) {
        if (index >= 0 && index < scores.Count) {
            return scores[index];
        } else {
            Debug.Log("Test class getScore() index of " + index + " was an invalid request due to an invalid index. The value 0 was returned.");
            return 0f;
        }
    }

    public float getMostRecentScore() {
        if (scores.Count > 0) {
            return scores[scores.Count - 1];
        } else {
            Debug.Log("Test class getMostRecentScore() cannot be executed since there are no scores currently in the scores list. The value 0 was returned.");
            return 0f;
        }
    }

    public float getHighestScore() {
        if (scores.Count > 0) {
            float highestScore = scores[0];
            for (int i = 1; i < scores.Count; ++i) {
                if (scores[i] > highestScore) {
                    highestScore = scores[i];
                }
            }
            return highestScore;
        } else {
            Debug.Log("Test class getHighestScore() cannot be executed since there are no scores currently in the scores list. The value 0 was returned.");
            return 0f;
        }
    }

    public float getLowestScore() {
        if (scores.Count > 0) {
            float lowestScore = scores[0];
            for (int i = 1; i < scores.Count; ++i) {
                if (scores[i] < lowestScore) {
                    lowestScore = scores[i];
                }
            }
            return lowestScore;
        } else {
            Debug.Log("Test class getLowestScore() cannot be executed since there are no scores currently in the scores list. The value 0 was returned.");
            return 0f;
        }
    }

    public List<float> getScores() {
        return scores;
    }
}
