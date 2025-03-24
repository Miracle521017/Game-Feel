using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteCreator : MonoBehaviour
{
    [Header("轨道时刻表文件")]
    public TrackTimerLists_Dic trackTimerLists_Dic;

    public AudioSource bgm;

    public Slider bgmSlider;

    public GameObject pointPre;

    public Transform notesparent;

    private List<PointGameObject>tempTrackList= new List<PointGameObject>();

    [Header("轨道位置配置")]
    public float[] trackPositions = new float[4]; // 存储4个轨道的x坐标

    private void Start()
    {
        CreateNecks();
    }

    public void CreateNecks()
    {
        if (trackTimerLists_Dic)
        {
            foreach(var item  in trackTimerLists_Dic.trackTimerLists) 
            {
                AddPoint(item.trackId, item.timer);
            }
        }
    }
    private void AddPoint(int trackId, float currentTime)
    {
        PointGameObject pNode = new PointGameObject();
        pNode.timer = currentTime;
        pNode.trackId = trackId;
        pNode.gameObject = Instantiate(pointPre,notesparent);
       // trackTimerLists_Dic.trackTimerLists.Add(pNode);
        tempTrackList.Add(pNode); 
    }

    private void Update()
    {
        foreach(var notes in tempTrackList)
        {
            notes.gameObject.transform.position = new Vector3(trackPositions[notes.trackId], (bgm.time - notes.timer)*-15,0);
        }

   
            bgmSlider.value = bgm.time / bgm.clip.length;
  

    }
}
