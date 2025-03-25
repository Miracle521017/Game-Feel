using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private int trackID;
    [SerializeField] private float timer;
    private NoteCreator noteCreator;

    public AudioSource audioSource;

    public int TrackID => trackID;
    public float Timer => timer;

    public void Initialize(int trackId, float timer)
    {
        this.trackID = trackId;
        this.timer = timer;
    }

    private void Start()
    {
        audioSource = GameObject.Find("水果音效").GetComponent<AudioSource>();
        noteCreator = FindObjectOfType<NoteCreator>();
        if (noteCreator == null)
            Debug.LogError("找不到NoteCreator实例");
    }

    public void TriggerNote()
    {
        // 添加计分逻辑
        GameManager.Instance?.AddScore();
        audioSource.Play();
        if (noteCreator != null)
            noteCreator.DeletePoint(gameObject);
        Destroy(gameObject);
       
    }
}
