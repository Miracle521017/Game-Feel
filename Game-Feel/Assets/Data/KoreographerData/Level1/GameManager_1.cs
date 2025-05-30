using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_1 : MonoBehaviour
{
    public static GameManager_1 Instance;

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
            DontDestroyOnLoad(gameObject);
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
        gameTime = Time.time - startTime;
    }

    public void StartGame()
    {
        score = 0;
        combo = 0;
        startTime = Time.time;

        if (songLength > 0)
        {
            Debug.Log($"歌曲长度：{songLength} 秒");
        }
        else
        {
            Debug.LogError("歌曲长度未设置！");
        }

        Debug.Log("游戏开始！");
    }

    public void AddScore(int points)
    {
        score += points + (int)(combo * 0.2f);
        combo = points > 0 ? combo + 1 : 0;

        if (points > 0)
        {
            Debug.Log($"得分：{points} + {combo * 0.2f}（Combo 加成），当前总分：{score}，Combo：{combo}");
        }
        else
        {
            Debug.Log($"扣分：{points}，当前总分：{score}，Combo：{combo}");
        }
    }

    public void BreakCombo()
    {
        combo = 0;
        Debug.Log("Combo 被打破！");
    }
}
