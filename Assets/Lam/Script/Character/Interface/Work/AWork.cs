using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWork : MonoBehaviour
{
    [SerializeField] protected float _damage;
    protected IWorkerAnimator _animatorWorker;
    protected ICharacterAnimator _animatorCharacter;
    protected Coroutine isManufactureProcess;
    protected ArmyMovement _armyMovement;

    private void Start() 
    {
        _animatorWorker = GetComponent<IWorkerAnimator>();
        _animatorCharacter = GetComponent<ICharacterAnimator>();
    }

    public void Manufacture(Transform nature)
    {
        if (isManufactureProcess == null)
        {
            isManufactureProcess = StartCoroutine(DoManufacture(nature));
        }
    }

    public virtual void StopManufacture(Transform nature)
    {
        if (isManufactureProcess != null)
        {
            StopCoroutine(isManufactureProcess);
            isManufactureProcess = null;
            _animatorWorker.Work(false);

            if (nature)
            {
                NatureHealth health = nature.GetComponent<NatureHealth>();
                health.StopTakeDamge();
            }
        }
    }

    protected virtual IEnumerator DoManufacture(Transform nature)
    { 
        NatureHealth health = nature.GetComponent<NatureHealth>();
        float time = health.TakeDamage(_damage);
        _animatorWorker.Work(true);
        yield return new WaitForSeconds(time);
        _animatorWorker.Work(false);
        isManufactureProcess = null;
        
    }
}
