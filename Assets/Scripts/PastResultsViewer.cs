using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PastResultsViewer : MonoBehaviour
{
    [SerializeField] private GameObject pastResultsViewer;
    [SerializeField] private List<GameObject> nonHeaderRows;

    [SerializeField] private List<PastResult> pastResults;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showPastResults()
    {
        pastResultsViewer.SetActive(true);
    }

    public void hidePastResults()
    {
        pastResultsViewer.SetActive(false);
    }
}
