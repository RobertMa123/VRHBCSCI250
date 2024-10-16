using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ReactionTest : TimedTest
{
    [SerializeField] protected GameObject leftArrow;
    [SerializeField] protected GameObject rightArrow;

    public enum Reaction_curDirection {
        NONE = 0,
        LEFT,
        RIGHT,
    }

    protected int minWaitTime = 2;
    protected int maxWaitTime = 3;

    protected float intervalTimer = 0f;
    protected bool intervalTimerRunning = true;

    protected Reaction_curDirection curDirection = Reaction_curDirection.NONE;

    public override void UpdateTest() {
        base.UpdateTest();

        if (curDirection == Reaction_curDirection.NONE) {
            if (intervalTimer <= 0) {
                showArrow();
                startShowTime();
                intervalTimer = Random.Range(minWaitTime, maxWaitTime);
            } else {
                intervalTimer -= Time.deltaTime;
            }
        } else { 
            if (Keyboard.current.leftArrowKey.wasPressedThisFrame) {
                if (curDirection == Reaction_curDirection.LEFT) {
                    correctAnswerGiven();
                } else {
                    wrongAnswerGiven();
                }
            } else if (Keyboard.current.rightArrowKey.wasPressedThisFrame) {
                if (curDirection == Reaction_curDirection.RIGHT) {
                    correctAnswerGiven();
                } else {
                    wrongAnswerGiven();
                }
            }
        }
    }

    public override void InitializeTest() {
        base.InitializeTest();

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
        Debug.Log("Correct");
        pauseSaveResetHideTime();
        hideArrow();
        intervalTimer = Random.Range(minWaitTime, maxWaitTime);
    }

    protected void wrongAnswerGiven() {
        Debug.Log("Wrong");
        pauseResetHideTime();
        hideArrow();
        intervalTimer = Random.Range(minWaitTime, maxWaitTime);
    }
}
