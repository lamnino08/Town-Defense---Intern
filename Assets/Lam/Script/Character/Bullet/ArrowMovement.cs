using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : AArrowMovement
{
    private float travelTime;
    private float startTime;
    private float maxHeight;

    protected override void Start()
    {
        base.Start();
        startTime = Time.time;
        maxHeight = 2;
        // maxHeight = startPos.y + 1/((startPos.y - endPos.y) / 8) ;
    }

    protected override void ArrowFlyCurve()
    {
        float distance = Vector3.Distance(_startPos, _endPos);
        travelTime = distance/speed;
        float t = (Time.time - startTime) / travelTime;
        t = Mathf.Clamp01(t); 

         Vector3 previousPosition = transform.position;
        Vector3 flatPos = Vector3.Lerp(_startPos, _endPos, t);
        float height = maxHeight * Mathf.Sin(t * Mathf.PI); 
        Vector3 currentPos = new Vector3(flatPos.x, flatPos.y + height, flatPos.z);
        
        transform.position = currentPos;

        Vector3 direction = (currentPos - previousPosition).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion adjustedRotation = targetRotation * Quaternion.Euler(0, 90, 0); 
            transform.rotation = adjustedRotation;
        }
    }
}