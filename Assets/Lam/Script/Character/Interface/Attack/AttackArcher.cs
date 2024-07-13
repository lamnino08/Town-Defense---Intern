using System.Collections;
using UnityEngine;

public abstract class AttackArcher : Attack
{
    [SerializeField] protected GameObject arrowPrefab;
    [SerializeField] protected Transform bowTransform;
    protected override IEnumerator DoActtack(GameObject enemy)
    {
        yield return new WaitForSeconds(1);
        while (enemy)
        {
            _animator.Attack();
            GameObject arrow = Instantiate(arrowPrefab, bowTransform.position, bowTransform.rotation * new Quaternion(0,-90,0,0));
            AArrowMovement arrowScript = arrow.GetComponent<AArrowMovement>();
            if (arrowScript)
            {
                arrowScript.UpdateTarget(_damage, enemy.transform);
            }
            yield return new WaitForSeconds(_acttackSpeed);
        }

        StopActtack();
    }
}
