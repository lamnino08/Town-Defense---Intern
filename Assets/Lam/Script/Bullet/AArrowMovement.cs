using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AArrowMovement : MonoBehaviour
{
    public Transform target;
    public float damage;
    public float speed; 
    public LayerMask enemyLayer;

    protected Vector3 startPos;
    protected Vector3 endPos;

    protected virtual void Start()
    {
        startPos = transform.position;
        UpdateTargetInfo();
    }

    protected void Update()
    {
        if (target != null)
        {
            // Debug.Log(this.gameObject.name +"   "+target.gameObject.name);
            UpdateTargetInfo();
            ArrowFlyCurve();
        }
        else
        {
            // Debug.Log("here");
            Destroy(gameObject);
        }
    }

    // public abstract void Debugs();
    public void UpdateTarget(float dame, Transform target)
    {
        // Debug.Log("Here");
        this.damage = dame;
        this.target = target;
    }

    protected void UpdateTargetInfo()
    {
        endPos = target.position + new Vector3(0, 0.25f, 0); 
    }

    protected abstract void ArrowFlyCurve();

    protected void OnTriggerEnter(Collider other)
    {
        if ((enemyLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            IHealth enemyHealth = other.GetComponent<IHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
            // Debug.Log("Herer");
            Destroy(gameObject);
        }
    }
}