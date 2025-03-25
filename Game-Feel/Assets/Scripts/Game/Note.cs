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
            Debug.LogError("æ‰¾ä¸åˆ°NoteCreatorå®ä¾‹");
    }

    public void TriggerNote()
    {

        // Ìí¼Ó¼Æ·ÖÂß¼­
        GameManager.Instance?.AddScore();
       
        if (noteCreator != null)
            noteCreator.DeletePoint(gameObject);
        Destroy(gameObject);
       
    }
}
