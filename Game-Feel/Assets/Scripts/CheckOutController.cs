using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckOutController : MonoBehaviour
{
    public string levelToCheck;
    public Text scoreText;
    public Image star1;
    public Image star2;
    public Image star3;

    public int starScore;//相当于combo
    // Start is called before the first frame update
    public static CheckOutController instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        
    }

    void Start()
    {//这里是判定
        if (PlayerPrefs.HasKey(levelToCheck + "_score"))
        {
            scoreText.text=PlayerPrefs.GetInt(levelToCheck + "_score").ToString();
        }
     
       if (PlayerPrefs.HasKey(levelToCheck + "_startScore"))
       {
           starScore = PlayerPrefs.GetInt(levelToCheck + "_startScore");
       }
        star1.enabled = starScore >= 1;
        star2.enabled = starScore >= 2;
        star3.enabled = starScore >= 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
