using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterFrame : MonoBehaviour
{
    [SerializeField] private string BGM;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.instance.isMusicStart && GameManager.instance.isStartGame)
        {
            if (collision.CompareTag("Note"))
            {
                AudioManager.instance.PlayBGM("BGM1");
                GameManager.instance.isMusicStart = true;
            }
        }
    }
}
