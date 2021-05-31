using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI tmp_record;

    

    public void Show(int score)
    {
        tmp_record.text = "게임 종료!\n기록 : " + score.ToString();
        this.transform.SetAsLastSibling();
        this.gameObject.SetActive(true);
    }
    public void ReLoadScene()
    {
        SceneManager.LoadScene(0);
    }
}
