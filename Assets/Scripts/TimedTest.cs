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

    protected GameObject timerText;

    protected Color curTimerColor;

    public TimedTest() : base() {

    }

    public override void UpdateTest()
    {
        if (!timerPaused) {
            time += Time.deltaTime;
            timerText.GetComponent<TextMeshProUGUI>().text = time.ToString();
        }
    }

    public override void InitializeTest() {
        // get the canvas in scene
        canvas = GameObject.FindObjectOfType<Canvas>();

        // create timer text and add to canvas
        timerText = new GameObject("Timer Text");
        timerText.AddComponent<TextMeshProUGUI>().text = time.ToString();
        timerText.GetComponent<TextMeshProUGUI>().fontSize = 30;
        timerText.AddComponent<TextMesh>().alignment = TextAlignment.Left;
        timerText.GetComponent<Transform>().SetParent(canvas.GetComponent<Transform>());
        timerText.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
        curTimerColor = timerText.GetComponent<TextMeshProUGUI>().color;
    }

    // destroy timer text and return the lowest time as score
    public override float EndTest() {
        Destroy(timerText);

        float lowestScore = getLowestScore();

        clearScores();

        return lowestScore;
    }

    protected void setTimerColor(Color color) {
        timerText.GetComponent<TextMeshProUGUI>().color = color;
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
        timerText.gameObject.SetActive(true);
    }

    protected void hideTimer() {
        timerText.gameObject.SetActive(false);
    }

    protected void toggleShowTimer() {
        if (timerText.gameObject.activeInHierarchy) {
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
