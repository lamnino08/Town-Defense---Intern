using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWork : MonoBehaviour
{
    [SerializeField] protected float _damage;
    protected IWorkerAnimator _animatorWorker;
    protected ICharacterAnimator _animatorCharacter;
    protected Coroutine isManufactureProcess;
    protected ArmyMovement _armyMovement;
    public void Manufacture(Transform nature)
    {
        if (isManufactureProcess == null)
        {
            isManufactureProcess = StartCoroutine(DoManufacture(nature));
        }
    }

    public void StopManufacture()
    {
        if (isManufactureProcess != null)
        {
            StopCoroutine(isManufactureProcess);
            isManufactureProcess = null;
        } 
    }

    protected  IEnumerator DoManufacture(Transform nature)
    { 
        NatureHealth health = nature.GetComponent<NatureHealth>();
        float time = health.TakeDamage(_damage);
        _animatorWorker.Work(true);
        yield return new WaitForSeconds(time);
        _animatorWorker.Work(false);
    }
}
