using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject goUI = null;

    [SerializeField] Text[] txtCount = null;
    [SerializeField] Text txtCoin = null;
    [SerializeField] Text txtScore = null;
    [SerializeField] Text txtMaxCombo = null;

    ComboManager theCombo;
    ScoreManager theScore;
    TimingManager theTiming;

    // Start is called before the first frame update
    void Start()
    {
        theCombo = FindObjectOfType<ComboManager>();
        theScore = FindObjectOfType<ScoreManager>();
        theTiming = FindObjectOfType<TimingManager>();
    }

    public void ShowResult()
    {
        AudioManager.instance.StopBGM();

        goUI.SetActive(true);
        for (int i = 0; i < txtCount.Length; i++)
            txtCount[i].text = string.Format("{0:#,##0}", theTiming.GetJudgementRecord(i));

        int t_score = theScore.GetCurrentScore();
        int t_coin = t_score / 50;
        int t_combo = theCombo.GetMaxCombo();

        txtMaxCombo.text = string.Format("{0:#,##0}", t_combo);
        txtCoin.text = string.Format("{0:#,##0}", t_coin);
        txtScore.text = string.Format("{0:#,##0}", t_score);
    }

    public void BtnMainMenu()
    {
        goUI.SetActive(false);
        theCombo.ResetCombo();
        GameManager.instance.MainMenu();
    }
}
