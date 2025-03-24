using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteChecker : MonoBehaviour
{
    //[Header("轨道设置")]
    //public int trackID; // 对应轨道ID
    public KeyCode inputKey = KeyCode.A; // 可配置的输入按键

    //[Header("判定参数")]
    //public float perfectRange = 0.05f;
    //public float goodRange = 0.1f;
    //public float scoreMultiplier = 1.0f;

    //private GameObject currentNote; // 当前进入触发器的音符

    private void Update()
    {
        //if (Input.GetKeyDown(inputKey) )
        //{
        //    HandleNoteHit();
        //}
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Note"))
        {
            if (Input.GetKeyDown(inputKey))
            {
                Debug.Log("Cut!");
                Destroy(other.gameObject);
            }
             //NoteInfo noteInfo = other.GetComponent<NoteInfo>();
            //if (noteInfo != null && noteInfo.trackID == trackID)
            //{
            //    currentNote = other.gameObject;
            //}
        }
    }

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.gameObject == currentNote)
    //    {
    //        //HandleMissNote();
    //        Debug.Log("Miss");
    //        currentNote = null;
    //    }
    //}

    //private void HandleNoteHit()
    //{
    //    if (currentNote == null) return;

    //    // 计算时间差
    //    //NoteInfo noteInfo = currentNote.GetComponent<NoteInfo>();
    //    //float timeDifference = Mathf.Abs(bgm.time - noteInfo.timer);

    //    // 判定精度
    //    //if (timeDifference <= perfectRange)
    //    //{
    //    //    //gameManager.AddScore(100 * scoreMultiplier);
    //    //    Debug.Log("Perfect!");
    //    //}
    //    //else if (timeDifference <= goodRange)
    //    //{
    //    //    //gameManager.AddScore(50 * scoreMultiplier);
    //    //    Debug.Log("Good!");
    //    //}
    //    //else
    //    //{
    //    //    //gameManager.AddScore(10 * scoreMultiplier);
    //    //    Debug.Log("Miss!");
    //    //}

    //    DestroyNote();
    //}

    //private void HandleMissNote()
    //{
    //   // gameManager.ComboBreak();
    //    Debug.Log("Missed Note!");
    //    DestroyNote();
    //}

    //private void DestroyNote()
    //{
    //    if (currentNote != null)
    //    {
    //        Destroy(currentNote);
    //        currentNote = null;
    //    }
    //}
}
