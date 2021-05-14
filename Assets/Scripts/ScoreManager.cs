using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text txtScore = null;
    [SerializeField] int increaseScore = 10;  //노트를 맞출 시 얼마씩 증가할지 기준점수
    int currentScore = 0; //현재점수
    [SerializeField] float[] weight = null; //판정에 따라 가중치가 각각 다르게 적용 되도록 배열로 선언

    [SerializeField] int comboBonusScore = 10;

    Animator myAnim;
    string animScoreUp = "ScoreUp";

    ComboManager theComboManager;

    // Start is called before the first frame update
    void Start()
    {
        theComboManager = FindObjectOfType<ComboManager>();
        myAnim = GetComponent<Animator>();
        txtScore.text = "0";
        currentScore = 0;
    }

    public void Initialized()
    {
        txtScore.text = "0";
        currentScore = 0;
    }

    public void IncreaseScore(int p_JudgementState)
    {
        //콤보 증가
        theComboManager.IncreaseCombo();

        //콤보 가중치 계산
        int t_currentCombo = theComboManager.GetCurrentCombo();
        int t_bonusComboScore = (t_currentCombo / 10) * comboBonusScore;

        //판정 가중치 계산
        int t_increaseScore = increaseScore + comboBonusScore;
        t_increaseScore = (int)(t_increaseScore * weight[p_JudgementState]);

        //점수 반영
        currentScore += t_increaseScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore);

        //애니 실행
        myAnim.SetTrigger(animScoreUp);
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}
