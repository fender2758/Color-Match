using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Button : MonoBehaviour
{
    [SerializeField]
    int temp_color;

    private int color_int;
    [SerializeField]
    public int Color_int
    {
        get
        {
            return color_int;
        }
        set
        {
            color_int = value;
            ResetButton();
        }
    }

    TextMeshProUGUI tmp_color;

    Color btn_color;

    private void Start()
    {
        Image img = GetComponent<Image>();
        tmp_color = GetComponentInChildren<TextMeshProUGUI>();
        Color_int = temp_color;

    }

    public void onClick()
    {
        GameManager.instance.SubmitAnswer(btn_color);
    }

    private void ResetButton()
    {
        switch (color_int)
        {
            case 0:
                btn_color = new Color(226f / 255, 42f / 255, 0f / 255);
                //img.color = new Color(226f / 255, 42f / 255, 0f / 255);
                tmp_color.text = "빨강";
                break;
            case 1:
                btn_color = new Color(0f / 255, 73f / 255, 227f / 255);
                //img.color = new Color(0f / 255, 73f / 255, 227f / 255);
                tmp_color.text = "파랑";
                break;
            case 2:
                btn_color = new Color(87f / 255, 229f / 255, 0f / 255);
                //img.color = new Color(87f / 255, 229f / 255, 0f / 255);
                tmp_color.text = "초록";
                break;
            case 3:
                btn_color = new Color(219f / 255, 212f / 255, 0f / 255);
                //img.color = new Color(219f / 255, 212f / 255, 0f / 255);
                tmp_color.text = "노랑";
                break;
            case 4:
                btn_color = new Color(165f / 255, 0f, 255f / 255);
                //img.color = new Color(165f / 255, 0f, 255f / 255);
                tmp_color.text = "보라";
                break;
            case 5:
                btn_color = new Color(255f / 255, 153f / 255, 0f / 255);
                //img.color = new Color(255f / 255, 153f / 255, 0f / 255);
                tmp_color.text = "주황";
                break;
        }
    }
}
