using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// test manager monobehaviour to call the test functions of the current test
// THERE SHOULD BE ONLY ONE TEST MANAGER IN SCENE
public class TestManager : MonoBehaviour
{
    private Test currentTest;
    
    private float currentScore = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (currentTest != null) {
            currentTest.InitializeTest();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTest != null) {
            currentTest.UpdateTest();
        }
    }

    // end the test, set current score to the test's score, and remove the test
    public void endTest() {
        if (currentTest != null) {
            currentScore = currentTest.EndTest();
            Debug.Log("Score: " + currentScore);
            currentTest = null;
        }
    }

    // set the current test to newTest if there is no current test
    public void setTest(Test newTest) {
        if (currentTest == null) {
            this.currentTest = newTest;
            currentScore = 0f;
            currentTest.InitializeTest();
        }
    }

    // set the current test to newTest, overriding the current test if one exists
    public void setTestOverride(Test newTest) {
        endTest();
        this.currentTest = null;
        setTest(newTest);
    }
}
