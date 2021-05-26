using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    Color textColor, edgeColor;
    public Color TextColor
    {
        get { return textColor; }
        set
        {
            textColor = value;
            text.color = value;
        }
    }
    public Color EdgeColor
    {
        get { return edgeColor; }
        set
        {
            edgeColor = value;
            outer.color = value;
        }
    }
    public Image outer, inner;
    public TextMeshProUGUI text;

    /** wordString is same as answer.*/
    string wordString;
    public string WordString
    {
        get
        {
            return wordString;
        }
        set
        {
            wordString = value;
            text.text = wordString;
        }
    }
}
