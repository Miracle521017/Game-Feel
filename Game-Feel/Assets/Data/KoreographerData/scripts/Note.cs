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
    [Header("Koreographer设置")]
    //[EventID] public string eventId;          // 在Inspector面板中选择对应的事件ID

    [Header("流速设置")]
    public NoteType noteType;
    public bool moveDirection;
    public float moveSpeed = 5f;
    public float perfectTime = 0.5f;
    public float activeTime = 0.2f;

    [Header("Requirements")]
    public GloveState requiredGlove;
    public ToolType requiredTool;

    public float spawnTime;
    public bool isJudged = false;
    public bool isActive = false;

    public AudioSource noteAudioSource; // 音符的AudioSource组件

    private Vector3 startPos;
    private Vector3 endPos;
    private float journeyLength;
    private float startTime;

    void Start()
    {
        Debug.Log("开始");
        spawnTime = Time.time;
        //ConfigureRequirements();

        // 根据方向设置起始和结束位置
        if (!moveDirection)
        {
            startPos = transform.position;
            endPos = new Vector3(transform.position.x, transform.position.y - 8f, transform.position.z);
        }
        else
        {
            startPos = new Vector3(transform.position.x, transform.position.y - 8f, transform.position.z);
            endPos = transform.position;
            transform.position = startPos;
        }

        journeyLength = Vector3.Distance(startPos, endPos);
        startTime = Time.time;

        // 注册Koreographer事件
        //Koreographer.Instance.RegisterForEvents("RotateNote", OnRotateNoteEvent);
    }

    void Update()
    {
        Debug.Log("Note script is running");
        if (!isJudged)
        {
            float distCovered = (Time.time - startTime) * moveSpeed;
            float fracJourney = distCovered / journeyLength;

            if (!moveDirection)
            {
                transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
            }
            else
            {
                transform.position = Vector3.Lerp(startPos, endPos, fracJourney);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D called");
        if (other.CompareTag("Checker") && !isActive)
        {
            Debug.Log("进入判定区域！");
            isActive = true;
            //StartCoroutine(DeactivateAfterTime());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("离开判定区域");
        if (other.CompareTag("Checker"))
        {
            isActive = false;
        }
    }

    //IEnumerator DeactivateAfterTime()
    //{
    //    yield return new WaitForSeconds(activeTime);
    //    if (!isJudged)
    //    {
    //       // JudgementSystem.Instance.MissNote(this);
    //        //Destroy(gameObject);
    //    }
    //}

    //void ConfigureRequirements()
    //{
    //    string currentScene = SceneManager.GetActiveScene().name;

    //    switch (currentScene)
    //    {
    //        case "Level1":
    //        case "Level2":
    //            requiredGlove = GloveState.BareHand;
    //            requiredTool = ToolType.None;
    //            break;
    //        case "Level3":
    //            switch (noteType)
    //            {
    //                case NoteType.ShellFood:
    //                    requiredGlove = GloveState.BareHand;
    //                    requiredTool = ToolType.Brush;
    //                    break;
    //                case NoteType.RegularVeggie:
    //                    requiredGlove = GloveState.BareHand;
    //                    requiredTool = ToolType.None;
    //                    break;
    //                case NoteType.Cookware:
    //                    requiredGlove = GloveState.Gloved;
    //                    requiredTool = ToolType.Cloth;
    //                    break;
    //                case NoteType.Yam:
    //                    requiredGlove = GloveState.Gloved;
    //                    requiredTool = ToolType.None;
    //                    break;
    //            }
    //            break;
    //    }
    //}

    //public void Judge(PlayerController player)
    //{
    //    if (isJudged || !ShouldHandleInput()) return;

    //    float timeDiff = Mathf.Abs(Time.time - spawnTime);
    //    bool conditionMet = requiredGlove == player.currentGlove &&
    //                      requiredTool == player.currentTool;

    //    if (conditionMet)
    //    {
    //        if (timeDiff <= perfectTime)
    //        {
    //            GameManager.Instance.AddScore(100); // 假设perfectScore为100
    //            ShowJudgementEffect("PERFECT!");
    //        }
    //        else
    //        {
    //            GameManager.Instance.AddScore(50); // 假设goodScore为50
    //            ShowJudgementEffect("GOOD");
    //        }
    //    }
    //    else
    //    {
    //        GameManager.Instance.AddScore(-50); // 假设missPenalty为-50
    //        ShowJudgementEffect("MISS");
    //        GameManager.Instance.BreakCombo();
    //    }

    //    // 播放音效
    //    if (noteAudioSource != null)
    //    {
    //        noteAudioSource.Play();
    //    }

    //    isJudged = true;
    //    Destroy(gameObject);
    //}

    //void ShowJudgementEffect(string result)
    //{
    //    // 显示判定效果，比如UI文本或粒子效果
    //    Debug.Log(result);
    //}

    //void OnRotateNoteEvent(KoreographyEvent koreoEvent)
    //{
    //    // 旋转音符
    //    StartCoroutine(RotateNote());
    //}

    //IEnumerator RotateNote()
    //{
    //    float duration = 1f; // 旋转持续时间
    //    float angle = 360f; // 旋转角度
    //    float startTime = Time.time;
    //    Vector3 startRotation = transform.eulerAngles;
    //    Vector3 targetRotation = new Vector3(startRotation.x, startRotation.y, startRotation.z + angle);

    //    while (Time.time - startTime < duration)
    //    {
    //        float t = (Time.time - startTime) / duration;
    //        transform.eulerAngles = Vector3.Lerp(startRotation, targetRotation, t);
    //        yield return null;
    //    }

    //    transform.eulerAngles = targetRotation;
    //}

    //void OnDestroy()
    //{
    //    // 取消注册Koreographer事件
    //   // Koreographer.Instance.UnregisterForEvents(this);
    //}

    //bool ShouldHandleInput()
    //{
    //    string currentScene = SceneManager.GetActiveScene().name;

    //    // 根据你的场景命名规则调整这些条件
    //    switch (currentScene)
    //    {
    //        case "Level1":
    //            // 第一关卡只允许基础操作
    //            return true;

    //        case "Level2":
    //            // 第二关卡允许更多操作
    //            return true;

    //        case "Level3":
    //            // 第三关卡允许所有操作
    //            return true;

    //        default:
    //            // 其他场景不允许操作
    //            return true;
    //    }
   // }
}