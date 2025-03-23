using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public KeyCode left=KeyCode.A;
    public KeyCode right=KeyCode.D;
    void Update()
    {
        if (Input.GetKeyDown(left))
        {
            Debug.Log("左手切下！");
        }
        if (Input.GetKeyDown(right))
        {
            Debug.Log("右手切下！");
        }
    }
}
