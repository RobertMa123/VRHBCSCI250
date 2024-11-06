using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTutorial
{
    protected string testName;

    protected List<string> pages = new List<string>();

    protected int curPage = 0;

    public TestTutorial()
    {

    }

    public TestTutorial(string testName)
    {
        this.testName = testName;
    }

    public int getCurPage()
    {
        return curPage;
    }

    public void setCurPage(int pageNumber)
    {
        if (pageNumber < 0) Debug.Log("TestTutorial class SetCurPage() page number was negative, nothing was executed.");
        else if (pageNumber >= pages.Count) Debug.Log("Test Tutorial class SetCurPage() page number was greater than the index of last page, nothing was executed.");
        else
        {
            this.curPage = pageNumber;
        }
    }

    // returns new value of curPage if increased, or -1 if the value was not increased but was already at max page number
    public int increaseCurPage()
    {
        if (curPage + 1 < pages.Count)
        {
            ++curPage;
            return curPage;
        }
        else
        {
            return -1;
        }
    }

    // returns new value of curPage if decreased, or -1 if the value was not decreased but was already at 0
    public int decreaseCurPage()
    {
        if (curPage - 1 >= 0)
        {
            --curPage;
            return curPage;
        } else
        {
            return -1;
        }
    }

    public string getCurText()
    {
        if (pages.Count > 0) return pages[curPage];
        else
        {
            Debug.Log("TestTutorial class getCurText() function returned empty string since page count is 0");
            return "";
        }
    }

    public void addPage(string text) {
        pages.Add(text);
    }
}
