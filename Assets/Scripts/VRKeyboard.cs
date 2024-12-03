using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VRKeyboard : MonoBehaviour
{
    public TextMeshProUGUI text;

    public string curString = "";

    private TextMeshPro outputText;

    private bool caps = false;

    [SerializeField] private List<ToggleKeyLook> keyLookToggles = new List<ToggleKeyLook>();

    private float timeElapsed = 0.0f;
    private const float WAIT_TIME = 1.0f;


    // Start is called before the first frame update
    void Start()
    {

        outputText = transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        timeElapsed = WAIT_TIME;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeElapsed < WAIT_TIME) timeElapsed += Time.deltaTime;
    }

    public void A()
    {
        ConcatenateAppropriateCap("A", "a");
    }

    public void B()
    {
        ConcatenateAppropriateCap("B", "b");
    }
    public void C()
    {
        ConcatenateAppropriateCap("C", "c");
    }
    public void D()
    {
        ConcatenateAppropriateCap("D", "d");
    }
    public void E()
    {
        ConcatenateAppropriateCap("E", "e");
    }
    public void F()
    {
        ConcatenateAppropriateCap("F", "f");
    }
    public void G()
    {
        ConcatenateAppropriateCap("G", "g");
    }
    public void H()
    {
        ConcatenateAppropriateCap("H", "h");
    }
    public void I()
    {
        ConcatenateAppropriateCap("I", "i");
    }
    public void J()
    {
        ConcatenateAppropriateCap("J", "j");
    }
    public void K()
    {
        ConcatenateAppropriateCap("K", "k");
    }
    public void L()
    {
        ConcatenateAppropriateCap("L", "l");
    }
    public void M()
    {
        ConcatenateAppropriateCap("M", "m");
    }
    public void N()
    {
        ConcatenateAppropriateCap("N", "n");
    }
    public void O()
    {
        ConcatenateAppropriateCap("O", "o");
    }
    public void P()
    {
        ConcatenateAppropriateCap("P", "p");
    }
    public void Q()
    {
        ConcatenateAppropriateCap("Q", "q");
    }
    public void R()
    {
        ConcatenateAppropriateCap("R", "r");
    }
    public void S()
    {
        ConcatenateAppropriateCap("S", "s");
    }
    public void T()
    {
        ConcatenateAppropriateCap("T", "t");
    }
    public void U()
    {
        ConcatenateAppropriateCap("U", "u");
    }
    public void V()
    {
        ConcatenateAppropriateCap("V", "v");
    }
    public void W()
    {
        ConcatenateAppropriateCap("W", "w");
    }
    public void X()
    {
        ConcatenateAppropriateCap("X", "x");
    }
    public void Y()
    {
        ConcatenateAppropriateCap("Y", "y");
    }
    public void Z()
    {
        ConcatenateAppropriateCap("Z", "z");
    }
    
    public void ToggleCaps()
    {
        if (timeElapsed >= WAIT_TIME)
        {
            if (caps) caps = false;
            else caps = true;

            for (int i = 0; i < keyLookToggles.Count; ++i)
            {
                keyLookToggles[i].ToggleLook();
            }

            timeElapsed = 0.0f;
        }
    }


    public void Backspace()
    {
        if (curString.Length > 0)
        {
            curString = curString.Substring(0, curString.Length - 1);
        }
        updateDisplay();
    }

    // concatenates the capital string if caps is true, else concatenates lowercaseString
    public void ConcatenateAppropriateCap(string capitalString, string lowercaseString)
    {
        if (timeElapsed >= WAIT_TIME)
        {
            if (caps == true)
            {
                Concatenate(capitalString);
            } else
            {
                Concatenate(lowercaseString);
            }

            timeElapsed = 0.0f;
        }
    }

    public void Concatenate(string stringToConcat)
    {
        curString += stringToConcat;
        outputText.text = $"You are {curString}";
        updateDisplay();
    }

    public void Clear()
    {
        curString = "";
        updateDisplay();
    }

    public void Overwrite(string stringToOverwriteWith)
    {
        curString = stringToOverwriteWith;
        updateDisplay();
    }

    public void Enter()
    {
        // ADD CODE TO SAVE INITIALS TO DATABASE
        Clear();
    }

    private void updateDisplay()
    {
        text.text = curString;
    }

   
}
