using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// any test with a timer
public class TimedTest : Test
{
    private float time = 0.0f;

    private bool timerPaused = false;

    private Canvas canvas;

    private GameObject timerText;

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
    }

    // destroy timer text and return the time as score
    public override float EndTest() {
        score = time;
        time = 0.0f;
        Destroy(timerText);

        return score;
    }
}
