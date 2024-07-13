using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBarLookCamera : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    private float lastColorChangeTime;

    
    private void Start() {
        mainCamera = Camera.main.transform;
    }

    private void Update() {
        transform.LookAt(transform.position + mainCamera.forward);

        if (Time.time > lastColorChangeTime + 5)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetTime()
    {
        lastColorChangeTime = Time.time;
    }
}

