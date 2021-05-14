using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusManager : MonoBehaviour
{
    [SerializeField] float blinkSpeed = 0.1f;
    [SerializeField] int blinkCount = 20;
    [SerializeField] MeshRenderer playerMesh = null;
    bool isBlink = false;

    int maxHp = 3;
    int currentHp = 3;

    int maxShield = 3;
    int currentShield = 0;
    bool isDead = false;

    [SerializeField] GameObject[] go_hp;
    [SerializeField] GameObject[] go_shield;

    Result theResult;
    NoteManager theNote;

    [SerializeField] int increaseShieldCombo = 5;
    int currentShieldCombo = 0; //콤보가 increaseShieldCombo(5개) 쌓이면 1쉴드 추
    [SerializeField] Image shieldGauge = null;

    private void Start()
    {
        theNote = FindObjectOfType<NoteManager>();
        theResult = FindObjectOfType<Result>();
    }

    IEnumerator BlinkCoroutine()
    {
        isBlink = true;
        for (int i = 0; i < blinkCount; i++)
        {
            playerMesh.enabled = !playerMesh.enabled;
            yield return new WaitForSeconds(blinkSpeed);
        }
        playerMesh.enabled = true;
        isBlink = false;
    }

    public void ResetShieldCombo()
    {
        currentShieldCombo = 0;
        shieldGauge.fillAmount = (float)currentShieldCombo / increaseShieldCombo;
    }

    public void CheckShield()
    {
        currentShieldCombo++;

        if (currentShieldCombo >= increaseShieldCombo)
        {
            IncreaseShield();
            currentShieldCombo = 0;
        }

        shieldGauge.fillAmount = (float)currentShieldCombo / increaseShieldCombo;
    }

    public void IncreaseShield()
    {
        currentShield++;

        if (currentShield >= maxShield)
            currentShield = maxShield;

        SettingShield();
    }

    public void DecreaseShield(int p_num)
    {
        currentShield -= p_num;

        if (currentShield <= 0)
            currentShield = 0;

        SettingShield();
    }

    public void IncreaseHp(int p_num)
    {
        currentHp += p_num;
        if (currentHp >= maxHp)
            currentHp = maxHp;

        SettingHp();
    }

    public void DecreaseHp(int p_num)
    {
        if (!isBlink)
        {
            if (currentShield > 0)
                DecreaseShield(p_num);
            else
            {
                currentHp -= p_num;

                if (currentHp <= 0)
                {
                    isDead = true;
                    theResult.ShowResult();
                    GameManager.instance.isStartGame = true;
                    theNote.RemoveNote();
                }
                else
                    StartCoroutine(BlinkCoroutine());

                SettingHp();
            }
        }
    }

    void SettingHp()
    {
        for (int i = 0; i < go_hp.Length; i++)
        {
            if (i < currentHp)
                go_hp[i].SetActive(true);
            else
                go_hp[i].SetActive(false);
        }
    }

    void SettingShield()
    {
        for (int i = 0; i < go_shield.Length; i++)
        {
            if (i < currentShield)
                go_shield[i].SetActive(true);
            else
                go_shield[i].SetActive(false);
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void Initialized()
    {
        currentHp = maxHp;
        currentShield = 0;
        currentShieldCombo = 0;
        isDead = false;
        shieldGauge.fillAmount = 0;
        SettingHp();
        SettingShield();
    }
}
