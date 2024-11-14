using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOperator : MonoBehaviour
{
    private bool toggle = false;
    public void Activate()
    {
        this.gameObject.SetActive(toggle);
        toggle = !toggle;
    }
}
