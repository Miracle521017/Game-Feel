using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewBehaviourScript : MonoBehaviour
{
    
    public string levelToLoad;
    public bool doneUI;
    private void Awake()
    {
      
    }

    // Start is called before the first frame update
    void Start()
    {
        doneUI = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            EndLevel();
           
            
        }
    }
    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo()
    {

        yield return new WaitForSeconds(1f);

        UIController1.instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController1.instance.fadeSpeed) + 1f);
        doneUI = true;
        if(doneUI){SceneManager.LoadScene(levelToLoad);}
    }
}
