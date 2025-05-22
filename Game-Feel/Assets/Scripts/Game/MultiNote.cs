using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MultiNote : MonoBehaviour//多击音符类
{
    public List<int> trackIDs;       // 需要触发的轨道ID列表
    public float timer;              // 音符出现时间
    public List<Note> linkedNotes = new List<Note>(); // 关联的音符对象

    // 初始化多轨道音符
    //public void Initialize(List<int> trackIds, float time)
    //{
    //    trackIDs = trackIds;
    //    timer = time;

    //    // 为每个轨道创建音符并关联
    //    foreach (int trackId in trackIDs)
    //    {
    //        GameObject noteObj = NoteCreator.Instance.CreateSingleNote(trackId, time);
    //        Note note = noteObj.GetComponent<Note>();
    //        note.SetMutiNote(this);
    //        linkedNotes.Add(note);
    //    }
    //}

    // 检查所有关联音符是否都被触发
    public bool CheckAllTriggered()                                 
    {
        foreach (Note note in linkedNotes)
        {
            if (!note.triggered) return false;
        }
        return true;
    }

    // 触发多轨道音符
    public void Trigger()
    {
        GameManager.Instance?.AddScore(trackIDs.Count); // 根据触发轨道数加分
        foreach (Note note in linkedNotes)
        {
            note.TriggerNote();
        }
        Destroy(gameObject);
    }
}
