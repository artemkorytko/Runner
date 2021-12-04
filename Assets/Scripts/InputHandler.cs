using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputHandler : MonoBehaviour
{
    private float prevPosX = 0f;
    private bool isHold;
    private float relativeOffset;

    public float HorizontalAxis => -relativeOffset;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isHold = true;
            prevPosX = Input.mousePosition.x;
        }
        if (isHold)
        {
            float mousePosX = Input.mousePosition.x;
            float offset = prevPosX - mousePosX;
            relativeOffset = offset / Screen.width;
            prevPosX = mousePosX;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isHold = false;
            prevPosX = 0;
        }
    }
}

