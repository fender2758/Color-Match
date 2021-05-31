using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    StageCreator sc;
    [SerializeField]
    ScoreCounter score_counter;
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
        set { level = value; tmp_level.text = "Level : "+ value.ToString(); }
    }

    [SerializeField]
    GameObject[] buttons = new GameObject[6];

    [SerializeField]
    Timer timer;

    [SerializeField]
    UnityEngine.UI.Image bg;

    [SerializeField]
    LvlupPanel lp;

    [SerializeField]
    GameOverPanel gameover_panel;

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

        gameover_panel.gameObject.SetActive(false);
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
        next_quiz.transform.position = new Vector3(-0.8f, 0, 1);
        next_quiz.transform.SetAsFirstSibling();
        next_quiz.transform.localScale = new Vector2(quiz_obj_scale * 0.8f, quiz_obj_scale * 0.8f);

        bg.transform.SetAsFirstSibling();
    }

    public void SubmitAnswer(Color clr_clr)
    {
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
        cur_quiz.transform.DOLocalMoveX(1000, 1f);
        StartCoroutine(WaitAndDestroyQuiz(cur_quiz));

        next_quiz.transform.DOMoveX(0, 1f);
        next_quiz.transform.DOScale(quiz_obj_scale, 0.5f);

        cur_quiz = next_quiz;

        if(stage.Count == 0)
        {
            print("남은 퀴즈가 엄서요");
            if (level < 2) lp.LevelUp(Level++);
            stage = sc.CreateQueue(level);
        }
        print("남은 퀴즈 개수 : " + stage.Count.ToString());
        next_quiz = stage.Dequeue();
        next_quiz.transform.position = new Vector3(-0.8f, 0,1);
        next_quiz.transform.SetAsFirstSibling();
        next_quiz.transform.localScale = new Vector2(quiz_obj_scale*0.8f, quiz_obj_scale * 0.8f);

        bg.transform.SetAsFirstSibling();

        //난이도가 3단계 이상이라면 버튼의 위치를 뒤섞어준다.
        if(level >= 2)
        {
            Shuffle();
        }

        timer.Correct();

        score_counter.AddScore();
    }
    
    //클릭한 버튼이 오답일 때
    public void Wrong()
    {
        if(level >= 2)
        {
            Shuffle();
        }

        timer.Wrong();
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

    //이미 지난 문제는 삭제
    IEnumerator WaitAndDestroyQuiz(Quiz q)
    {
        yield return new WaitForSeconds(1f);
        Destroy(q.gameObject);
    }
    /// <summary>
    /// 게임 종료
    /// </summary>
    public void Die()
    {
        gameover_panel.Show(score_counter.GetScore());
        timer.StopTimer();
        cur_quiz.TextColor = Color.clear;
    }
}
