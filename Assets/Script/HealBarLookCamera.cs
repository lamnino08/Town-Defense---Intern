using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBarLookCamera : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    private void Start() {
        mainCamera = Camera.main.transform;
    }

    private void Update() {
        transform.LookAt(transform.position + mainCamera.forward);
    }
}
