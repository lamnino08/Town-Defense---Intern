using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AttackArcher : Attack
{
    [SerializeField] protected GameObject arrowPrefab;
    [SerializeField] protected Transform bowTransform;
    protected override IEnumerator DoActtack(GameObject enemy)
    {
        while (enemy)
        {
            _animator.Attack();
            GameObject arrow = Instantiate(arrowPrefab, bowTransform.position, bowTransform.rotation * new Quaternion(0,-90,0,0));
            ArrowMovement arrowScript = arrow.GetComponent<ArrowMovement>();
            if (arrowScript)
            {
                arrowScript.damage = _damage;
                arrowScript.target = enemy.transform;
            }
            yield return new WaitForSeconds(_acttackSpeed);
        }

        StopActtack();
    }
}
