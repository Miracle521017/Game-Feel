using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{ 
    public Text scoreText;
    
    public static UIController instance;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        
    }
    void Start()
    {
        
        //这里写判定逻辑
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
