using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ToggleKeyLook : MonoBehaviour
{
    private TextMeshProUGUI text;

    private bool originalLookShowing = true;

    private string originalLook;
    [SerializeField] private string secondaryLook;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        originalLook = text.text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleLook()
    {
        if (originalLookShowing)
        {
            text.text = secondaryLook;
            originalLookShowing = false;
        }
        else
        {
            text.text = originalLook;
            originalLookShowing = true;
        }
    }
}
