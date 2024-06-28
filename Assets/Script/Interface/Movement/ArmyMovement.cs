using UnityEngine;
using UnityEditor; 

public abstract class ArmyMovement : MonoBehaviour, IArmyMovement, ICharacterMovement
{
    [SerializeField] protected float rangeFindEnemy;
    [SerializeField] protected LayerMask layerMaskOfEnemy;
    [SerializeField]
    protected Transform target;    
    protected IArmyAnimator _animatorArmy;
    protected ICharacterAnimator _animatorCharacter;
    protected Attack _attack;
    protected bool isHadEnemy = false;
    

    protected virtual void Start() 
    {
        _animatorArmy = GetComponent<ArmyAnimator>();
        _animatorCharacter = GetComponent<ArmyAnimator>();
        _attack = GetComponent<Attack>();
    }   

    public virtual void DefineEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, rangeFindEnemy, layerMaskOfEnemy);
        if (colliders.Length == 0)
        {
            if (isHadEnemy)
            {
                isHadEnemy = false;
            }
            return;
        }

        Collider closestCollider = null;
        float closestDistance = float.MaxValue;

        foreach (Collider collider in colliders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCollider = collider;
            }
        }

        if (closestCollider != null)
        {
            target = closestCollider.transform;
            isHadEnemy = true;
        }
    }

    public abstract void DirectToTarget();

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeFindEnemy);

        if (target != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, target.position);

            float distanceToTarget = Vector3.Distance(transform.position, target.position);
            Vector3 midPoint = (transform.position + target.position) / 2;
            
            Handles.Label(midPoint, distanceToTarget.ToString("F2") + " units");
        }
    }
    #endif

}
