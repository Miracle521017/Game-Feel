using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteChecker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checker")
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(Clicked());
        }
    }

    IEnumerator Clicked()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
