using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LSManager : MonoBehaviour
{
    public LSPlayer thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCo());
    }
    public IEnumerator LoadLevelCo()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(thePlayer.currentPoint.levelToLoad);

    }
}
