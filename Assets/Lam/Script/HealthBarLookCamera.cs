using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarLookCamera : HealthBar
{
    protected override void Update() 
    {
        transform.LookAt(transform.position + mainCamera.forward);
    }
}
