using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public  float totalTime = 2f;
    private float timer = 0f;
    [HideInInspector]
    public int Score = 0;
    [HideInInspector]
    public bool GameEnd = false;
    string content;

    void Start()
    {
        Score = 0;
    }

    void Update()
    {
        if(timer <= totalTime) timer += Time.deltaTime;
        else  GameEnd = true;
    }

    void OnGUI()
    {
        if(!GameEnd) content = " 剩余时间: " + (int)(totalTime - timer);
        else         content = " 得分: " + Score;

        GUI.Box(new Rect(500,25,500,100), $"<color='black'><size=80>{content}</size></color>");
    }
}
