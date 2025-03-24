using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioSource cut;

    public KeyCode left=KeyCode.A;
    public KeyCode right=KeyCode.D;

    public Animator leftAni;
    public Animator leftAni_1;
    public Animator rightAni;
    public Animator rightAni_1;
    void Update()
    {
        CutCheck(left, leftAni, leftAni_1);
        CutCheck(right, rightAni, rightAni_1);
    }

    void CutCheck(KeyCode keyCode,Animator animator,Animator animator1)
    {
        if (Input.GetKeyDown(keyCode))
        {
            animator.SetBool("Cut", true);
            animator1.SetBool("Cut", true);
            cut.Play();

        }
        if (Input.GetKeyUp(keyCode))
        {
            animator.SetBool("Cut", false);
            animator1.SetBool("Cut", false);
        }
    }
}
