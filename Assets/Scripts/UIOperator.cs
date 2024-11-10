using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOperator : MonoBehaviour
{
    public bool active;

    private void Start()
    {
        this.gameObject.SetActive(active);   
    }

    public void Activate()
    {
        active = !active;
        this.gameObject.SetActive(active);
    }
}
