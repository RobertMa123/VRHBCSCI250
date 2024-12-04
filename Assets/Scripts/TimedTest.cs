using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// any test with a timer
public class TimedTest : Test
{
    protected float time = 0.0f;

    protected bool timerPaused = false;

    private Canvas canvas;

    bool regularTimer = false;

    protected GameObject timerText;
    [SerializeField] protected GameObject timerText_VR;

    protected Color curTimerColor;

    public TimedTest() : base() {
        testName = "Timed Test";
        highestScoreIsBest = false;
    }

    public override void UpdateTest()
    {
        if (tutorialFinished) {
            if (!timerPaused)
            {
                if (regularTimer) timerText.SetActive(true);
                timerText_VR.SetActive(true);
                time += Time.deltaTime;
                timerText_VR.GetComponent<TextMeshProUGUI>().text = time.ToString();
                if (regularTimer) timerText.GetComponent<TextMeshProUGUI>().text = time.ToString();
            }
        }
    }

    public override void InitializeTest() {
        // get the canvas in scene
        canvas = GameObject.FindObjectOfType<Canvas>();

        // create timer text and add to canvas
        if (regularTimer)
        {
            timerText = new GameObject("Timer Text");
            timerText.AddComponent<TextMeshProUGUI>().text = time.ToString();
            timerText.GetComponent<TextMeshProUGUI>().fontSize = 30;
            timerText.AddComponent<TextMesh>().alignment = TextAlignment.Left;
            timerText.GetComponent<Transform>().SetParent(canvas.GetComponent<Transform>());
            timerText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            curTimerColor = timerText.GetComponent<TextMeshProUGUI>().color;
            timerText.SetActive(false);
        }

        if (!regularTimer) curTimerColor = timerText_VR.GetComponent<TextMeshProUGUI>().color;
        timerText_VR.SetActive(false);
    }

    // destroy timer text and return the lowest time as score
    public override float EndTest() {
        hideTimer();
        if (regularTimer) Destroy(timerText);

        float lowestScore = getLowestScore();

        clearScores();

        incorrectInputs = 0;

        return lowestScore;
    }

    protected void setTimerColor(Color color) {
        if (regularTimer) timerText.GetComponent<TextMeshProUGUI>().color = color;
        timerText_VR.GetComponent<TextMeshProUGUI>().color = color;
        curTimerColor = color;
    }

    protected void pauseTimer() {
        timerPaused = true;
    }

    protected void startTimer() {
        timerPaused = false;
    }

    protected void toggleStartTimer() {
        if (timerPaused) {
            timerPaused = false;
        } else {
            timerPaused = true;
        }
    }

    protected void showTimer() {
        if (regularTimer) timerText.gameObject.SetActive(true);
        timerText_VR.gameObject.SetActive(true);
    }

    protected void hideTimer() {
        if (regularTimer) timerText.gameObject.SetActive(false);
        timerText_VR.gameObject.SetActive(false);
    }

    protected void toggleShowTimer() {
        if (timerText.gameObject.activeInHierarchy || timerText_VR.gameObject.activeInHierarchy) {
            hideTimer();
        } else {
            showTimer();
        }
    }

    protected void saveTime() {
        addScore(time);
    }

    protected void resetTime() {
        time = 0.0f;
    }

    protected void saveAndResetTime() {
        saveTime();
        resetTime();
    }

    protected void startShowTime() {
        startTimer();
        showTimer();
    }

    protected void pauseResetHideTime() {
        pauseTimer();
        resetTime();
        hideTimer();
    }

    protected void pauseSaveResetHideTime() {
        pauseTimer();
        saveAndResetTime();
        hideTimer();
    }
}
