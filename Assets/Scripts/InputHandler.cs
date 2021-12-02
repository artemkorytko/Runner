using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private float prevPosX = 0;
    private bool isHold;
    private float realOffset = 0;

    public float HorizontalAxis => realOffset;
    public bool IsHold => isHold;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isHold = true;
            prevPosX = Input.mousePosition.x;
        }

        if (isHold)
        {
            float newPos = Input.mousePosition.x;
            float offset = prevPosX - newPos;
            realOffset = offset / Screen.width;
            prevPosX = newPos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isHold = false;
            prevPosX = 0;
        }
    }
}
