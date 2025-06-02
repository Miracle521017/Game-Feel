using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_1 : MonoBehaviour
{
    public Slider progressSlider;
    public int score;
    public int combo;
    public float gameTime;
    public float startTime;
    public float songLength;
    public AudioSource musicSource;

    public List<JudgementZone> judgementZones=new List<JudgementZone>(); // 8个判定区域


    void Awake()
    {
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

            // Debug.Log($"游戏时间：{gameTime:F2} 秒，Slider值：{sliderValue:F2}");
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
