using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private KeyCode[] trackKeys = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F };

    private void Awake()
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

    public bool IsTrackPressed(int trackID)
    {
        if (trackID >= 0 && trackID < trackKeys.Length)
        {
            return Input.GetKey(trackKeys[trackID]);
        }
        return false;
    }
}
