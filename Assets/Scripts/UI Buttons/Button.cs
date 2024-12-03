using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Unity.VisualScripting.Member;


public class Button : MonoBehaviour
{
    public bool bDebug = false;

    public float deadTime = 1.0f;
    private bool _deadTimeActive = false;

    public UnityEvent onPressed, onReleased;

    [Space(5f)]
    private AudioSource source;
    [SerializeField] private AudioClip buttonDown;
    [SerializeField] private AudioClip buttonUp;

    private void Start()
    {
        try
        {
            source = transform.GetComponentInParent<AudioSource>();
        }
        catch { Debug.Log("No audio source found in parent"); };
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Button" && !_deadTimeActive)
        {
            onPressed?.Invoke();
            if (bDebug) Debug.Log("button pressed");
        }
        PlaySound(buttonDown);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Button" && !_deadTimeActive)
        {
            onReleased?.Invoke();
            if (bDebug) Debug.Log("button released");
            StartCoroutine(WaitForDeadTime());
        }
        PlaySound(buttonUp);
    }

    IEnumerator WaitForDeadTime()
    {
        _deadTimeActive = true;
        yield return new WaitForSeconds(deadTime);
        _deadTimeActive = false;
    }

    private void PlaySound(AudioClip clip)
    {
        try
        {

            source.pitch = Random.Range(0.8f, 1.2f);
            source.volume = 0.6f;
            source.PlayOneShot(clip);
        }
        catch { Debug.Log("Couldn't play sound effect. Check for missing variable references."); }
    }
}
