using SonicBloom.Koreo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [Header("Koreographer设置")]
    [EventID]
    public string eventId;          // 在Inspector面板中选择对应的事件ID
    [Header("跳跃参数")]
    public float jumpHeight = 0.5f; // 跳跃高度
    public float jumpSpeed = 2f;    // 跳跃速度
    [Header("摆动参数")]
    public float swingAmount = 5f;  // 摆动幅度（角度）
    public float swingSpeed = 1f;   // 摆动速度

    private Vector3 startPos;       // 初始位置
    private Quaternion startRot;    // 初始旋转
    private bool isAnimating;       // 动画状态标志

    void Start()
    {
        // 记录初始状态
        startPos = transform.position;
        startRot = transform.rotation;

        // 注册Koreographer事件
        Koreographer.Instance.RegisterForEvents(eventId, OnMusicEvent);
    }

    void OnDestroy()
    {
        // 确保取消事件注册
        if (Koreographer.Instance != null)
        {
            Koreographer.Instance.UnregisterForEvents(eventId, OnMusicEvent);
        }
    }

    void OnMusicEvent(KoreographyEvent evt)
    {
        // 如果已有动画在进行，先停止
        if (isAnimating)
        {
            StopAllCoroutines();
        }

        // 启动组合动画
        StartCoroutine(JumpAnimation());
        StartCoroutine(SwingAnimation());
    }

    // 跳跃动画协程
    IEnumerator JumpAnimation()
    {
        isAnimating = true;
        float progress = 0f;

        while (progress < 1f)
        {
            // 使用正弦曲线实现跳跃抛物线
            float yOffset = Mathf.Sin(progress * Mathf.PI) * jumpHeight;
            transform.position = startPos + Vector3.up * yOffset;

            progress += Time.deltaTime * jumpSpeed;
            yield return null;
        }

        // 重置位置
        transform.position = startPos;
        isAnimating = false;
    }

    // 摆动动画协程
    IEnumerator SwingAnimation()
    {
        float timer = 0f;
        float startAngle = transform.localEulerAngles.z;

        while (timer < 1f)
        {
            // 使用正弦波实现平滑摆动
            float angle = Mathf.Sin(timer * Mathf.PI * 2) * swingAmount;
            transform.localEulerAngles = new Vector3(0, 0, startAngle + angle);

            timer += Time.deltaTime * swingSpeed;
            yield return null;
        }

        // 重置旋转
        transform.localRotation = startRot;
    }
}
