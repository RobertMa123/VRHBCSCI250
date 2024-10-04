using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStartTrigger : MonoBehaviour
{
    // the test this test start trigger should trigger
    [SerializeField] private Test test;

    // the test manager
    private TestManager testManager;

    // renderer component of the test start indicator
    private Renderer indicatorRenderer;

    // speed at which to change the color of the button when pressed
    [SerializeField] private float colorChangeSpeed = 0.5f;

    // color of button when unpressed (set to whatever color the indicator is on start)
    private Color startColor;

    // color of button when pressed down
    [SerializeField] private Color endColor;
    
    // animator of the test start button
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        testManager = GameObject.FindObjectOfType<TestManager>();

        indicatorRenderer = GetComponent<Renderer>();
        startColor = indicatorRenderer.material.color;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When step on test starter
    private void OnTriggerEnter(Collider other) {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleUp")) {
            if (other.gameObject.tag == "Player") {
                StartCoroutine(ChangeColor());
                animator.SetTrigger("Press");
            }
        }

        
    }

    // When step off test starter
    private void OnTriggerExit(Collider other) {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("IdleDown")) {
            if (other.gameObject.tag == "Player") {
                StartCoroutine(ChangeColor(true));
                animator.SetTrigger("Depress");
            }
        }
    }

    // Fade color into next color (reverse = false goes from startColor to endColor, and reverse = true does that in reverse)
    private IEnumerator ChangeColor(bool reverse = false) {
        float tick = 0f;
        
        Color colorToTurnTo = endColor;
        Color colorToStartFrom = startColor;
        
        if (reverse) {
            colorToTurnTo = startColor;
            colorToStartFrom = endColor;
        }

        while (indicatorRenderer.material.color != colorToTurnTo) {
            tick += Time.deltaTime * colorChangeSpeed;
            indicatorRenderer.material.color = Color.Lerp(colorToStartFrom, colorToTurnTo, tick);
            if (indicatorRenderer.material.color == colorToTurnTo) {
                // set test manager's current test to this test start trigger's test when push down is done
                if (!reverse) testManager.setTestOverride(test);
                // end test manager's current test when pop back up is done (for now, may want to change it so stepping off trigger doesn't end the test)
                else testManager.endTest();
            }
            yield return null;
        }

    }

    public void setTest(Test test) {
        this.test = test;
    }

    public Test getTest() {
        return test;
    }
}
