using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class NavMesh : MonoBehaviour
{
    [SerializeField] private Transform Target;
    protected  NavMeshAgent navMeshAgent;

    protected  void Awake() 
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected  void Start() {
        navMeshAgent.destination = Target.position;
    }
}
