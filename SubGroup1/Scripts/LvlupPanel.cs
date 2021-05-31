using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class LvlupPanel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI tmp_lvlup;

    Image p_img;

    private void Start()
    {
        p_img = GetComponent<Image>();
        p_img.color = Color.clear;
    }

    public void LevelUp(int l)
    {
        if (l == 0)//레벨 0->1 시
        {
            tmp_lvlup.text = "레벨 업!\n이제 테두리와 글자의 색이 서로 달라집니다.";
        }
        else {  //레벨 1->2 시
            tmp_lvlup.text = "레벨 업!\n이제 버튼이 눌릴 때마다 위치가 뒤섞입니다.";
        }
        p_img.color = Color.black;
        p_img.DOFade(0, 2f);
        tmp_lvlup.color = Color.white;
        tmp_lvlup.DOFade(0, 2f);
    }
}
