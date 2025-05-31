using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementZone : MonoBehaviour
{
    public static JudgementZone Instance;
    public static int TrackCount = 4;

    [Header("音符轨道列表")]
    public List<Note>[] trackNotes = new List<Note>[TrackCount]; // 轨道列表，每个轨道一个音符列表

    public Dictionary<int, List<Note>> notesToJudge = new Dictionary<int, List<Note>>(); // 用于快速查找和管理
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // 初始化每个轨道的音符列表
        trackNotes = new List<Note>[TrackCount];
        notesToJudge = new Dictionary<int, List<Note>>();

        for (int i = 0; i < trackNotes.Length; i++)
        {
            trackNotes[i] = new List<Note>();
            notesToJudge[i] = new List<Note>();
        }
        //Debug.Log($"JudgementZone initialized with {TrackCount} tracks.");
    }

    //记录音符入轨道列表
    public void RecordNote(Note note, int track)
    {
        if (track >= 0 && track < TrackCount)
        {
            trackNotes[track].Add(note);
            notesToJudge[track].Add(note);
        }
        Debug.Log("音符记录到轨道 " + track + " 的判定列表中");
    }

    //获取列表第一个音符的方法
    public Note GetNote(int track)
    {
        if (track >= 0 && track < TrackCount && notesToJudge[track].Count > 0)
        {
            return notesToJudge[track][0];//返回第一个音符
        }
        return null;
    }

    //移出音符的方法
    public bool RemoveNote(Note note, int track)
    {
        if (track >= 0 && track < TrackCount && notesToJudge[track].Contains(note))
        {
            notesToJudge[track].Remove(note);
            trackNotes[track].Remove(note); // 同步移除
            Debug.Log($"Note ' removed from track {track}.");
            return true;
        }
        else
        {
            Debug.LogError("移除失败！");
        }
        Debug.LogWarning("尝试从轨道 " + track + " 移除不存在的音符");
        return false;
    }
}
