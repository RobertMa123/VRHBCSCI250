using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// any test with a timer
public class MemoryTest : Test
{
    public enum Direction
    {
        UP = 0,
        DOWN,
        LEFT,
        RIGHT
    }

    public enum MemoryTest_Phases
    {
        START_SEQUENCE = 0,
        LIGHTING_SEQUENCE,
        WAITING_NEXT_BUTTON,
        AWAITING_RESPONSE,
        RESTART_SEQUENCE
    }

    // BUTTONS
    public GameObject button_up;
    public GameObject button_up_inner;
    public GameObject button_up_outer;

    public GameObject button_down;
    public GameObject button_down_inner;
    public GameObject button_down_outer;

    public GameObject button_left;
    public GameObject button_left_inner;
    public GameObject button_left_outer;

    public GameObject button_right;
    public GameObject button_right_inner;
    public GameObject button_right_outer;


    // COLORS
    public Material c_litButtonInner_up;
    public Material c_litButtonOuter_up;
    private Material c_buttonInner_up;
    private Material c_buttonOuter_up;

    public Material c_litButtonInner_down;
    public Material c_litButtonOuter_down;
    private Material c_buttonInner_down;
    private Material c_buttonOuter_down;

    public Material c_litButtonInner_left;
    public Material c_litButtonOuter_left;
    private Material c_buttonInner_left;
    private Material c_buttonOuter_left;

    public Material c_litButtonInner_right;
    public Material c_litButtonOuter_right;
    private Material c_buttonInner_right;
    private Material c_buttonOuter_right;

    public GameObject turnIndicator;
    private Material c_indicator_default;
    public Material c_indicator_playerTurn;
    public Material c_indicator_restart;


    private Direction lastLitButton = Direction.UP;

    MemoryTest_Phases currentPhase = MemoryTest_Phases.START_SEQUENCE;
    private int curCorrectCount = 0;

    private List<Direction> directionSequence = new List<Direction>();

    private float timer = 0.0f;
    [SerializeField] private const float TIME_INTERVAL = 0.5f;

    private int curLightSequenceIndex = 0;

    protected int correctInputs = 0;

    public void Start()
    {
        c_indicator_default = turnIndicator.GetComponent<MeshRenderer>().material;
        hideButtons();
    }

    public MemoryTest() : base()
    {
        testName = "Memory Test";
        highestScoreIsBest = true;

        tutorial.addPage("Welcome to the Memory Test.");
        tutorial.addPage("The test is played like Simon.");
        tutorial.addPage("The cube indicator in the center will light up white after the sequence finishes displaying. This indicates that it is your turn to input the sequence.");
        tutorial.addPage("If you repeat the sequence back correctly, the cube light will turn off and the sequence will replay with one more light added to the end.");
        tutorial.addPage("The cube will turn black if you get it wrong, then restart with a new sequence.");
        tutorial.addPage("Continue to start.");
    }

    public override void UpdateTest()
    {
        if (tutorialFinished)
        {
            if (currentPhase == MemoryTest_Phases.START_SEQUENCE)
            {
                if (timer < TIME_INTERVAL)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    timer = 0.0f;
                    addRandomValueToSequence();
                    currentPhase = MemoryTest_Phases.WAITING_NEXT_BUTTON;
                }
            }
            else if (currentPhase == MemoryTest_Phases.WAITING_NEXT_BUTTON)
            {
                if (timer < TIME_INTERVAL)
                {
                    timer += Time.deltaTime;
                }
                else
                {
                    timer = 0.0f;
                    currentPhase = MemoryTest_Phases.LIGHTING_SEQUENCE;
                    turnIndicator.GetComponent<MeshRenderer>().material = c_indicator_default;
                    StartCoroutine(lightButton(directionSequence[curLightSequenceIndex]));
                }
            }
            else if (currentPhase == MemoryTest_Phases.RESTART_SEQUENCE)
            {
                turnIndicator.GetComponent<MeshRenderer>().material = c_indicator_restart;
                clearSequence();
                currentPhase = MemoryTest_Phases.START_SEQUENCE;
            }
        }
    }

    public override void InitializeTest()
    {
        c_buttonInner_up = button_up_inner.GetComponent<MeshRenderer>().material;
        c_buttonInner_down = button_down_inner.GetComponent<MeshRenderer>().material;
        c_buttonInner_left = button_left_inner.GetComponent<MeshRenderer>().material;
        c_buttonInner_right = button_right_inner.GetComponent<MeshRenderer>().material;

        c_buttonOuter_up = button_up_outer.GetComponent<MeshRenderer>().material;
        c_buttonOuter_down = button_down_outer.GetComponent<MeshRenderer>().material;
        c_buttonOuter_left = button_left_outer.GetComponent<MeshRenderer>().material;
        c_buttonOuter_right = button_right_outer.GetComponent<MeshRenderer>().material;
    }

    public void upButtonPressed()
    {
        buttonPressed(Direction.UP);
    }

    public void downButtonPressed()
    {
        buttonPressed(Direction.DOWN);
    }

    public void leftButtonPressed()
    {
        buttonPressed(Direction.LEFT);
    }

    public void rightButtonPressed()
    {
        buttonPressed(Direction.RIGHT);
    }

    public void buttonPressed(Direction direction)
    {
        if (currentPhase == MemoryTest_Phases.AWAITING_RESPONSE)
        {
            string stringDirection = "";

            if (direction == Direction.UP)
            {
                stringDirection = "up";
            }
            else if (direction == Direction.DOWN)
            {
                stringDirection = "down";
            }
            else if (direction == Direction.LEFT)
            {
                stringDirection = "left";
            }
            else
            {
                stringDirection = "right";
            }

            Debug.Log(stringDirection);

            if (direction == directionSequence[curCorrectCount])
            {
                ++correctInputs;
                if (curCorrectCount >= directionSequence.Count - 1)
                {
                    curCorrectCount = 0;
                    currentPhase = MemoryTest_Phases.START_SEQUENCE;
                }
                else ++curCorrectCount;
            } else
            {
                addScore(directionSequence.Count - 1);
                ++incorrectInputs;
                curCorrectCount = 0;
                currentPhase = MemoryTest_Phases.RESTART_SEQUENCE;
            }
        }
    }

    public override float EndTest()
    {
        hideButtons();

        float highestScore = getHighestScore();

        clearScores();

        clearSequence();

        curCorrectCount = 0;

        incorrectInputs = 0;
        correctInputs = 0;

        curLightSequenceIndex = 0;

        timer = 0.0f;

        currentPhase = MemoryTest_Phases.START_SEQUENCE;

        turnIndicator.GetComponent<MeshRenderer>().material = c_indicator_default;

        return highestScore;
    }

    public void hideButtons() {
        unlightButton(lastLitButton);

        button_up.SetActive(false);
        button_down.SetActive(false);
        button_left.SetActive(false);
        button_right.SetActive(false);
        turnIndicator.SetActive(false);
    }

    public void showButtons() {
        button_up.SetActive(true);
        button_down.SetActive(true);
        button_left.SetActive(true);
        button_right.SetActive(true);
        turnIndicator.SetActive(true);
    }

    public override void setTutorialFinished(bool tutorialFinished) {
        base.setTutorialFinished(tutorialFinished);

        if (!tutorialFinished) showButtons();
    }

    public void lightRandomButton(float delay = 0.75f) {
        StartCoroutine(lightButton(getRandomDirection(), delay));
    }

    public IEnumerator lightButton(Direction direction, float delay = 0.5f) {
        Debug.Log("light button " + direction.ToString());
        yield return new WaitForSeconds(delay);

        if (direction == Direction.UP) {
            button_up_inner.GetComponent<MeshRenderer>().material = c_litButtonInner_up;
            button_up_outer.GetComponent<MeshRenderer>().material = c_litButtonOuter_up;
        } else if (direction == Direction.DOWN) {
            button_down_inner.GetComponent<MeshRenderer>().material = c_litButtonInner_down;
            button_down_outer.GetComponent<MeshRenderer>().material = c_litButtonOuter_down;
        } else if (direction == Direction.LEFT) {
            button_left_inner.GetComponent<MeshRenderer>().material = c_litButtonInner_left;
            button_left_outer.GetComponent<MeshRenderer>().material = c_litButtonOuter_left;
        } else {
            button_right_inner.GetComponent<MeshRenderer>().material = c_litButtonInner_right;
            button_right_outer.GetComponent<MeshRenderer>().material = c_litButtonOuter_right;
        }
        StartCoroutine(unlightButton(direction));
    }

    public IEnumerator unlightButton(Direction direction, float delay = 0.5f) {
        yield return new WaitForSeconds(delay);

        if (direction == Direction.UP) {
            button_up_inner.GetComponent<MeshRenderer>().material = c_buttonInner_up;
            button_up_outer.GetComponent<MeshRenderer>().material = c_buttonOuter_up;
        } else if (direction == Direction.DOWN) {
            button_down_inner.GetComponent<MeshRenderer>().material = c_buttonInner_down;
            button_down_outer.GetComponent<MeshRenderer>().material = c_buttonOuter_down;
        } else if (direction == Direction.LEFT){
            button_left_inner.GetComponent<MeshRenderer>().material = c_buttonInner_left;
            button_left_outer.GetComponent<MeshRenderer>().material = c_buttonOuter_left;
        } else {
            button_right_inner.GetComponent<MeshRenderer>().material = c_buttonInner_right;
            button_right_outer.GetComponent<MeshRenderer>().material = c_buttonOuter_right;
        }

        if (curLightSequenceIndex == directionSequence.Count - 1)
        {
            curLightSequenceIndex = 0;
            turnIndicator.GetComponent<MeshRenderer>().material = c_indicator_playerTurn;
            currentPhase = MemoryTest_Phases.AWAITING_RESPONSE;
        }
        else
        {
            ++curLightSequenceIndex;
            currentPhase = MemoryTest_Phases.WAITING_NEXT_BUTTON;
        }
    }

    public Direction getRandomDirection() {
        int randVal = Random.Range(0, 4);

        if (randVal == 0) return Direction.UP;
        else if (randVal == 1) return Direction.DOWN;
        else if (randVal == 2) return Direction.LEFT;
        else return Direction.RIGHT;
    }

    public void addRandomValueToSequence() {
        directionSequence.Add(getRandomDirection());
    }

    public void clearSequence() {
        directionSequence.Clear();
    }

    public override float getAccuracyPercentage()
    {
        if (correctInputs > 0 || incorrectInputs > 0)
        {
            float totalNumButtonPresses = correctInputs + incorrectInputs;

            float accuracyPercentage = correctInputs / totalNumButtonPresses;
            accuracyPercentage *= 100;

            return accuracyPercentage;
        }
        else
        {
            Debug.Log("Memory Test class getAccuracyPercentage() cannot be executed since there are no scores currently in the scores list. The value 0 was returned.");
            return 0f;
        }
    }
}