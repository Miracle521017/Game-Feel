using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartTextAnimation : MonoBehaviour
{
    public TextMeshProUGUI titleText; // 你的 TextMeshPro 文本对象
    public float animationDuration = 3.0f; // 动画持续时间
    public float dilateValue = 0.1f; // 目标 dilate 值
    public Vector2 endPosition; // 自定义目标位置

    public float jumpHeight = 10f; // 跳跃高度
    public float jumpCycleDuration = 1f; // 跳跃周期（1秒）


    public Vector2 startPosition;
    private float initialDilate;
    private float timer;
    private bool isDilateComplete = false;
    private bool isJumping = false;
    private float jumpTimer;

    void Start()
    {
        // 确保 titleText 已赋值
        if (titleText == null)
        {
            Debug.LogError("TextMeshProUGUI object is not assigned!");
            return;
        }

        // 初始化起始位置和 dilate 值
        startPosition = titleText.transform.position;
        initialDilate = -1.0f; // 初始 dilate 值
        titleText.fontMaterial.SetFloat("_FaceDilate", initialDilate);

        endPosition = new Vector2(Screen.width - 80f, Screen.height - 40f);
    }

    void Update()
    {
        if (timer < animationDuration)
        {
            timer += Time.deltaTime;

            // 计算插值比例
            float t = timer / animationDuration;

            if (!isDilateComplete)
            {
                // 插值计算 dilate 值
                float currentDilate = Mathf.Lerp(initialDilate, dilateValue, t);
                titleText.fontMaterial.SetFloat("_FaceDilate", currentDilate);

                // 检查 dilate 是否达到目标值
                if (Mathf.Approximately(currentDilate, dilateValue))
                {
                    isDilateComplete = true;
                    timer = 0; // 重置计时器以开始移动
                }
            }
            else
            {
                // 插值计算位置
                Vector2 currentPosition = Vector2.Lerp(startPosition, endPosition, t);
                titleText.transform.position = currentPosition;

                // 检查是否到达目标位置
                if (Vector2.Distance(titleText.transform.position, endPosition) < 0.1f)
                {
                    isJumping = true;
                }
            }
        }

        // 跳跃动画
        if (isJumping)
        {
            jumpTimer += Time.deltaTime;
            float jumpT = jumpTimer / jumpCycleDuration;

            // 使用正弦函数计算跳跃位置
            float yOffset = Mathf.Sin(jumpT * Mathf.PI * 2) * jumpHeight;
            Vector2 jumpingPosition = new Vector2(endPosition.x, endPosition.y + yOffset);
            titleText.transform.position = jumpingPosition;
        }

    }
}
