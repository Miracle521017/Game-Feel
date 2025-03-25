using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] public int score = 0;
    [SerializeField] private float scoreMultiplier = 1f;
    [SerializeField] private int consecutiveNotes = 0;
    private const int MaxConsecutive = 10;
    private const float MaxMultiplier = 2f;
    
    [SerializeField] private int totalNotes; // 总音符数（示例值）
    [SerializeField] private int triggeredNotes = 0; // 成功触发的音符数

    // 新增：添加TextMeshPro组件引用
    [SerializeField] private TextMeshProUGUI scoreText;

   void Start()
    {
        
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }

        // 初始化分数显示
        UpdateScoreDisplay();
    }

    public void AddScore(int baseScore = 10)
    {
        score += Mathf.RoundToInt(baseScore * scoreMultiplier);
        consecutiveNotes++;
        triggeredNotes++; // 增加成功触发的音符数

        // 更新分数显示
        UpdateScoreDisplay();

        // 检查是否达到连续触发条件
        if (consecutiveNotes >= MaxConsecutive)
        {
            IncreaseMultiplier();
            consecutiveNotes = 0; // 重置计数器
        }
    }

    private void IncreaseMultiplier()
    {
        if (scoreMultiplier < MaxMultiplier)
        {
            scoreMultiplier += 0.1f;
            scoreMultiplier = Mathf.Clamp(scoreMultiplier, 0, MaxMultiplier);
            Debug.Log("倍率提升至: " + scoreMultiplier);
        }
    }

    public void ResetMultiplier()
    {
        scoreMultiplier = 1f;
        consecutiveNotes = 0;
        Debug.Log("连击中断，倍率重置为1");
    }

    public void ResetGame()
    {
        score = 0;
        scoreMultiplier = 1f;
        consecutiveNotes = 0;
        triggeredNotes = 0;
        UpdateScoreDisplay(); // 重置分数显示
    }

    // 新增：计算星级
    public float CalculateStars()
    {
        
        float triggerRate = (float)triggeredNotes / totalNotes;
        return triggerRate;
        if (triggerRate >= 1f)
        {
            Debug.Log("三星！");
        }
        else if (triggerRate >= 0.8f)
        {
            Debug.Log("二星！");
        }
        else if (triggerRate >= 0.6f)
        {
            Debug.Log("一星！");
        }
        else
        {
            Debug.Log("失败");
        }

    }

    // 新增：更新分数显示的方法
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }

    public int CurrentScore => score;
    public float CurrentMultiplier => scoreMultiplier;
}
