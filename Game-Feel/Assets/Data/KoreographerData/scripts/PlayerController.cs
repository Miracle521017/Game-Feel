using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;

//玩家状态及道具
public enum PlayerType { PlayerA, PlayerB }
public enum GloveState { BareHand, Gloved }
public enum ToolType { None, Brush, Cloth, CupBrush }
public enum PlayerState { Up, Down }

public class PlayerController : MonoBehaviour
{
    public GameManager_1 gameManager;//游戏全局状态管理

    public PlayerType playerType; // 当前玩家类型
    public GloveState currentGlove = GloveState.BareHand; // 当前手套状态
    public ToolType currentTool = ToolType.None; // 当前使用的道具
    public PlayerState playerState;//当前玩家状态

    private KeyCode leftKey;
    private KeyCode rightKey;
    private KeyCode upKey;
    private KeyCode downKey;
    private KeyCode gloveKey; // 切换手套状态的按键
    private KeyCode[] toolKeys; // 切换道具的按键数组
    private KeyCode noneKey; // 取消道具的按键

    public bool isDown=true;//标识玩家所在轨道方向（true使用下面的轨道 false则为上面的轨道）

    //对应左右手需要的轨道
    public int leftIndex = 0;
    public int rightIndex = 1;

    [Header("Hand Objects")]
    public GameObject bareHandLeft; // 左裸手模型
    public GameObject bareHandRight; // 右裸手模型
    public GameObject glovedHandLeft; // 左戴手套模型
    public GameObject glovedHandRight; // 右戴手套模型

    [Header("动画组件")]
    public Animator left;
    public Animator right;

    void Start()
    {
        gameManager=GameObject.Find("Managers").GetComponent<GameManager_1>();
        playerState=PlayerState.Down;
        InitializeControls(); // 初始化控制按键
        UpdateHandVisuals(); // 更新手部视觉效果
    }

    void Update()
    {

        //检测玩家输入并检测是否需要切换轨道
        InputCheck();
        HandleSwitchUpAndDown();
        //  HandleGloveToggle(); // 处理手套切换
        // HandleToolSelection(); // 处理道具选择


    }

    //初始化玩家状态
    void InitializeControls()
    {
        if (playerType == PlayerType.PlayerA)
        {
            leftKey = KeyCode.A;
            leftIndex = 0;
            rightKey = KeyCode.D;
            rightIndex = 1;
            upKey=KeyCode.W; 
            downKey=KeyCode.S;
            gloveKey = KeyCode.LeftShift; // 玩家A的手套切换按键
            toolKeys = new KeyCode[] { KeyCode.H, KeyCode.J, KeyCode.K }; // 玩家A的道具选择按键
            noneKey = KeyCode.Space; // 玩家A的取消道具按键
        }
        else
        {
            leftKey = KeyCode.LeftArrow;
            leftIndex = 2;
            rightKey = KeyCode.RightArrow;
            rightIndex = 3;
            upKey = KeyCode.UpArrow;
            downKey = KeyCode.DownArrow;
            gloveKey = KeyCode.RightShift; // 玩家B的手套切换按键
            toolKeys = new KeyCode[] { KeyCode.Keypad1, KeyCode.Keypad2, KeyCode.Keypad3 }; // 玩家B的道具选择按键
            noneKey = KeyCode.Keypad0; // 玩家B的取消道具按键
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
                //TODO：更新视觉效果
                currentTool = (ToolType)(i + 1);
            }
        }

        if (Input.GetKeyDown(noneKey))
        {
            //TODO：更新视觉效果
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

    void InputCheck()
    {
        //Debug.Log("Checking input for player type: " + playerType);
        // 处理按键输入
        if (Input.GetKeyDown(leftKey))
        {
            left.SetBool("Pressed", true);
            gameManager.judgementZones[leftIndex].Check(playerType);
        }
        else if (Input.GetKeyDown(rightKey))
        {
            right.SetBool("Pressed", true);
            gameManager.judgementZones[rightIndex].Check(playerType);
        }

        //抬起
        if (Input.GetKeyUp(leftKey))
        {
            left.SetBool("Pressed", false);
        }
        else if(Input.GetKeyUp(rightKey))
        {
            right.SetBool("Pressed",false);
        }
    }

    void HandleSwitchUpAndDown()//切换方向
    {
        if (Input.GetKeyDown(upKey))
        {
            if (playerState == PlayerState.Down)
            {
                playerState = PlayerState.Up;
                //切换到上方
                if (playerType == PlayerType.PlayerA)
                {
                    //切换轨道
                    leftIndex = 4; 
                    rightIndex=5;
                    //TODO：加音效、改变位置
                }
                else
                {
                    leftIndex=6; 
                    rightIndex=7;
                    //同上
                }  
            }
        }
        else if (Input.GetKeyDown(downKey))
        {
            if (playerState == PlayerState.Up)
            {
                playerState = PlayerState.Down;
                //切换到上方
                if (playerType == PlayerType.PlayerA)
                {
                    //切换轨道
                    leftIndex = 0;
                    rightIndex = 1;
                    //TODO：加音效、改变位置
                }
                else
                {
                    leftIndex = 2;
                    rightIndex = 3;
                    //同上
                }
            }
        }
    }

}
