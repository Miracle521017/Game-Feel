using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteChecker : MonoBehaviour
{
    public KeyCode targetKey = KeyCode.A;
    [SerializeField]private List<Collider2D> currentNotes = new List<Collider2D>();

    private void Update()
    {
        // 持续检测按键输入
        if (Input.GetKeyDown(targetKey) && currentNotes.Count > 0)
        {
            Debug.Log("音符在触发区域内时按下了 " + targetKey);
            // 这里可以添加触发后的处理逻辑
           currentNotes.Clear(); // 如果需要清除已触发的音符
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            currentNotes.Add(collision);
           // Debug.Log("音符进入触发区域");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            currentNotes.Remove(collision);
           // Debug.Log("音符离开触发区域");
        }
    }
}
