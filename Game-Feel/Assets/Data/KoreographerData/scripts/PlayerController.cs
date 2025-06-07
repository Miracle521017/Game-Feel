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

   [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode upKey;
    [SerializeField] private KeyCode downKey;
    [SerializeField] private KeyCode gloveKey; // 切换手套状态的按键
    [SerializeField] private KeyCode[] toolKeys; // 切换道具的按键数组
    [SerializeField] private KeyCode noneKey; // 取消道具的按键

    public bool isDown=true;//标识玩家所在轨道方向（true使用下面的轨道 false则为上面的轨道）

    //对应左右手需要的轨道
    public int leftIndex = 0;
    public int rightIndex = 1;

    [Header("Hand Objects")]
    // 下方轨道的手部模型
    public GameObject bareHandLeftDown; // 左裸手模型(下方)
    public GameObject bareHandRightDown; // 右裸手模型(下方)
    public GameObject glovedHandLeftDown; // 左戴手套模型(下方)
    public GameObject glovedHandRightDown; // 右戴手套模型(下方)

    // 上方轨道的手部模型
    public GameObject bareHandLeftUp; // 左裸手模型(上方)
    public GameObject bareHandRightUp; // 右裸手模型(上方)
    public GameObject glovedHandLeftUp; // 左戴手套模型(上方)
    public GameObject glovedHandRightUp; // 右戴手套模型(上方)

    [Header("动画组件")]
    public Animator bareLeftDownAnimator; // 下方轨道左手裸手动画
    public Animator bareRightDownAnimator; // 下方轨道右手裸手动画
    public Animator glovedLeftDownAnimator; // 下方轨道左手戴手套动画
    public Animator glovedRightDownAnimator; // 下方轨道右手戴手套动画
    public Animator bareLeftUpAnimator; // 上方轨道左手裸手动画
    public Animator bareRightUpAnimator; // 上方轨道右手裸手动画
    public Animator glovedLeftUpAnimator; // 上方轨道左手戴手套动画
    public Animator glovedRightUpAnimator; // 上方轨道右手戴手套动画

   [SerializeField] private Animator currentLeftAnimator;
   [SerializeField] private Animator currentRightAnimator;

    void Start()
    {
        gameManager=GameObject.Find("Managers").GetComponent<GameManager_1>();
        playerState=PlayerState.Down;
        InitializeControls(); // 初始化控制按键

        // 初始化动画机引用
        if (isDown)
        {
            if (currentGlove == GloveState.BareHand)
            {
                currentLeftAnimator =  bareLeftDownAnimator;
                currentRightAnimator = bareRightDownAnimator;
            }
            else
            {
                currentLeftAnimator = glovedLeftDownAnimator ;
                currentRightAnimator = glovedRightDownAnimator ;
            }
        }

        UpdateAllVisuals(); // 更新手部视觉效果
    }

    void Update()
    {

        //检测玩家输入并检测是否需要切换轨道
        InputCheck();
        HandleSwitchUpAndDown();
        HandleGloveToggle(); // 处理手套切换
        HandleToolSelection(); // 处理道具选择


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
        else if (playerType == PlayerType.PlayerB)
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
            UpdateAllVisuals();
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

    void UpdateAllVisuals()
    {
        UpdateHandVisuals();
        UpdateAnimator();
    }

    // 更新手部视觉效果
    void UpdateHandVisuals()
    {
        // 确保所有手部对象都存在
        if (bareHandLeftDown == null || bareHandRightDown == null ||
            glovedHandLeftDown == null || glovedHandRightDown == null ||
            bareHandLeftUp == null || bareHandRightUp == null ||
            glovedHandLeftUp == null || glovedHandRightUp == null)
        {
            Debug.LogError("Some hand objects are not assigned!");
            return;
        }

        // 根据手套状态和轨道位置显示/隐藏对应的手部模型
        if (isDown)
        {
            // 下方轨道的手部模型
            bareHandLeftDown.SetActive(currentGlove == GloveState.BareHand);
            bareHandRightDown.SetActive(currentGlove == GloveState.BareHand);
            glovedHandLeftDown.SetActive(currentGlove == GloveState.Gloved);
            glovedHandRightDown.SetActive(currentGlove == GloveState.Gloved);

            // 上方轨道的手部模型
            bareHandLeftUp.SetActive(false);
            bareHandRightUp.SetActive(false);
            glovedHandLeftUp.SetActive(false);
            glovedHandRightUp.SetActive(false);
        }
        else
        {
            // 下方轨道的手部模型
            bareHandLeftDown.SetActive(false);
            bareHandRightDown.SetActive(false);
            glovedHandLeftDown.SetActive(false);
            glovedHandRightDown.SetActive(false);

            // 上方轨道的手部模型
            bareHandLeftUp.SetActive(currentGlove == GloveState.BareHand);
            bareHandRightUp.SetActive(currentGlove == GloveState.BareHand);
            glovedHandLeftUp.SetActive(currentGlove == GloveState.Gloved);
            glovedHandRightUp.SetActive(currentGlove == GloveState.Gloved);
        }
    }

    // 更新动画器的可见性
    void UpdateAnimator()
    {
        if (isDown)
        {
            if (currentGlove == GloveState.BareHand)
            {
                currentLeftAnimator= bareLeftDownAnimator ;
                currentRightAnimator =bareRightDownAnimator;
            }
            else
            {
                currentLeftAnimator= glovedLeftDownAnimator;
                currentRightAnimator = glovedRightDownAnimator;
            }
        }
        else
        {
            if (currentGlove == GloveState.BareHand)
            {
                currentLeftAnimator = bareLeftUpAnimator ;
                currentRightAnimator = bareRightUpAnimator ;
            }
            else
            {
                currentLeftAnimator =glovedLeftUpAnimator ;
                currentRightAnimator = glovedRightUpAnimator;
            }
        }

        // 确保动画器可见性
        bareLeftDownAnimator.gameObject.SetActive(false);
        bareRightDownAnimator.gameObject.SetActive(false);
        glovedLeftDownAnimator.gameObject.SetActive(false);
        glovedRightDownAnimator.gameObject.SetActive(false);
        bareLeftUpAnimator.gameObject.SetActive(false);
        bareRightUpAnimator.gameObject.SetActive(false);
        glovedLeftUpAnimator.gameObject.SetActive(false);
        glovedRightUpAnimator.gameObject.SetActive(false);

        if (isDown)
        {
            if (currentGlove == GloveState.BareHand)
            {
                bareLeftDownAnimator.gameObject.SetActive(true);
                bareRightDownAnimator.gameObject.SetActive(true);
            }
            else
            {
                glovedLeftDownAnimator.gameObject.SetActive(true);
                glovedRightDownAnimator.gameObject.SetActive(true);
            }
        }
        else
        {
            if (currentGlove == GloveState.BareHand)
            {
                bareLeftUpAnimator.gameObject.SetActive(true);
                bareRightUpAnimator.gameObject.SetActive(true);
            }
            else
            {
                glovedLeftUpAnimator.gameObject.SetActive(true);
                glovedRightUpAnimator.gameObject.SetActive(true);
            }
        }
    }

    void InputCheck()
    {
        // 处理按键输入
        if (Input.GetKeyDown(leftKey))
        {
            currentLeftAnimator.SetBool("Pressed", true);
            gameManager.judgementZones[leftIndex].Check(playerType);
        }
        else if (Input.GetKeyDown(rightKey))
        {
            currentRightAnimator.SetBool("Pressed", true);
            gameManager.judgementZones[rightIndex].Check(playerType);
        }

        // 抬起
        if (Input.GetKeyUp(leftKey))
        {
            currentLeftAnimator.SetBool("Pressed", false);
        }
        else if (Input.GetKeyUp(rightKey))
        {
            currentRightAnimator.SetBool("Pressed", false);
        }
    }

    void HandleSwitchUpAndDown()//切换方向
    {
        if (Input.GetKeyDown(upKey))
        {
            if (playerState == PlayerState.Down)
            {
                playerState = PlayerState.Up;
                isDown = false;
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
                UpdateAllVisuals();
            }
        }
        else if (Input.GetKeyDown(downKey))
        {
            if (playerState == PlayerState.Up)
            {
                playerState = PlayerState.Down;
                isDown = true;
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
                UpdateAllVisuals();
            }
        }
    }

}
