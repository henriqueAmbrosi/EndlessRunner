using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Swipe_ : MonoBehaviour
{

    Vector2 initialPos;
    Vector2 releasePos;
    Touch primaryTouch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public UnityEvent SwipeUp;
    public UnityEvent SwipeDown;
    public UnityEvent SwipeLeft;
    public UnityEvent SwipeRight;

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            primaryTouch = Input.GetTouch(0);
            if(primaryTouch.phase == TouchPhase.Began)
            {
                initialPos = primaryTouch.position;
            }
            if(primaryTouch.phase == TouchPhase.Ended)
            {
                releasePos = primaryTouch.position;
                Vector2 direction = releasePos - initialPos;
                direction = direction.normalized;
                direction = new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y));
                CheckDirection(direction);
            }
        }
    }

    void CheckDirection(Vector2 direction)
    {
        if(direction == Vector2.up)
        {
            SwipeUp.Invoke();
        }
        if(direction == Vector2.down)
        {
            SwipeDown.Invoke();

        }

        if (direction == Vector2.left) {
            SwipeLeft.Invoke();

        }
        if (direction == Vector2.right) {
            SwipeRight.Invoke();

        }
    }
}
