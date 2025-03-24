using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private int trackID;
    [SerializeField] private float timer;
    private NoteCreator noteCreator;

    public int TrackID => trackID;
    public float Timer => timer;

    public void Initialize(int trackId, float timer)
    {
        this.trackID = trackId;
        this.timer = timer;
    }

    private void Start()
    {
        noteCreator = FindObjectOfType<NoteCreator>();
        if (noteCreator == null)
            Debug.LogError("ÕÒ²»µ½NoteCreatorÊµÀý");
    }

    public void TriggerNote()
    {
        if (noteCreator != null)
            noteCreator.DeletePoint(gameObject);

        Destroy(gameObject);
    }
}
