using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] protected Transform mainCamera;
    private float lastColorChangeTime;

    
    protected void Start() {
        mainCamera = Camera.main.transform;
    }

    protected virtual void Update() {
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

