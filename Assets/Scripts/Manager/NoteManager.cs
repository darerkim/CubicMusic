using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d;

    [SerializeField] Transform tfNoteAppear = null;
    TimingManager theTimingManager;
    EffectManager theEffectManager;

    private void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();
    }

    void Update()
    {
        if (GameManager.instance.isStartGame)
        {
            //노트 생성
            currentTime += Time.deltaTime;

            if (currentTime >= 60d / bpm)
            {
                GameObject t_Note = ObjectPool.instance.noteQueue.Dequeue();
                t_Note.transform.position = tfNoteAppear.position;
                t_Note.SetActive(true);
                theTimingManager.boxNoteList.Add(t_Note);
                currentTime -= 60d / bpm;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            if (collision.GetComponent<Note>().GetNoteFlag())
            {
                theEffectManager.JudgementEffect(4);
                theTimingManager.MissRecord();
            }

            if (!collision.GetComponent<Note>().noteImage.enabled)
                collision.GetComponent<Note>().noteImage.enabled = true;

            theTimingManager.boxNoteList.Remove(collision.gameObject);
            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);
        }
    }

    public void RemoveNote()
    {
        GameManager.instance.isStartGame = false;

        for (int i = 0; i < theTimingManager.boxNoteList.Count; i++)
        {
            theTimingManager.boxNoteList[i].SetActive(false);
            ObjectPool.instance.noteQueue.Enqueue(theTimingManager.boxNoteList[i]);
        }

        theTimingManager.boxNoteList.Clear();
    }
}
