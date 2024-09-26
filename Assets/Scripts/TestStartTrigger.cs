using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStartTrigger : MonoBehaviour
{
    private Renderer indicatorRenderer;
    [SerializeField] private float colorChangeSpeed = 0.5f;
    private Color startColor;
    [SerializeField] private Color endColor;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
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
            yield return null;
        }
    }
}
