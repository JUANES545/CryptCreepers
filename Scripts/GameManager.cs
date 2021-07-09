using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance; 
    public int time = 30;
    public int difficulty = 1;
    [SerializeField] private int score;
    public int Score
    {
        get => score;
        set
        {
            score = value;
            GUIManager.sharedInstance.updateUIScore(score);
            if (score % 1000 == 0)
            {
                difficulty++;
            }
        }
    }

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        GUIManager.sharedInstance.updateUIScore(score);
        GUIManager.sharedInstance.updateUITime(time);
        StartCoroutine(CountDownRoutine());
    }
    
    void Update()
    {
        
    }

    IEnumerator CountDownRoutine()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            GUIManager.sharedInstance.updateUITime(time);
        }

        GUIManager.sharedInstance.GameOver();
    }


}
