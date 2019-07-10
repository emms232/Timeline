using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]

public struct InputData 
{
    public float hMovement, vMovement, vMouse, hMouse;

    public bool dash, jump;

    public string Horizontal { get; private set; }

    public void getinput()
    {
        hMovement=Input.GetAxis("Horizontal");
        vMovement = Input.GetAxis("Vertical");

        vMouse = Input.GetAxis("Mouse Y");
        hMouse = Input.GetAxis("Mouse X");

        dash = Input.GetButton("Dash");
        jump = Input.GetButton("Jump");
    }

}

