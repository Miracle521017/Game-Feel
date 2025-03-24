using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score;
    public int combo;

    public float comboMultiplier=1f;

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
    }

    public void AddScore(int amount)
    {
        score += Mathf.RoundToInt(amount * comboMultiplier);
        combo++;
        // 触发UI更新等操作
    }

    public void ComboBreak()
    {
        combo = 0;
        // 触发连击中断效果
    }
}
