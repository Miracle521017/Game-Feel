using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementSystem : MonoBehaviour
{
    public static JudgementSystem Instance;

    public bool isConditionCorrect=false;

    [Header("Timing Thresholds")]
    public float perfectThreshold = 0.05f;
    public float goodThreshold = 0.1f;

    [Header("Scoring")]
    public int perfectScore = 100;
    public int goodScore = 80;
    public int missPenalty = 0;



    public void TriggerNote(Note note)
    {
        CheckCondition(note);

        if (isConditionCorrect)
        {
            note.isJudged = true;
            GameManager.Instance.AddScore(perfectScore);
            Destroy(note.gameObject);
            //´ý²¹³ä¼Ó·ÖÂß¼­
        }
        else
        {
            Miss(note);
        }
    }

    //MissÅÐ¶¨
    public void Miss(Note note)
    {
        GameManager.Instance.BreakCombo();
        note.isJudged = true;
        Destroy(note.gameObject);
    }

    public void CheckCondition(Note note)
    {
        if(note.track==0||note.track==1||note.track==5)
        {

        }
    }
}
