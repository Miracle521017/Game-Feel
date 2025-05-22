using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class Note : MonoBehaviour
{
    [SerializeField] private int trackID;
    [SerializeField] private float timer;
    public NoteCreator noteCreator;
    public MultiNote linkedMutiNote; // 新增关联的多轨道音符
    public bool triggered;          // 触发状态标记

    // 新增设置多轨道关联的方法
    public void SetMutiNote(MultiNote multiNote)
    {
        linkedMutiNote = multiNote;
    }

    // 修改触发方法
    public void TriggerNote()
    {
        triggered = true;

        // 如果是多轨道音符的一部分
        if (linkedMutiNote != null)
        {
            if (linkedMutiNote.CheckAllTriggered())
            {
                linkedMutiNote.Trigger();
            }
        }
        else // 普通音符逻辑
        {
            GameManager.Instance?.AddScore();
            noteCreator?.DeletePoint(gameObject);
            Destroy(gameObject);
        }
    }

    public virtual void Initialize(int trackId,float timer)
    {
        this.trackID = trackId;
        this.timer = timer;
    }

}
