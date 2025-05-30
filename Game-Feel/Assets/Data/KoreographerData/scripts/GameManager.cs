using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Slider progressSlider;
    public AudioSource musicSource;

    [Header("Game State")]
    public int score;
    public int combo;
    public float gameTime;
    public float startTime;
    public float songLength;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (musicSource.isPlaying)
        {
            gameTime = Time.time - startTime;
            float sliderValue = Mathf.Min(gameTime / songLength, 1f);

            if (progressSlider != null)
            {
                progressSlider.value = sliderValue;
            }

            Debug.Log($"游戏时间：{gameTime:F2} 秒，Slider值：{sliderValue:F2}");
        }
        else
        {
            if (progressSlider != null)
            {
                progressSlider.value = 1;
                Debug.Log("音乐播放完毕，Slider值设为1");
            }
        }
    }

    public void StartGame()
    {
        score = 0;
        combo = 0;
        startTime = Time.time;

        if (musicSource.clip != null)
        {
            songLength = musicSource.clip.length;
            Debug.Log($"歌曲长度：{songLength} 秒");
        }
        else
        {
            songLength = 0f;
            Debug.LogError("AudioClip未设置！");
        }

        if (progressSlider != null)
        {
            progressSlider.value = 0;
        }

        musicSource.Play();
        Debug.Log($"游戏开始！当前得分：{score}，Combo：{combo}");
    }

    public void AddScore(int points)
    {
        score += points + (int)(combo * 0.2f);
        combo = points > 0 ? combo + 1 : 0;

        if (points > 0)
        {
            Debug.Log($"得分：{points} + {0}（Combo 加成），当前总分：{score}，Combo：{combo}");
        }
        else
        {
            Debug.Log($"扣分：{points}，当前总分：{score}，Combo：{combo}");
        }
    }

    public void BreakCombo()
    {
        combo = 0;
        Debug.Log($"Combo 被打破！当前 Combo：{combo}");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
