using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float totalTime;
    private float timer = 0f;
    [HideInInspector]
    public int Score = 0;
    [HideInInspector]
    public bool GameEnd = false;
    string content;
    public TMP_Text displayText, displayText1,end; // 引用Text组件
    public GameObject into;
    public AudioSource audioSource;
    private float pauseDuration = 2f; // 定义暂停时长
    public GameObject endUI;

    void Start()
    {
        into.SetActive(true);
        StartCoroutine(StartGameAfterDelay(pauseDuration));
        Score = 0;
        endUI.SetActive(false);
    }

    void Update()
    {
        if (GameEnd)
        {
            endUI.SetActive(true); // 游戏结束后激活endUI
            end.text = "  " + Score;
            return;
        }

        if (timer <= totalTime) timer += Time.deltaTime;
        else GameEnd = true;

        displayText.text = "剩余时间: " + (int)(totalTime - timer);
        displayText1.text = " 得分: " + Score;
    }

    private IEnumerator StartGameAfterDelay(float delay)
    {

        float elapsedTime = 0f;
        float startVolume = audioSource.volume;
        float targetVolume = 0.3f;
        float fadeDuration = delay;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeDuration);
            yield return null;
        }

        into.SetActive(false);
    }

    //void OnGUI()
    //{
    //    if(!GameEnd) content = " 剩余时间: " + (int)(totalTime - timer);
    //    else         content = " 得分: " + Score;

    //    GUI.Box(new Rect(500,25,500,100), $"<color='black'><size=80>{content}</size></color>");
    //}
}