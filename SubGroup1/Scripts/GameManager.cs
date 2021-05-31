using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    StageCreator sc;
    Queue<Quiz> stage = new Queue<Quiz>();

    public Quiz cur_quiz, next_quiz;

    //quiz 이미지의 크기
    float quiz_obj_scale;


    [SerializeField]
    TextMeshProUGUI tmp_level;
    int level ;
    public int Level
    {
        get { return level; }
        set { level = value; tmp_level.text = value.ToString(); }
    }

    [SerializeField]
    GameObject[] buttons = new GameObject[6];

    public static GameManager instance;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    private void Start()
    {
        GetTwoQuizesAndStart();
        Level = 0;
    }
    // 스테이지 (퀴즈 큐) 생성 -> 2개를 Dequeue해서 cur_quiz, next_quiz에 설정, 이후 정답을 맞힐 때마다 Dequeue와 quiz를 재설정.
    // 

    public void GetTwoQuizesAndStart()
    {
        stage = sc.CreateQueue(level);
        cur_quiz = stage.Dequeue();
        cur_quiz.transform.position = Vector3.zero;
        quiz_obj_scale = cur_quiz.transform.localScale.x;

        next_quiz = stage.Dequeue();
        next_quiz.transform.position = new Vector3(-0.5f, 0, 1);
        next_quiz.transform.SetAsFirstSibling();
        next_quiz.transform.localScale = new Vector2(quiz_obj_scale * 0.8f, quiz_obj_scale * 0.8f);
    }

    public void SubmitAnswer(Color clr_clr)
    {
        string clr_str;
        /*
        switch (clr)
        {
            case 0:
                //clr_str = "빨강";
                clr_clr = new Color(226f / 255, 42f / 255, 0f / 255);
                break;
            case 1:
                //clr_str = "파랑";
                clr_clr = new Color(0f / 255, 73f / 255, 227f / 255);
                break;
            case 2:
                //clr_str = "초록";
                clr_clr = new Color(87f / 255, 229f / 255, 0f / 255);
                break;
            case 3:
                //clr_str = "노랑";
                clr_clr = new Color(219f / 255, 212f / 255, 0f / 255);
                break;
            case 4:
                //clr_str = "보라";
                clr_clr = new Color(165f / 255, 0f, 255f / 255);
                break;
            case 5:
                //clr_str = "주황";
                clr_clr = new Color(255f / 255, 153f / 255, 0f / 255);
                break;
            default:
                //clr_str = " ";
                clr_clr = Color.clear;
                break;
        }
        */
        if (clr_clr == cur_quiz.TextColor)
        {
            Correct();
        }
        else
            Wrong();
    }

    //클릭한 버튼이 정답일 때
    public void Correct()
    {
        cur_quiz.transform.position = new Vector3(100, 0, 0); 

        cur_quiz = next_quiz;
        cur_quiz.transform.position = Vector3.zero;
        cur_quiz.transform.localScale = new Vector2(quiz_obj_scale, quiz_obj_scale);

        if(stage.Count == 0)
        {
            if (level < 2) Level++;
            stage = sc.CreateQueue(level);
        }
        next_quiz = stage.Dequeue();
        next_quiz.transform.position = new Vector3(-0.5f, 0,1);
        next_quiz.transform.SetAsFirstSibling();
        next_quiz.transform.localScale = new Vector2(quiz_obj_scale*0.8f, quiz_obj_scale * 0.8f);

        if(level >= 2)
        {
            Shuffle();
        }
    }
    
    //클릭한 버튼이 오답일 때
    public void Wrong()
    {

    }

    //버튼의 color_int를 뒤죽박죽으로 바꿈
    public void Shuffle()
    {
        int[] nums = { 0, 1, 2, 3, 4, 5 };
        int num1, num2;

        for(int i = 0; i < nums.Length; ++i)
        {
            num1 = Random.Range(0, nums.Length);
            num2 = Random.Range(0, nums.Length);

            int temp = nums[num1];
            nums[num1] = nums[num2];
            nums[num2] = temp;
        }

        for(int i = 0; i < nums.Length; i++)
        {
            buttons[i].GetComponent<Button>().Color_int = nums[i];
        }
    }
}
