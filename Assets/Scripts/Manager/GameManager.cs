using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] go_GameUI = null;
    [SerializeField] GameObject go_TitleUI = null; 

    public static GameManager instance;

    public bool isStartGame = false;
    public bool isMusicStart = false;

    ComboManager theCombo;
    ScoreManager theScore;
    TimingManager theTiming;
    StatusManager theStatus;
    StageManager theStage;
    PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {
        theStage = FindObjectOfType<StageManager>();
        theCombo = FindObjectOfType<ComboManager>();
        theScore = FindObjectOfType<ScoreManager>();
        theTiming = FindObjectOfType<TimingManager>();
        theStatus = FindObjectOfType<StatusManager>();
        thePlayer = FindObjectOfType<PlayerController>();
        instance = this;
    }

    public void GameStart()
    {
        for (int i = 0; i < go_GameUI.Length; i++)
        {
            go_GameUI[i].SetActive(true);  
        }

        isStartGame = true;

        theCombo.ResetCombo();
        theScore.Initialized();
        theTiming.Initialized();
        theStatus.Initialized();
        thePlayer.Initialized();
        theStage.RemoveStage();
        theStage.SettingStage();
    }

    public void MainMenu()
    {
        for (int i = 0; i < go_GameUI.Length; i++)
        {
            go_GameUI[i].SetActive(false);
        }

        isStartGame = false;

        go_TitleUI.SetActive(true);
    }
}
