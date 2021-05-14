using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMenu : MonoBehaviour
{
    [SerializeField] GameObject TitleMenu = null;

    public void BtnBack()
    {
        this.gameObject.SetActive(false);
        TitleMenu.SetActive(true);
    }

    public void BtnPlay()
    {
        this.gameObject.SetActive(false);
        GameManager.instance.GameStart();
    }
}
