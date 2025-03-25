using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrumCollect : MonoBehaviour
{
    public Transform[] trackPoses;
    
    public Transform line;
    
    public GameObject pointPre;

    public AudioSource bgm;

    public Slider bgmSlider;

    private bool _dragSlider=false;

    [SerializeField]private float _cooldown = 0.05f;

    public float interval = 0.5f;
    public int CurrentTrackId=-1;

    public TrackTimerLists_Dic trackTimerLists_Dic;

    public int currentTrackCounts = 4;//“ÙπÏ ˝¡ø
    // Start is called before the first frame update
    void Start()
    {
        bgm.Pause();
    }

    void OnClickPlay()
    { 
        if(Input.GetKeyUp(KeyCode.Space))
        {
            if (bgm.isPlaying)
            {
                bgm.Pause();
                Debug.Log("‘›Õ££°");
            }
            else
            {
                bgm.Play();
            }
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        OnClickPlay();
        if (!_dragSlider)
        {
            bgmSlider.value=bgm.time/bgm.clip.length;
        }
        else
        {
            bgm.time=bgmSlider.value*bgm.clip.length;
        }

        foreach(var point in trackTimerLists_Dic.trackTimerLists)
        {
            point.gameObject.transform.position = new Vector3(point.trackId, (bgm.time - (point.timer)) * 10, 0);
        }

        _cooldown-=Time.deltaTime;
        if(_cooldown <= 0)
        {
            _cooldown = interval;
            AddPointFromKeyCode(KeyCode.A);
            AddPointFromKeyCode(KeyCode.D);
            AddPointFromKeyCode(KeyCode.LeftArrow);
            AddPointFromKeyCode(KeyCode.RightArrow);
        }
    }

    private void AddPoint(int trackId,float currentTime)
    {
        PointGameObject pNode = new PointGameObject();
        pNode.timer = currentTime;
        pNode.trackId = trackId;
        pNode.gameObject = Instantiate(pointPre);
        trackTimerLists_Dic.trackTimerLists.Add(pNode);
    }
    private void AddPointFromKeyCode(KeyCode keyCode)
    {
        if(Input.GetKey(keyCode))
        {
            if (currentTrackCounts == 4)
            {
                switch (keyCode)
                {
                    case KeyCode.A:
                        CurrentTrackId = 0;
                        break;
                    case KeyCode.D:
                        CurrentTrackId = 1;
                        break;
                    case KeyCode.LeftArrow:
                        CurrentTrackId = 2;
                        break;
                    case KeyCode.RightArrow:
                        CurrentTrackId = 3;
                        break;

                }
            }
            AddPoint(CurrentTrackId, bgm.time);
        }
    }
}
