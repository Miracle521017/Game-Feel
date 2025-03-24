using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newTrackTimer", menuName = "CreateDate/Create New TrackTimerData")]
public class TrackTimerLists_Dic : ScriptableObject
{
    public List<PointGameObject> trackTimerLists = new List<PointGameObject>();
}

[System.Serializable]
public class PointGameObject
{
    public int trackId;
    public float timer;
    public GameObject gameObject;

    public PointGameObject() { }

    public PointGameObject(int trackId, float timer)
    {
        this.trackId = trackId;
        this.timer = timer;
    }
}