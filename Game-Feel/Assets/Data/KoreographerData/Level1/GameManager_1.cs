using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_1 : MonoBehaviour
{
    public static GameManager_1 Instance;

    public Slider progressSlider;//游戏进度


    public int score;//当局游戏总分
    public int basicscore =100;//没有combo的基础的分
    public int maxCombo=50;//最大连击数，超过后不再增加倍率

    public float[] comboMultipliers = { 1.0f, 1.2f, 1.5f,2.0f};//不同连击区间的得分倍率
    public int[] comboThresholds = { 10, 20, 30 };//不同倍率的连击边界
    public int currentCombo = 0;//当前连击数目

    public float gameTime;//游戏时间
    public float startTime;//游戏开始时间
    public float songLength;//歌曲长度
    public AudioSource musicSource;

    public List<JudgementZone> judgementZones=new List<JudgementZone>(); // 8个判定区域


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
            }
        }
    }
    
    public void StartGame()
    {
        score = 0;
        currentCombo = 0;

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

    //得分
    public void AddScore()
    {

        currentCombo++;//combo增加

        float currentMultiplier = 1.0f;

        if (currentCombo >= comboThresholds[comboThresholds.Length - 1])
        {
            currentMultiplier = comboMultipliers[comboMultipliers.Length - 1];
        }
        else
        {
            for(int i = 0; i < comboThresholds.Length; i++)
            {
                if(currentCombo>= comboThresholds[i])
                {
                    currentMultiplier = comboMultipliers[i];
                }
            }
        }


    }

    //不得分
    public void BreakCombo()
    {
        combo = 0;
        Debug.Log("Combo 被打破！");
    }
}
