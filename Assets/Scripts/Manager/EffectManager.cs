﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] Animator noteHitAnimator = null;
    [SerializeField] Animator judgementAnimator = null;
    [SerializeField] UnityEngine.UI.Image judgementImage = null; 
    [SerializeField] Sprite[] judgementSprite = null;
    
    string HIT = "Hit";

    public void NoteHitEffect()
    {
        noteHitAnimator.SetTrigger(HIT);
    }

    public void JudgementEffect(int p_num)
    {
        judgementImage.sprite = judgementSprite[p_num];
        judgementAnimator.SetTrigger(HIT);
    }
}
