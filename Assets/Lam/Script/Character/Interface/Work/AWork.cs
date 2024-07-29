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

    public virtual void StopManufacture()
    {
        if (isManufactureProcess != null)
        {
            StopCoroutine(isManufactureProcess);
            isManufactureProcess = null;
        }
    }

    protected virtual IEnumerator DoManufacture(Transform nature)
    { 
        NatureHealth health = nature.GetComponent<NatureHealth>();
        while (true)
        {
            _animatorWorker.Work();
            if (health != null)
            {
                health.TakeDamage(_damage); 
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
