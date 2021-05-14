using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlate : MonoBehaviour
{
    NoteManager theNote;
    Result theResult;

    // Start is called before the first frame update
    void Start()
    {
        theResult = FindObjectOfType<Result>();
        theNote = FindObjectOfType<NoteManager>(); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.isMusicStart = false;
            AudioManager.instance.PlaySFX("Fanfare");
            theNote.RemoveNote();
            theResult.ShowResult();
        }
    }
}
