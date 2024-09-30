using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonVR_RigHand : MonoBehaviour
{
    public GameObject parent;
    void Update()
    {
        this.transform.rotation = parent.transform.rotation;
    }
}
