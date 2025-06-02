using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgementZoneManager : MonoBehaviour
{
    public static JudgementZoneManager Instance;
    public JudgementZone[] judgementZones; // 8¸öÅÐ¶¨ÇøÓò

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
