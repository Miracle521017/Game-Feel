using SonicBloom.Koreo;
using SonicBloom.MIDI.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum NoteType { ShellFood, RegularVeggie, Cookware, Yam }
public enum MoveDirection { Down, Up }

public class Note : MonoBehaviour
{
    [Header("流速设置")]
    public NoteType noteType = NoteType.RegularVeggie;
    public bool isDirectionChange = false;//是否会改变流动方向 f为向下 t为向上
    public float moveSpeed = 5f;
    public float perfectTime = 0.5f;
    public float activeTime = 0.2f;

    [Header("Requirements")]
    public GloveState requiredGlove;
    public ToolType requiredTool;
    public string LanesTag = "Checker_Down";//判定区域的标签

    //基础属性
    public float spawnTime;
    public bool isJudged = false;//是否被判定
    public bool isActive = false;//是否处于判定区

    //音效
    public AudioSource noteAudioSource; // 音符的AudioSource组件

    //音符流动效果
    public float length = 7f;//总体纵向移动距离
    private Vector3 startPos;
    private Vector3 endPos;
    private float journeyLength;
    private float startTime;

    void Start()
    {
        //确定方向
        SetDirection();

        //根据音符类型设置需求
        ConfigureRequirements();

        //记录开始时间
        startTime = Time.time;
    }



    void Update()
    {
        //更新物体位置
        if (!isJudged)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fracJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
        }

    }

    //设置方向
    public void SetDirection()
    {
        // 根据方向设置起始和结束位置
        if (!isDirectionChange)
        {
            startPos = transform.position;
            endPos = new Vector3(transform.position.x, transform.position.y - length, transform.position.z);
        }
        else
        {
            startPos = new Vector3(transform.position.x, transform.position.y - length, transform.position.z);
            endPos = transform.position;
            transform.position = startPos;//从底部开始流动
            LanesTag = "Checker_Up";
            //TODO：可选：改变音符朝向
        }
        //计算轨道长度，便于后续流动
        journeyLength = Vector3.Distance(startPos, endPos);
    }

    //进入判定区
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(LanesTag))
        {
            Debug.Log("Entered the judge zone");
            isActive = true;
            //TODO: 输入检测、判定
        }
    }

    //离开判定区
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(LanesTag))
        {
            isActive = false;
            Debug.Log("Note reached the end, judged as MISS");
            if (!isJudged)
            {
                Destroy(gameObject, 0.5f);//销毁物体
                // TODO:MISS处理
            }
        }
    }

    void ConfigureRequirements()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        switch (currentScene)
        {
            case "Level1":
            case "Level2":
                requiredGlove = GloveState.BareHand;
                requiredTool = ToolType.None;
                break;
            case "Level3":
                switch (noteType)
                {
                    case NoteType.ShellFood:
                        requiredGlove = GloveState.BareHand;
                        requiredTool = ToolType.Brush;
                        break;
                    case NoteType.RegularVeggie:
                        requiredGlove = GloveState.BareHand;
                        requiredTool = ToolType.None;
                        break;
                    case NoteType.Cookware:
                        requiredGlove = GloveState.Gloved;
                        requiredTool = ToolType.Cloth;
                        break;
                    case NoteType.Yam:
                        requiredGlove = GloveState.Gloved;
                        requiredTool = ToolType.None;
                        break;
                }
                break;
        }
    }
}