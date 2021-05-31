using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField]
    Timer timer;

    [SerializeField]
    TextMeshProUGUI tmp_score;

    int score = 0;
    int Score
    {
        get { return score; }
        set
        {
            score = value;
            if (score % 5 == 0)     //퀴즈를 5개 맞힐 때마다 시간 감소량 증가
               timer.Faster();
        }
    }


    public void AddScore()
    {
        Score++;
        tmp_score.text =  score.ToString();
    }
    public int GetScore()
    {
        return Score;
    }
}
