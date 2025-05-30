using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementSystem_1 : MonoBehaviour
{
    public static JudgementSystem Instance;

    [Header("Timing Thresholds")]
    public float perfectThreshold = 0.05f;
    public float goodThreshold = 0.1f;

    [Header("Scoring")]
    public int perfectScore = 5;
    public int goodScore = 2;
    public int missPenalty = 0;

    private Note recentNote; // 记录最近进入检测区域的音符

    //void Awake() => Instance = this;

    public void RecordRecentNote(Note note)
    {
        recentNote = note;
    }

    public void JudgeNote(PlayerController player)
    {
        if (recentNote != null)
        {
            float timeDiff = Mathf.Abs(Time.time - recentNote.spawnTime);

            if (timeDiff <= perfectThreshold)
            {
                GameManager_1.Instance.AddScore(perfectScore);
                ShowJudgementEffect("PERFECT!", player);
            }
            else if (timeDiff <= goodThreshold)
            {
                GameManager_1.Instance.AddScore(goodScore);
                ShowJudgementEffect("GOOD", player);
            }
            else
            {
                GameManager_1.Instance.AddScore(missPenalty);
                ShowJudgementEffect("MISS", player);
                GameManager_1.Instance.BreakCombo();
            }

            recentNote.isJudged = true;
            Destroy(recentNote.gameObject);
            recentNote = null;
        }
    }

    public void AutoJudgeMiss(Note note)
    {
        GameManager_1.Instance.AddScore(missPenalty);
        GameManager_1.Instance.BreakCombo();
        ShowJudgementEffect("MISS", null);
        note.isJudged = true;
        Destroy(note.gameObject);
    }

    public void MissNote(Note note)
    {
        AutoJudgeMiss(note);
    }

    void ShowJudgementEffect(string text, PlayerController player)
    {
        // 实现视觉效果
    }
}
