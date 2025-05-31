using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementSystem_1 : MonoBehaviour
{
    public static JudgementSystem_1 Instance;

    [Header("Timing Thresholds")]
    public float perfectThreshold = 0.05f; // PERFECT判定的时间阈值
    public float goodThreshold = 0.1f;// GOOD判定的时间阈值

    [Header("Scoring")]
    public int perfectScore = 5;
    public int goodScore = 2;
    public int missPenalty = 0;

    private Note recentNote; // 记录最近进入检测区域的音符

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // 初始化单例实例
        }
        else
        {
            Destroy(gameObject); // 防止重复实例化
        }
    }

    public void RecordRecentNote(Note note)
    {
        recentNote = note;
    }

    // 判定音符，根据时间差给出评分
    public void JudgeNote(PlayerController player,int track)
    {

        if (recentNote != null && recentNote.track == track)
        {
            float timeDiff = Mathf.Abs(Time.time - recentNote.spawnTime);

            if (timeDiff <= perfectThreshold)
            {
                GameManager_1.Instance.AddScore(perfectScore); // 增加PERFECT分数
                ShowJudgementEffect("PERFECT!", player); // 显示PERFECT效果
            }
            else if (timeDiff <= goodThreshold)
            {
                GameManager_1.Instance.AddScore(goodScore); // 增加GOOD分数
                ShowJudgementEffect("GOOD", player); // 显示GOOD效果
            }
            else
            {
                GameManager_1.Instance.AddScore(missPenalty); // 增加MISS惩罚
                ShowJudgementEffect("MISS", player); // 显示MISS效果
                GameManager_1.Instance.BreakCombo(); // 打断连击
            }

            recentNote.isJudged = true; // 标记音符为已判定
            Destroy(recentNote.gameObject); // 销毁音符对象
            recentNote = null; // 重置最近音符
        }
    }

    public void AutoJudgeMiss(Note note)
    {
        //GameManager_1.Instance.AddScore(missPenalty); // 增加MISS惩罚
        //GameManager_1.Instance.BreakCombo(); // 打断连击
        ShowJudgementEffect("MISS", null); // 显示MISS效果
        note.isJudged = true; // 标记音符为已判定
        Destroy(note.gameObject); // 销毁音符对象
        Debug.Log("Miss");
    }

    public void MissNote(Note note)
    {
        AutoJudgeMiss(note);
    }

    void ShowJudgementEffect(string text, PlayerController player)
    {
        // 实现视觉效果
    }

     // 处理玩家输入，进行音符判定
    public void ProcessInput(PlayerController player, int trackIndex)
    {
        Note note = JudgementZone.Instance.GetNote(trackIndex); // 获取当前轨道上的音符
        if (note != null)
        {
            JudgeNote(player, trackIndex); // 判定音符
        }
    }
}
