using SonicBloom.Koreo;
using SonicBloom.MIDI.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum NoteType { ShellFood, RegularVeggie, Cookware, Yam }
public enum MoveDirection { Down, Up }

[System.Serializable]
public class Note : MonoBehaviour
{
    [Header("流速设置")]
    public NoteType noteType = NoteType.RegularVeggie;
    public bool isDirectionChange = false; // 是否会改变流动方向 f为向下 t为向上
    public float moveSpeed = 5f;
    public float perfectTime = 0.5f;
    public float activeTime = 0.2f;

    [Header("Requirements")]
    public GloveState requiredGlove;
    public ToolType requiredTool;
    public string targetTag = "Checker_Down"; // 判定区域的标签

    // 基础属性
    public float spawnTime;
    public bool isJudged = false; // 是否被判定
    public bool isActive = false; // 是否处于判定区
    public int track; // 音符所在的轨道索引（与对应的判定区域的索引一致,0123为下面四个轨道，4567为上面四个轨道,0145为p1控制的轨道，2367为p2控制的轨道）

    public List<JudgementZone> judgementZones;//所有判定区域的物体列表

    public int playerIndex=0;//当前音符所属于的玩家,0为P1，1为p2

    // 音效
    public AudioSource noteAudioSource; // 音符的AudioSource组件

    public GameManager_1 gameManager;//当前关卡的游戏管理系统

    public List<PlayerController> playerControllers= new List<PlayerController>();//当前局内所有玩家的控制脚本 以双人游戏为例 0为P1，1为P2

    // 音符流动效果
    public float length = 7f; // 总体纵向移动距离
    private Vector3 startPos;
    private Vector3 endPos;
    private float journeyLength;
    private float startTime;

    public bool isConditionCorrect=true;//音符是否与当前玩家状态匹配

    void Start()
    {
        if(gameManager == null)
        {
            gameManager = GameObject.Find("Managers").GetComponent<GameManager_1>();//初始化
        }

        if (track > 3)
        {
            isDirectionChange = true;
            targetTag = "Checker_Up";
        }

        // 确定方向
        SetDirection();

        //设置音符所属玩家
        CheckPlayer();

        // 根据音符类型设置需求
        ConfigureRequirements();

        // 记录开始时间
        startTime = Time.time;
    }

    void Update()
    {
        // 更新物体位置
        if (!isJudged)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fracJourney = distCovered / journeyLength;

            transform.position = Vector3.Lerp(startPos, endPos, fracJourney);

            // 检查是否到达终点
            if (fracJourney >= 1.0f)
            {
                Debug.Log("Note reached the end, judged as MISS");
                if (!isJudged)
                {
                    Destroy(gameObject);
                    //Miss
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 设置方向
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
            transform.position = startPos; // 从底部开始流动
            // TODO：可选：改变音符朝向
        }
        // 计算轨道长度，便于后续流动
        journeyLength = Vector3.Distance(startPos, endPos);
    }

    // 进入判定区
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag)&&!isActive)
        {
            gameManager.judgementZones[track].RecordNote(this);
            isActive = true;
        }
    }

    // 离开判定区
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(targetTag)&&isActive)
        {
            gameManager.judgementZones[track].RemoveNote(this);
            isActive = false;
            //Debug.Log("音符离开轨道 " + track + " 的判定区域");
            if (!isJudged)
            {
                Destroy(gameObject);
            }
        }
    }

    void ConfigureRequirements()
    { 
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
    }

    //检测音符对应的玩家
    public void CheckPlayer()
    {
        if(track==0||track==1||track == 4 || track == 5)
        {
            playerIndex = 0;
        }
        else
        {
            playerIndex = 1;
        }
    }

    //检测音符触发的条件是否正确
    public void CheckCondition()
    {
        if (playerControllers.Count >= 2)
        {
            if (playerControllers[playerIndex].currentTool == requiredTool && playerControllers[playerIndex].currentGlove==requiredGlove)
            {
                Debug.Log("道具对应正确！");
                isConditionCorrect = true;
            }
            else
            {
                Debug.Log("道具对应错误！");
                isConditionCorrect= false;
            }
        }
        else
        {
            Debug.Log("初始化不正确！");
        }
        
    }
}