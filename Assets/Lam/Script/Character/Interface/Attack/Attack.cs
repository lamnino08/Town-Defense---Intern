using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] protected float _damage;
    [SerializeField] protected float _acttackSpeed;
    protected ArmyAnimator _animator;
    protected Coroutine isAttackProcess;
    protected ParticleSystem AttackEffect;
    protected ArmyMovement _armyMovement;

    protected virtual void Start() {
        _animator = GetComponent<ArmyAnimator>();
        AttackEffect = GetComponentInChildren<ParticleSystem>();
        _armyMovement = GetComponent<ArmyMovement>();
    }

    public virtual void Acttack(GameObject enemy)
    {
        if (isAttackProcess == null)
        {
            isAttackProcess = StartCoroutine(DoActtack(enemy));
        }
    }

    public virtual void StopActtack()
    {
        if (isAttackProcess != null)
        {
            StopCoroutine(isAttackProcess);
            isAttackProcess = null;
        } 
    }

    protected virtual IEnumerator DoActtack(GameObject enemy)
    { 
        // yield return new WaitForSeconds(1);
        IHealth health = enemy.GetComponent<IHealth>();
        while (true)
        {
            _animator.Attack();
            if (health != null)
            {
                health.TakeDamage(_damage); 
            }
            yield return new WaitForSeconds(_acttackSpeed);
        }
    }
}
