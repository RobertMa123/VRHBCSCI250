using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialTutorial : MonoBehaviour
{
    [SerializeField] GameObject[] stages;
    private int currStage = 0;

    private void Start()
    {
        stages[currStage].SetActive(true);
        for(int i = 1; i < stages.Length; i++)
        {
            stages[i].SetActive(false);
        }
    }
    public void NextStage()
    {
        stages[currStage].SetActive(false);
        if (currStage < stages.Length - 1)
        {
            currStage++;
            stages[currStage].SetActive(true);
        }
    }
}
