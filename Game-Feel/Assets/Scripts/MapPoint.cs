using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public string levelToLoad,levelToCheck;
    public bool islocked;
    public MapPoint next, last;
    
    // Start is called before the first frame update
    void Start()
    {
        if (levelToLoad != null)
        {
            islocked = true;
            if (levelToCheck != null)
            {
                if (PlayerPrefs.HasKey(levelToCheck + "_unlocked"))
                {
                    if (PlayerPrefs.GetInt(levelToCheck + "_unlocked") == 1)
                    {
                        islocked = false;
                    }
                    else
                    {
                        islocked = true;
                    }
                }
                if(levelToCheck==levelToLoad)
                {
                    islocked = false;
                }
            }
            
            
            
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
