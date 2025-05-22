using System.Collections;

using System.Collections.Generic;

using UnityEngine;

public class NoteChecker : MonoBehaviour

{

    [Header("画面效果")]

    [SerializeField] private CameraShake cameraShake;

    public KeyCode targetKey = KeyCode.A;

    [SerializeField] private List<Collider2D> currentNotes = new List<Collider2D>();

    private void Update()

    {

        if (Input.GetKeyDown(targetKey))

        {

            cameraShake.Shake(0.1f, 0.2f); // 持续时间0.1秒，强度0.2

        }

        if (Input.GetKeyDown(targetKey) && currentNotes.Count > 0)

        {

            foreach (Collider2D noteCollider in currentNotes)

            {

                Note note = noteCollider.GetComponent<Note>();

                if (note != null)

                {

                    note.TriggerNote();

                }

            }

            currentNotes.Clear();

        }

        else if (Input.GetKeyDown(targetKey) && currentNotes.Count == 0)

        {

            // 新增：当按下按键但没有可触发的音符时，视为连击中断

            GameManager.Instance?.ResetMultiplier();

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