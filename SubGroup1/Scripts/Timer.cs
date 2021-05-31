using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;


/*
 * 시간이 지남에 따라 줄어드는 타이머.
 * 시간이 줄어드는 속도는 현재 점수에 따라서 독립적으로 빨라진다.
 * 정답을 맞추면 남은 시간이 늘어나고, 틀리면 줄어든다.
 */

public class Timer : MonoBehaviour
{
    [SerializeField]
    float default_dec, default_dec_add, max_dec; //각각 초당 시간 감소량, 감소량 증가값의 초기값, 최대 감소량

    float decrement; //초당 시간 감소량

    [SerializeField]
    TextMeshProUGUI tmp_timer;  //시간 표시할 텍스트 
    [SerializeField]
    Image timer_gauge;          //cur_time에 따라 길이가 실시간으로 바뀌는 이미지 바(bar)


    [SerializeField]
    float start_max_time;

    [SerializeField]
    float default_wrong_dec, default_recover_amu; //각각 틀렸을 때 체력 감소량과 맞췃을 때 체력 증가량의 초기값


    float recover_amount; //문제 맞출 시 회복량

    float max_time;
    public float Max_time
    {
        get { return max_time; }
        set { max_time = value; }
    }

    float cur_time;
    public float Cur_time
    {
        get { return cur_time; }
        set
        {
            if (value <= 0)                          //시간 초과(게임 종료)
            {
                GameManager.instance.Die();
            }

            if (value > Max_time)
                cur_time = Max_time;                    //최대치보다 많은 값 할당 시 최대치로 고정
            else cur_time = value;

            timer_gauge.fillAmount = cur_time / Max_time;
            tmp_timer.text = "남은 시간: " + string.Format("{0:0.0}", cur_time);

        }
    }

    private void Start()
    {
        Max_time = start_max_time;
        Cur_time = Max_time;
        decrement = default_dec;
        recover_amount = default_recover_amu;
    }

    private void FixedUpdate()
    {
        Cur_time -= decrement * Time.fixedDeltaTime;
    }

    public void AddTime(float t)
    {
        Cur_time += t;
    }
    
    public void Correct()
    {
        //타이머의 시간을 일정 수치 회복한다.
        AddTime(recover_amount);
    }
    public void Wrong()
    {
        //타이머의 시간을 감소시킨다.
        AddTime(-default_wrong_dec);

        timer_gauge.color = Color.red;
        timer_gauge.DOColor(Color.white, 0.5f);
    }
    
    /// <summary>
    /// 시간 감소량을 늘린다. 
    /// </summary>
    public void Faster()
    {
        if (decrement < max_dec)
            decrement += default_dec_add;
    }
    public void StopTimer()
    {
        cur_time = 0;
        decrement = 0;
    }

}
