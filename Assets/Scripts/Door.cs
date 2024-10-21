using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    private bool open = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Operate()
    {
        open = !open;
        animator.SetBool("OpenDoor", open);
    }
}
