using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementZone : MonoBehaviour
{
    public int judgementZoneIndex = 0;//该轨道的索引值

    [Header("音符轨道列表")]
    public List<Note> notesToJudge = new List<Note>(); // 每个判定区域中现存的待检测音符列表


    //记录音符入轨道列表
    public void RecordNote(Note note)
    {
        notesToJudge.Add(note);
        Debug.Log("音符记录到轨道 " + judgementZoneIndex + " 的判定列表中");
    }

    //获取列表第一个音符的方法
    public Note GetNote(int track)
    {
        if (notesToJudge.Count > 0)
        {
            //对第一个音符进行判定
        }
        return null;
    }

    //移出音符的方法
    public bool RemoveNote(Note note)
    {
        if ( notesToJudge.Contains(note))
        {
            notesToJudge.Remove(note);
            Debug.Log($"Note ' removed from track {judgementZoneIndex}.");
            return true;
        }
        else
        {
            Debug.LogError("移除失败！");
        }
        Debug.LogWarning("尝试从轨道 " + judgementZoneIndex + " 移除不存在的音符");
        return false;
    }
}
