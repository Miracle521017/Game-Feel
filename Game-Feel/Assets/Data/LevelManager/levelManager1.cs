using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager1 : MonoBehaviour
{
    public float levelDuration = 31f; // 关卡持续时间，单位为秒
    private float timer;
    public static levelManager1 instance;
    public string levelToLoad;

    private float starRating;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        timer = levelDuration; // 初始化计时器
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; // 减少计时器时间
        }
        else
        {
            EndLevel(); // 如果计时器时间小于或等于0，执行关卡结束函数
        }
    }
    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }
    public IEnumerator EndLevelCo()
    {
        //AudioManager.instance.PlayLevelVictory();

        yield return new WaitForSeconds(1.5f);
        UIController1.instance.FadeToBlack();
        yield return new WaitForSeconds((1f / UIController1.instance.fadeSpeed) + 3f);
        //关卡是否解锁判断
        if (GameManager.Instance.score > 60)
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 0);
        }
        
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        //得分是否更新判断
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_score", GameManager.Instance.score);
        /*if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_score")) 
        {
            if(GameManager.Instance.score > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_score", GameManager.Instance.score);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_score", GameManager.Instance.score);
        }*/

        //starRating = GameManager.Instance.CalculateStars();
        
        //    PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_starScore", starRating);
        
        //Debug.Log(starRating);

        //SceneManager.LoadScene(levelToLoad);
    }
}
