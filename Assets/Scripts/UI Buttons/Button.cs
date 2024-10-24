using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Button : MonoBehaviour
{
    public float deadTime = 1.0f;
    private bool _deadTimeActive = false;

    public UnityEvent onPressed, onReleased;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Button" && !_deadTimeActive)
        {
            onPressed?.Invoke();
            Debug.Log("button pressed");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Button" && !_deadTimeActive)
        {
            onReleased?.Invoke();
            Debug.Log("button released");
            StartCoroutine(WaitForDeadTime());
        }
    }

    IEnumerator WaitForDeadTime()
    {
        _deadTimeActive = true;
        yield return new WaitForSeconds(deadTime);
        _deadTimeActive = false;
    }
}
