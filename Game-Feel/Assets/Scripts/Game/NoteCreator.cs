using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteCreator : MonoBehaviour
{
    [Header("轨道配置")]
    public float noteSpeed = 15f; // 新增音符移动速度参数
    public float[] trackPositions = new float[4];

    [Header("引用配置")]
    public TrackTimerLists_Dic trackTimerLists_Dic;
    public AudioSource bgm;
    public Slider bgmSlider;
    public GameObject pointPre;
    public Transform notesparent;

    private List<PointGameObject> tempTrackList = new List<PointGameObject>();

    private void Start()
    {
        if (pointPre.GetComponent<Note>() == null)
        {
            Debug.LogError("音符预制体缺少NoteInfo组件！");
            return;
        }
        CreateNotes();
    }

    public void CreateNotes()
    {
        if (trackTimerLists_Dic)
        {
            foreach (var item in trackTimerLists_Dic.trackTimerLists)
            {
                AddPoint(item.trackId, item.timer);
            }
        }
    }

    private void AddPoint(int trackId, float currentTime)
    {
        var noteObj = Instantiate(pointPre, notesparent);
        noteObj.transform.position = new Vector3(
            trackPositions[trackId],
            0, // 初始Y位置设为0
            0
        );

        var noteInfo = noteObj.GetComponent<Note>();
        noteInfo.Initialize(trackId, currentTime);

        tempTrackList.Add(new PointGameObject
        {
            trackId = trackId,
            timer = currentTime,
            gameObject = noteObj
        });
    }

    public void DeletePoint(GameObject noteObject) 
    {
        var point = tempTrackList.Find(p => p.gameObject == noteObject);
        if (point != null)
        {
            tempTrackList.Remove(point);
            Destroy(noteObject);
        }
    }

    private void Update()
    {
        UpdateNotePositions();
        UpdateBGMSlider();
    }

    private void UpdateNotePositions()
    {
        List<PointGameObject> toRemove = new List<PointGameObject>();

        foreach (var note in tempTrackList)
        {
            if (note.gameObject == null)
            {
                toRemove.Add(note);
                continue;
            }

            float yPos = (bgm.time - note.timer) * -noteSpeed;
            note.gameObject.transform.position = new Vector3(
                trackPositions[note.trackId],
                yPos,
                0
            );

            // 自动清理超出范围的音符
            if (yPos < -10f)
            {
                toRemove.Add(note);
                Destroy(note.gameObject);
            }
        }

        foreach (var item in toRemove) tempTrackList.Remove(item);
    }

    private void UpdateBGMSlider()
    {
        if (bgm.clip != null)
            bgmSlider.value = bgm.time / bgm.clip.length;
    }
}
