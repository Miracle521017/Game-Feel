using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

//玩家状态及道具
public enum PlayerType { PlayerA, PlayerB }
public enum GloveState { BareHand, Gloved }
public enum ToolType { None, Brush, Cloth, CupBrush }

public class PlayerController : MonoBehaviour
{
    public PlayerType playerType;
    public GloveState currentGlove = GloveState.BareHand;
    public ToolType currentTool = ToolType.None;

    private KeyCode gloveKey;
    private KeyCode[] toolKeys;
    private KeyCode noneKey;

    [Header("Hand Objects")]
    public GameObject bareHandLeft;
    public GameObject bareHandRight;
    public GameObject glovedHandLeft;
    public GameObject glovedHandRight;

    void Start()
    {
        InitializeControls();
        UpdateHandVisuals();
    }

    void Update()
    {
        // 只在允许操作的场景中处理输入
        if (ShouldHandleInput())
        {
            HandleGloveToggle();
            HandleToolSelection();
        }
    }

    //初始化玩家状态
    void InitializeControls()
    {
        if (playerType == PlayerType.PlayerA)
        {
            gloveKey = KeyCode.LeftShift;
            toolKeys = new KeyCode[] { KeyCode.H, KeyCode.J, KeyCode.K };
            noneKey = KeyCode.Space;
        }
        else
        {
            gloveKey = KeyCode.RightShift;
            toolKeys = new KeyCode[] { KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3 };
            noneKey = KeyCode.Keypad0;
        }
    }

    //手套状态检测
    void HandleGloveToggle()
    {
        if (Input.GetKeyDown(gloveKey))
        {
            currentGlove = currentGlove == GloveState.BareHand ?
                          GloveState.Gloved : GloveState.BareHand;
        }
    }

    //道具状态检测
    void HandleToolSelection()
    {
        for (int i = 0; i < toolKeys.Length; i++)
        {
            if (Input.GetKeyDown(toolKeys[i]))
            {
                currentTool = (ToolType)(i + 1);
            }
        }

        if (Input.GetKeyDown(noneKey))
        {
            currentTool = ToolType.None;
        }
    }
    // 更新手部视觉效果
    void UpdateHandVisuals()
    {
        // 确保所有手部对象都存在
        if (bareHandLeft == null || bareHandRight == null ||
            glovedHandLeft == null || glovedHandRight == null)
        {
            Debug.LogError("Some hand objects are not assigned!");
            return;
        }

        // 根据手套状态显示/隐藏对应的手部模型
        bareHandLeft.SetActive(currentGlove == GloveState.BareHand);
        bareHandRight.SetActive(currentGlove == GloveState.BareHand);
        glovedHandLeft.SetActive(currentGlove == GloveState.Gloved);
        glovedHandRight.SetActive(currentGlove == GloveState.Gloved);
    }

    // 判断当前场景是否允许玩家操作
    bool ShouldHandleInput()
    {
        string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        // 根据你的场景命名规则调整这些条件
        switch (currentScene)
        {
            case "Level1":
                // 第一关卡只允许基础操作
                return true;

            case "Level2":
                // 第二关卡允许更多操作
                return true;

            case "Level3":
                // 第三关卡允许所有操作
                return true;

            default:
                // 其他场景不允许操作
                return false;
        }
    }
}
