using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PastResultsViewer : MonoBehaviour
{
    [SerializeField] private GameObject pastResultsViewer;
    [SerializeField] private List<GameObject> nonHeaderRows;

    [SerializeField] private TextAsset jsonDatabase;
    private ItemsList itemsList = new ItemsList();

    // FOR LOADING FROM JSON DATABASE
    [System.Serializable]
    public class Items
    {
        public string playerName;
        public float points;

        // Comparison Functions
        public bool isLessThan(Items other)
        {
            if (points < other.points) return true;
            else return false;
        }

        public bool isGreaterThan(Items other)
        {
            if (points > other.points) return true;
            else return false;
        }

        public bool isEqualTo(Items other)
        {
            if (points == other.points) return true;
            else return false;
        }

        public bool isNotEqualTo(Items other)
        {
            if (points != other.points) return true;
            else return false;
        }
    }

    [System.Serializable]
    public class ItemsList
    {
        public Items[] items;
    }

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

        for (int i = 0; i < nonHeaderRows.Count; ++i)
        {
            if (i < itemsList.items.Length)
            {
                nonHeaderRows[i].gameObject.SetActive(true);
                nonHeaderRows[i].GetComponent<PastResult_Row>().rank.text = (i+1).ToString();
                nonHeaderRows[i].GetComponent<PastResult_Row>().score.text = itemsList.items[i].points.ToString();
                nonHeaderRows[i].GetComponent<PastResult_Row>().name.text = itemsList.items[i].playerName;
            }
            else
            {
                nonHeaderRows[i].gameObject.SetActive(false);
            }
        }
    }

    public void hidePastResults()
    {
        pastResultsViewer.SetActive(false);
    }

    public void sortPastResults()
    {
        for (int i = 0; i < itemsList.items.Length - 2; ++i)
        {
            for (int j = 0; j < itemsList.items.Length - 2; ++j)
            {
                if (itemsList.items[i].isGreaterThan(itemsList.items[i+1]))
                {
                    Items tempItems = itemsList.items[i+1];
                    itemsList.items[i+1] = itemsList.items[i];
                    itemsList.items[i] = tempItems;
                }
            }
        }
    }

    public void loadPastResultsFromDatabase()
    {
        itemsList = JsonUtility.FromJson<ItemsList>(jsonDatabase.text);

        sortPastResults();
    }
}
