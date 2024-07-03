using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AttackKnight : Attack
{
    protected ParticleSystem effect;

    protected override void Start() 
    {
        base.Start();
        effect = GetComponentInChildren<ParticleSystem>();
    }
    protected override IEnumerator DoActtack(GameObject enemy)
    {
        while (true)
        {
            IHealth health = _armyMovement.target.GetComponent<IHealth>();
            _animator.Attack();
            effect.Play();
            if (health != null)
            {
                health.TakeDamage(_damage); 
            }
            yield return new WaitForSeconds(_acttackSpeed);
        }
    }
}
