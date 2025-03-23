using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newTrackTimer",menuName="CreateDate/Create New TrackTimerData")]
public class TrackTimerLists_Dic : ScriptableObject
{
    public List<PointGameObject> trackTimerLists;
}
public class PointData
{
    public float timer;//时刻
    public int trackId;//所在轨道

    public PointData() { }

    public PointData(float timer, int trackId)
    {
        this.timer = timer;
        this.trackId = trackId;
    }
}

[System.Serializable]
public class PointGameObject : PointData
{
    public GameObject gameObject;
}