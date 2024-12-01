using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

// test manager monobehaviour to call the test functions of the current test
// THERE SHOULD BE ONLY ONE TEST MANAGER IN SCENE
public class TestManager : MonoBehaviour
{
    private Test currentTest;

    [SerializeField] private TutorialUIObject tutorialUI;
    [SerializeField] private ResultsUIObject resultsUI;

    private List<float> scores = new List<float>();

    private bool scoreIsShowing = false;
    private bool tutorialIsShowing = false;


    // Start is called before the first frame update
    void Start()
    {
        if (resultsUI != null) resultsUI.testScoreViewer.SetActive(false);
        if (tutorialUI != null) tutorialUI.tutorialViewer.SetActive(false);

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

        if (scoreIsShowing) {
            if (Keyboard.current.fKey.wasPressedThisFrame) {
                hideScores();
            }
        }
        if (tutorialIsShowing) {
            if (Keyboard.current.fKey.wasPressedThisFrame) {
                tutorialNextPage();
            }
        }
    }

    // end the test, set current score to the test's score, and remove the test
    public void endTest()
    {
        if (currentTest != null)
        {

            scores = currentTest.getScores();
            InputHandler inputHandler = GetComponent<InputHandler>();
            if (inputHandler != null && scores.Count >= 2) inputHandler.AddNameToList(scores[1]);
            setScoreViewer();
            if (scores.Count > 0) showScores();
            currentTest.EndTest();
            currentTest = null;

        }
    }

    // set the current test to newTest if there is no current test
    public void setTest(Test newTest)
    {
        if (currentTest == null)
        {
            this.currentTest = newTest;
            currentTest.InitializeTest();
            setTutorialViewer();
        }
    }

    // set the current test to newTest, overriding the current test if one exists
    public void setTestOverride(Test newTest)
    {
        endTest();
        this.currentTest = null;
        setTest(newTest);
    }

    // ******************** Score Viewer Functions ********************

    public void setScoreViewer() {
        if (currentTest != null) {
            resultsUI = currentTest.getResultsUI();
            resultsUI.text_testType.text = currentTest.getTestName();
            resultsUI.text_avg.text = "Average Score: " + currentTest.getAverageScore().ToString("0.00");
            resultsUI.text_best.text = "Best Score: " + currentTest.getBestScore().ToString("0.00");
            resultsUI.text_worst.text = "Worst Score: " + currentTest.getWorstScore().ToString("0.00");
            resultsUI.text_accuracy.text = "Accuracy: " + currentTest.getAccuracyPercentage().ToString("0.00") + "%";
        }
    }

    public void showScores() {
        resultsUI.testScoreViewer.SetActive(true);
        scoreIsShowing = true;
    }

    public void hideScores() {
        resultsUI.testScoreViewer.SetActive(false);
        scoreIsShowing = false;
    }

    // ******************** Tutorial Viewer Functions ********************

    public void setTutorialViewer()
    {
        if (currentTest != null)
        {
            tutorialUI = currentTest.getTutorialUI();
            tutorialUI.tutorialViewer.SetActive(false);
            tutorialUI.text_tutorialTestType.text = currentTest.getTestName() + " Tutorial";
            currentTest.setTutorialFinished(false);
            setTutorialPage(0);

            showTutorial();
        }
    }

    public void setTutorialPage(int tutorialPage) {
        if (currentTest != null)
        {
            currentTest.getTestTutorial().setCurPage(tutorialPage);
            tutorialUI.text_instructions.text = currentTest.getTestTutorial().getCurText();
        }
    }

    public void tutorialNextPage() {
        if (currentTest != null) {
            int newPageNum = currentTest.getTestTutorial().increaseCurPage();
            if (newPageNum < 0) {
                hideTutorial();
            } else {
                setTutorialPage(newPageNum);
            }
        }
    }

    public void tutorialPreviousPage() {
        if (currentTest != null) {
            int newPageNum = currentTest.getTestTutorial().decreaseCurPage();
            if (newPageNum >= 0) {
                setTutorialPage(newPageNum);
            }
        }
    }

    public void showTutorial() {
        tutorialUI.tutorialViewer.SetActive(true);
        tutorialIsShowing = true;
    }

    public void hideTutorial()
    {
        if (currentTest != null) {
            tutorialUI.tutorialViewer.SetActive(false);
            tutorialIsShowing = false;
            currentTest.setTutorialFinished(true);
        }
    }
}
