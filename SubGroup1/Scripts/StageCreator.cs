using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCreator : MonoBehaviour
{
    [SerializeField]
    int quizes_per_stage;

    [SerializeField]
    QuizCreator qc;


    private void Start()
    {
    }

    //Creates and returns Quiz queue. argument is stage level
    //레벨은 0, 1, 2 세 단계가 있으며, 남은 시간이 줄어드는 속도는 독립적으로 빨라진다.
    public Queue<Quiz> CreateQueue(int level)
    {
        Queue<Quiz> queue = new Queue<Quiz>();

        for(int i = 0; i < quizes_per_stage; i++)
        {
            if (level <= 0)
                queue.Enqueue(qc.Create(true));
            else
                queue.Enqueue(qc.Create(false));
        }

        return queue;
    }
}
