using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementSystem : MonoBehaviour
{
    public static JudgementSystem Instance;

    [Header("Timing Thresholds")]
    public float perfectThreshold = 0.05f;
    public float goodThreshold = 0.1f;

    [Header("Scoring")]
    public int perfectScore = 100;
    public int goodScore = 80;
    public int missPenalty = 0;

    private Dictionary<int, Note> recentNotes = new Dictionary<int, Note>(); // 记录每个轨道上最近的音符

    void Awake() => Instance = this;

    public void RecordRecentNote(Note note, int trackIndex)
    {
        recentNotes[trackIndex] = note;
    }

    public void JudgeNote(int trackIndex, PlayerController player)
    {
        if (recentNotes.TryGetValue(trackIndex, out Note note))
        {
            float timeDiff = Mathf.Abs(Time.time - note.spawnTime);

            bool conditionMet = note.requiredGlove == player.currentGlove &&
                               note.requiredTool == player.currentTool;

            if (conditionMet)
            {
                if (timeDiff <= perfectThreshold)
                {
                    GameManager.Instance.AddScore(perfectScore);
                    ShowJudgementEffect("PERFECT!");
                }
                else if (timeDiff <= goodThreshold)
                {
                    GameManager.Instance.AddScore(goodScore);
                    ShowJudgementEffect("GOOD");
                }
            }
            else
            {
                GameManager.Instance.AddScore(missPenalty);
                ShowJudgementEffect("MISS");
                GameManager.Instance.BreakCombo();
            }

            note.isJudged = true;
            Destroy(note.gameObject);
            recentNotes.Remove(trackIndex);
        }
    }

    public void AutoJudgeMiss(Note note)
    {
        GameManager.Instance.AddScore(missPenalty);
        GameManager.Instance.BreakCombo();
        ShowJudgementEffect("MISS");
        note.isJudged = true;
        Destroy(note.gameObject);
    }

    public void MissNote(Note note)
    {
        AutoJudgeMiss(note);
    }

    void ShowJudgementEffect(string text)
    {
        // 实现视觉效果
    }
}
