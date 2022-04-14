using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public bool ClockwiseRotation;

    private float rotationAngle;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            this.rotatePlayer(90f, ClockwiseRotation);
        }

    }

    void rotatePlayer(float degrees, bool clockwise)
    {
        if (clockwise)
        {
            rotationAngle -= degrees;
        }
        else
        {
            rotationAngle += degrees;
        }
        transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
    }
}
