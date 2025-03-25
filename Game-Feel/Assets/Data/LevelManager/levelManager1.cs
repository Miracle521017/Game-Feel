using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelManager1 : MonoBehaviour
{
    public static levelManager1 instance;
    public string levelToLoad;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }
    public IEnumerator EndLevelCo()
    {
        //AudioManager.instance.PlayLevelVictory();

        yield return new WaitForSeconds(1.5f);
        //关卡是否解锁判断
        if (GameManager.Instance.score > 60)
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1);
        }
        
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        //得分是否更新判断
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_score")) 
        {
            if(GameManager.Instance.score > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_score", GameManager.Instance.score);
            }
        }
        else
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_score", GameManager.Instance.score);
        }

        /*if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_starScore"))
        {
            if( < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time"))
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", timeInLevel);
        }*/

        SceneManager.LoadScene(levelToLoad);
    }
}
