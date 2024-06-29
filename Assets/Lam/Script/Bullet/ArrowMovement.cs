using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMovement : MonoBehaviour
{
    public Transform target;
    public float damage;
    public float speed = 20f; 
    public LayerMask enemyLayer;

    private Vector3 startPos;
    private Vector3 endPos;
    private float travelTime;
    private float startTime;
    private float maxHeight;

    void Start()
    {
        startPos = transform.position;
        UpdateTargetInfo();
        startTime = Time.time;
        maxHeight = 2f;
    }

    void Update()
    {
        if (target != null)
        {
            UpdateTargetInfo();
            ArrowFlyCurve();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void UpdateTargetInfo()
    {
        endPos = target.position + new Vector3(0, 0.25f, 0); 
        float distance = Vector3.Distance(startPos, endPos);
        travelTime = distance / speed;
    }

    private void ArrowFlyCurve()
    {
        float t = (Time.time - startTime) / travelTime;
        t = Mathf.Clamp01(t); 

         Vector3 previousPosition = transform.position;
        Vector3 flatPos = Vector3.Lerp(startPos, endPos, t);
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

    void OnTriggerEnter(Collider other)
    {
        if ((enemyLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            IHealth enemyHealth = other.GetComponent<IHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}