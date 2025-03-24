using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSPlayer : MonoBehaviour
{
    public MapPoint currentPoint;
    private bool hasMoved = false;
    private bool leveiLoading;
    public float moveSpeed=10f;

    public LSManager theManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!leveiLoading)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position,
                moveSpeed * Time.deltaTime);
            if ((Input.GetAxisRaw("Horizontal") > .5f )&& !hasMoved)
            {
                if (currentPoint.next != null)
                {
                    SetNextPoint(currentPoint.next);
                    hasMoved = true;
                }
            }
       
            if ((Input.GetAxisRaw("Horizontal") < -.5f)&& !hasMoved)
            {
                if (currentPoint.last != null)
                {
                    SetNextPoint(currentPoint.last);
                    hasMoved = true;
                }
            }
        
            if (Input.GetAxisRaw("Horizontal") ==0)
            {
                hasMoved = false;
            }

            if (Input.GetButtonDown("Jump")&&!currentPoint.islocked)
            {
                leveiLoading = true;
                theManager.LoadLevel();
            }
        }
        
        
    }

    public void SetNextPoint(MapPoint nextpoint)
    {
        currentPoint = nextpoint;
    }
}
