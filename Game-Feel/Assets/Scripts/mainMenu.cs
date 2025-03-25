using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mainMenu : MonoBehaviour
{   public string startScene; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        StartCoroutine(LoadSceneAfterSound());
    }
    private IEnumerator LoadSceneAfterSound()
    {
        audioManager.instance.Playsounds();

        // 等待额外的一秒
        yield return new WaitForSeconds(1);

        // 异步加载场景
        SceneManager.LoadScene(startScene);
    }
    public void QuitGame()
    {
        audioManager.instance.Playsounds();
        Application.Quit();
        
        Debug.Log("Quitting Game");
    }
}
