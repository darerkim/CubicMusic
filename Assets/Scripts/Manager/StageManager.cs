﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject stage = null;
    GameObject currentStage = null;
    Transform[] stagePlates;

    [SerializeField] float offsetY = 3;
    [SerializeField] float plateSpeed = 10;

    int stepCount = 0;
    int totalPlatesCount = 0;

    public void RemoveStage()
    {
        if (currentStage != null)
            Destroy(currentStage);
    }
    
    public void SettingStage()
    {
        stepCount = 0;
        currentStage = Instantiate(stage, Vector3.zero, Quaternion.identity);
        stagePlates = currentStage.GetComponent<Stage>().plates;
        totalPlatesCount = stagePlates.Length;

        for (int i = 0; i < totalPlatesCount; i++)
        {
            stagePlates[i].position = new Vector3(stagePlates[i].position.x,
                                                  stagePlates[i].position.y + offsetY,
                                                  stagePlates[i].position.z);
        }
    }

    public void ShowNextPlates()
    {
        if (stepCount < totalPlatesCount)
            StartCoroutine(MovePlateCo(stepCount++));
            //stagePlates[stepCount++].gameObject.SetActive(true);
    }

    public IEnumerator MovePlateCo(int p_num)
    {
        stagePlates[p_num].gameObject.SetActive(true);
        Vector3 t_destPos = new Vector3(stagePlates[p_num].position.x,
                                        stagePlates[p_num].position.y - offsetY,
                                        stagePlates[p_num].position.z);

        while (Vector3.SqrMagnitude(stagePlates[p_num].position - t_destPos) >= 0.001f)
        {
            stagePlates[p_num].position = Vector3.Lerp(stagePlates[p_num].position,
                                                       t_destPos,
                                                       plateSpeed * Time.deltaTime);
            yield return null;
        }

        stagePlates[p_num].position = t_destPos;
    }
}
