using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizCreator : MonoBehaviour
{
    [SerializeField]
    GameObject quizPrefab;
    [SerializeField]
    Canvas mainCanvas;


    enum colors
    {
        RED, BLUE, GREEN, YELLOW, PURPLE, ORANGE
    };

    

    public Quiz Create(bool is_easy)
    {
        GameObject tempObj = Instantiate(quizPrefab, new Vector3(0,100,0), Quaternion.identity, mainCanvas.transform);
        Quiz temp = tempObj.GetComponent<Quiz>();

        var i = (colors)Random.Range(0, 6);
        var j = (colors)Random.Range(0, 6);
        var k = (colors)Random.Range(0, 6);

        switch (i)
        {
            case colors.RED:
                temp.TextColor = new Color(226f / 255, 42f / 255, 0f / 255);
                if(is_easy) temp.EdgeColor = new Color(226f / 255, 42f / 255, 0f / 255);
                break;
            case colors.BLUE:
                temp.TextColor = new Color(0f / 255, 73f / 255, 227f / 255);
                if (is_easy) temp.EdgeColor = new Color(0f / 255, 73f / 255, 227f / 255);
                break;
            case colors.GREEN:
                temp.TextColor = new Color(87f / 255, 229f / 255, 0f / 255);
                if (is_easy) temp.EdgeColor = new Color(87f / 255, 229f / 255, 0f / 255);
                break;
            case colors.YELLOW:
                temp.TextColor = new Color(219f / 255, 212f / 255, 0f / 255);
                if (is_easy) temp.EdgeColor = new Color(219f / 255, 212f / 255, 0f / 255);
                break;
            case colors.PURPLE:
                temp.TextColor = new Color(165f / 255, 0f, 255f / 255);
                if (is_easy) temp.EdgeColor = new Color(165f / 255, 0f, 255f / 255);
                break;
            case colors.ORANGE:
                temp.TextColor = new Color(255f / 255, 153f / 255, 0f / 255);
                if (is_easy) temp.EdgeColor = new Color(255f / 255, 153f / 255, 0f / 255);
                break;
        }

        if (!is_easy)
        {
            switch (j)
            {
                case colors.RED:
                    temp.EdgeColor = Color.red;
                    break;
                case colors.BLUE:
                    temp.EdgeColor = Color.blue;
                    break;
                case colors.GREEN:
                    temp.EdgeColor = Color.green;
                    break;
                case colors.YELLOW:
                    temp.EdgeColor = Color.yellow;
                    break;
                case colors.PURPLE:
                    temp.EdgeColor = new Color(165f / 255, 0f, 255f / 255);
                    break;
                case colors.ORANGE:
                    temp.EdgeColor = new Color(255f / 255, 153f / 255, 0f / 255);
                    break;
            }
        }

        switch (k)
        {
            case colors.RED:
                temp.WordString = "빨강";
                break;
            case colors.BLUE:
                temp.WordString = "파랑";
                break;
            case colors.GREEN:
                temp.WordString = "초록";
                break;
            case colors.YELLOW:
                temp.WordString = "노랑";
                break;
            case colors.PURPLE:
                temp.WordString = "보라";
                break;
            case colors.ORANGE:
                temp.WordString = "주황";
                break;
        }

        return temp;
    }
}
