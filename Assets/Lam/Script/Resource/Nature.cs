using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Nature : MonoBehaviour
{
    [SerializeField] protected Resource[] resources;

    public virtual void Manufacture()
    {
        foreach (var resource in resources)
        {
            resource.Manufacture();
        }
    }
}
