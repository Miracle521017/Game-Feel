using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementZone : MonoBehaviour
{
    public int judgementZoneIndex = 0;//该轨道的索引值
    public KeyCode targetKey = KeyCode.A;//该轨道对应的按键
    public bool isConditionCorrect = true;

    [Header("音符轨道列表")]
    public List<Note> notesToJudge = new List<Note>(); // 每个判定区域中现存的待检测音符列表

    private void Start()
    {
        switch (judgementZoneIndex)
        {
            case 0:
            case 4:
                targetKey= KeyCode.A;
                break;
            case 1:
            case 5:
                targetKey= KeyCode.D;
                break;
            case 2:
            case 6:
                targetKey= KeyCode.LeftArrow;
                break;
            case 3:
            case 7:
                targetKey= KeyCode.RightArrow;
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        //if (notesToJudge.Count>0)
        //{
        //    CheckInput();
        //}
    }

    //记录音符入轨道列表
    public void RecordNote(Note note)
    {
        notesToJudge.Add(note);
      // Debug.Log("音符记录到轨道 " + judgementZoneIndex + " 的判定列表中");
    }

    //移出音符的方法
    public bool RemoveNote(Note note)
    {
        if ( notesToJudge.Contains(note))
        {
            notesToJudge.Remove(note);
           // Debug.Log($"Note ' removed from track {judgementZoneIndex}.");
            return true;
        }
        else
        {
            Debug.LogError("移除失败！");
        }
        Debug.LogWarning("尝试从轨道 " + judgementZoneIndex + " 移除不存在的音符");
        return false;
    }

    //public void CheckInput()
    //{
    //    Debug.Log("进行轨道" + judgementZoneIndex + "的输入测试");
    //    if(Input.GetKeyDown(targetKey))
    //    {
    //        Debug.Log("按下对应按键！");
    //        //TODO：进行音效和粒子效果补充
    //        //触发音符
    //        TriggerNote(notesToJudge[0]);
    //    }
    //}


    public void TriggerNote(Note note)
    {
        //CheckCondition(note);

        if (isConditionCorrect)
        {
            Debug.Log("判定成功！");
            note.isJudged = true;
           // GameManager.Instance.AddScore();
            Destroy(note.gameObject);
            //待补充加分逻辑
        }
        else
        {
            Miss(note);
        }
    }

    //Miss判定
    public void Miss(Note note)
    {
        GameManager.Instance.BreakCombo();
        note.isJudged = true;
        Destroy(note.gameObject);
    }

    public void CheckCondition(Note note)
    {
        //int player = 0;//用0和1标识玩家一和玩家二
        //if (note.track == 0 || note.track == 1 || note.track == 4 || note.track == 5)
        //{

        //}
        //else
        //{
        //    player = 1;
        //}
    }
}
