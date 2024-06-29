using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarEnable : MonoBehaviour
{
    private float lastColorChangeTime;

    private void Update() {
        if (gameObject.activeSelf == true && Time.time > lastColorChangeTime + 5)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetTime()
    {
        gameObject.SetActive(true);
        lastColorChangeTime = Time.time;
    }

    
}
