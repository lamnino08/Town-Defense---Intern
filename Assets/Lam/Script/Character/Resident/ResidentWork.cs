using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidentWork : AWork
{
    public override void StopManufacture()
    {
        if (isManufactureProcess != null)
        {
            StopCoroutine(isManufactureProcess);
            isManufactureProcess = null;
            ResidentMovement movement = GetComponent<ResidentMovement>();
            movement.DoneTarget();
            _animatorWorker.Work(false);
        }
    }
}
