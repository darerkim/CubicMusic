using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] GameObject goComboImage = null;
    [SerializeField] UnityEngine.UI.Text txt_Combo = null;

    int currentCombo = 0;
    int maxCombo = 0;

    Animator myAnim;
    string animComboUp = "ComboUp";

    void Start()
    {
        myAnim =  GetComponent<Animator>();
        goComboImage.SetActive(false);
        txt_Combo.gameObject.SetActive(false);
    }

    public void IncreaseCombo(int p_num = 1)
    {
        currentCombo += p_num;
        txt_Combo.text = string.Format("{0:#,##0}", currentCombo);

        //최대콤보 갱신
        if (maxCombo < currentCombo)
            maxCombo = currentCombo;

        if (currentCombo > 2)
        {
            myAnim.SetTrigger(animComboUp);
            goComboImage.SetActive(true);
            txt_Combo.gameObject.SetActive(true);
        }
    }

    public int GetCurrentCombo()
    {
        return currentCombo;
    }

    public int GetMaxCombo()
    {
        return maxCombo;
    }

    public void ResetCombo()
    {
        currentCombo = 0;
        txt_Combo.text = "0"; 
        goComboImage.SetActive(false );
        txt_Combo.gameObject.SetActive(false);
    }
}
