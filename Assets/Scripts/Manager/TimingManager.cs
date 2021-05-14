using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>(); //리스트에 있는 노트들이 판정 범위에 있는지 비교하기 위해 필요

    [SerializeField] Transform center = null; //판정범위의 중심을 알려줄 센터변수
    [SerializeField] RectTransform[] timingRect; //다양한 판정 범위 perfect,cool,good,bad
    Vector2[] timingBoxs = null; //판정 범위의 최소값X 최대값Y

    [SerializeField] EffectManager theEffect;

    ScoreManager theScoreManager;
    ComboManager theComboManager;
    StageManager theStageManager;
    PlayerController thePlayer;
    StatusManager theStatus;
    AudioManager theAudioManager;

    //판정 기록 변수
    int[] judgementRecord = new int[5];

    // Start is called before the first frame update
    void Start()
    {
        theAudioManager = AudioManager.instance;
        theStatus = FindObjectOfType<StatusManager>();
        thePlayer = FindObjectOfType<PlayerController>();
        theStageManager = FindObjectOfType<StageManager>();
        theComboManager = FindObjectOfType<ComboManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();
        theEffect = FindObjectOfType<EffectManager>();
        timingBoxs = new Vector2[timingRect.Length];

        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(center.localPosition.x - timingRect[i].rect.width / 2,
                              center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    //타이밍을 체크하는 함수
    public bool CheckTiming()
    {
        //생성된 노트의 개수만큼 반복
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_NotePosX = boxNoteList[i].transform.localPosition.x;

            for (int j = 0; j < timingBoxs.Length; j++)
            {
                if (timingBoxs[j].x <= t_NotePosX && t_NotePosX <= timingBoxs[j].y)
                {
                    //노트 제거
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    boxNoteList.RemoveAt(i);

                    //이펙트 연출 (판정 연출)
                    if (j < timingRect.Length)
                        theEffect.NoteHitEffect();

                    //발판 활성화 여부 판별
                    if (CheckCanNextPlate())
                    {
                        theStageManager.ShowNextPlates();//판때기 등장
                        theEffect.JudgementEffect(j);    //판정 연출
                        judgementRecord[j]++;            //판정 기록
                        theScoreManager.IncreaseScore(j);//점수 증가
                        theStatus.CheckShield();         //콤보게이지 체크
                    }
                    else
                    {
                        //판정 이펙트 (normal)
                        theEffect.JudgementEffect(5);
                        theComboManager.ResetCombo();
                        theStatus.ResetShieldCombo();
                    }

                    theAudioManager.PlaySFX("Clap");

                    return true;
                }
            }
        }

        theEffect.JudgementEffect(timingBoxs.Length);
        MissRecord();  //판정 기록 
        return false;
    }

    bool CheckCanNextPlate()
    {
        if (Physics.Raycast(thePlayer.destPos, Vector3.down, out RaycastHit t_hitInfo, 1.1f))
        {
            if (t_hitInfo.transform.CompareTag("BasicPlate"))
            {
                BasicPlate t_plate = t_hitInfo.transform.GetComponent<BasicPlate>();
                if (t_plate.flag)
                {
                    t_plate.flag = false;
                    return true;
                }
            }
        }
        return false;
    }

    public int GetJudgementRecord(int num)
    {
        return judgementRecord[num];
    }

    public void MissRecord()
    {
        judgementRecord[4]++;  //판정 기록
        theStatus.ResetShieldCombo();
        theComboManager.ResetCombo();
    }

    public void Initialized()
    {
        judgementRecord[0] = 0;
        judgementRecord[1] = 0;
        judgementRecord[2] = 0;
        judgementRecord[3] = 0;
        judgementRecord[4] = 0;
    }
}
