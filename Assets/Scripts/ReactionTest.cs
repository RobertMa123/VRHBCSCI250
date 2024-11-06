using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class ReactionTest : TimedTest
{
    [SerializeField] protected GameObject leftArrow;
    [SerializeField] protected GameObject rightArrow;
    private InputData input;
    public enum Reaction_curDirection {
        NONE = 0,
        LEFT,
        RIGHT,
    }

    protected int minWaitTime = 1;
    protected int maxWaitTime = 5;

    protected float intervalTimer = 0f;
    protected bool intervalTimerRunning = true;

    protected Reaction_curDirection curDirection = Reaction_curDirection.NONE;

    Color normalColor;
    Color wrongColor = new Color(100f, 0f, 0f, 100f);

    protected bool acceptingInput = false;

    public ReactionTest() : base() {
        testName = "Reaction Test";
        highestScoreIsBest = false;

        tutorial.addPage("Welcome to the basic reacion test.");
        tutorial.addPage("When the test starts, either a left or right arrow will appear on screen at random.");
        tutorial.addPage("A timer on-screen will increase in time as soon as the arrow appears.");
        tutorial.addPage("Press the corresponding controller's trigger as fast as you can to stop the timer.");
        tutorial.addPage("If your input is correct, the timer will remain white when it stops. However, if your input is incorrect, the timer will turn red.");
        tutorial.addPage("The timer and arrow will then disappear. After a few seconds, a new arrow will appear for you to react to.");
        tutorial.addPage("Once you are done testing your reaction speed, step off the standing platform and your results will appear.");
        tutorial.addPage("Try to prioritize speed while maintaining accuracy.\n\nContinue to start the test.");
    }

    public override void UpdateTest()
    {
        base.UpdateTest();

        if (tutorialFinished) {
            if (curDirection == Reaction_curDirection.NONE)
            {
                if (intervalTimer <= 0)
                {
                    showArrow();
                    acceptingInput = true;
                    startShowTime();
                    intervalTimer = Random.Range(minWaitTime, maxWaitTime);
                }
                else
                {
                    intervalTimer -= Time.deltaTime;
                }
            }
            else if (acceptingInput)
            {
                if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
                {
                    acceptingInput = false;
                    if (curDirection == Reaction_curDirection.LEFT)
                    {
                        correctAnswerGiven();
                    }
                    else
                    {
                        wrongAnswerGiven();
                    }
                }
                else if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
                {
                    acceptingInput = false;
                    if (curDirection == Reaction_curDirection.RIGHT)
                    {
                        correctAnswerGiven();
                    }
                    else
                    {
                        wrongAnswerGiven();
                    }
                }
            }
        }
    }

    public override void InitializeTest() {
        base.InitializeTest();

        normalColor = curTimerColor;

        leftArrow.gameObject.SetActive(false);
        rightArrow.gameObject.SetActive(false);
    }

    public override float EndTest() {
        leftArrow.gameObject.SetActive(false);
        rightArrow.gameObject.SetActive(false);

        float lowestScore = getLowestScore();
        base.EndTest();

        return lowestScore;
    }

    protected void showArrow() {
        int left = Random.Range(0, 2);

        if (left != 0) {
            curDirection = Reaction_curDirection.LEFT;
            leftArrow.gameObject.SetActive(true);
        }
        else {
            curDirection = Reaction_curDirection.RIGHT;
            rightArrow.gameObject.SetActive(true);
        }
    }

    protected void hideArrow() {
        if (curDirection == Reaction_curDirection.LEFT) {
            leftArrow.gameObject.SetActive(false);
        } else {
            rightArrow.gameObject.SetActive(false);
        }
        curDirection = Reaction_curDirection.NONE;
    }

    protected void correctAnswerGiven() {
        pauseTimer();
        StartCoroutine(delayHideArrowCorrect(1.0f));
    }

    protected void wrongAnswerGiven() {
        ++incorrectInputs;
        pauseTimer();
        setTimerColor(wrongColor);
        StartCoroutine(delayHideArrowWrong(1.0f));
    }

    protected IEnumerator delayHideArrowCorrect(float seconds) {
        yield return new WaitForSeconds(seconds);
        pauseSaveResetHideTime();
        hideArrow();
        intervalTimer = Random.Range(minWaitTime, maxWaitTime);
    }

    protected IEnumerator delayHideArrowWrong(float seconds) {
        yield return new WaitForSeconds(seconds);
        pauseResetHideTime();
        hideArrow();
        intervalTimer = Random.Range(minWaitTime, maxWaitTime);
        setTimerColor(normalColor);
    }
}
