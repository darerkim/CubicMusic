using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] Sound[] sfx = null;
    [SerializeField] Sound[] bgm = null;

    [SerializeField] AudioSource bgmPlayer = null;
    [SerializeField] AudioSource[] sfxPlayer = null;

    public void PlayBGM(string p_bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (bgm[i].name == p_bgmName)
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();
            }
        }
    }

    private void Start()
    {
        instance = this;
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySFX(string p_sfxName)
    {
        for (int i = 0; i < sfxPlayer.Length; i++)
        {
            if (!sfxPlayer[i].isPlaying)
            {
                for (int j = 0; j < sfx.Length; j++)
                {
                    if (sfx[j].name == p_sfxName)
                    {
                        sfxPlayer[i].clip = sfx[j].clip;
                        sfxPlayer[i].Play();
                        return;
                    }
                }
                Debug.Log(p_sfxName + " 효과음이 없습니.");
                return;
            }
        }
        Debug.Log("모든 오디오 플레이어가 재생중입니다.");
        return;
    }

    public void StopSFX()
    {
        for (int i = 0; i < sfxPlayer.Length; i++)
            sfxPlayer[i].Stop();
    }
}
