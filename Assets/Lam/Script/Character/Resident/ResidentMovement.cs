using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ResidentMovement : MonoBehaviour, ICharacterDynamicMovement, ICharacterUnit
{
    protected NavMeshAgent _navMeshAgent;
    protected NavMeshObstacle _navMeshObstacle;
    protected IDynamicAnimator _animatorDynamic;
    [SerializeField] private LayerMask _natureMask;
    [SerializeField] private LayerMask _constructionMask;
    [SerializeField] private ResidentWork _Work;
    private Vector3 _destination;
    [SerializeField] private Transform _natureTarget;
    [SerializeField] GameObject _selectCircle;
    private ResidentAnimator _animator;
    public List<Transform> listTarget = new List<Transform>();
    private bool isMoving;
    private Transform OwnHome;
    bool isBackToHome = false;

    private void Start() 
    {
        _animator = GetComponent<ResidentAnimator>();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();

        _Work = GetComponent<ResidentWork>();

        _selectCircle.SetActive(false);
    }

    private void Update() 
    {
        if (isBackToHome)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, .1f, _constructionMask);

            foreach (var collider in hitColliders)
            {
                if (collider.transform == OwnHome)
                {
                    isBackToHome = false;
                    _animator.Run();
                    _navMeshAgent.SetDestination(_natureTarget.position);
                }
            }
        } else 
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f, _natureMask);

            foreach (var collider in hitColliders)
            {
                if (collider.transform == _natureTarget)
                {
                    _navMeshAgent.isStopped = true;
                    _animator.Idle();
                    _Work.Manufacture(_natureTarget);

                    if (listTarget.Count ==0)
                    {
                        DefineNatureArea();
                    }
                }
            }
        }
    }

    private void DefineNatureArea()
    {
        Collider[] additionalTargets = Physics.OverlapSphere(_natureTarget.position, 5f, _natureMask);
            
        foreach (var additionalCollider in additionalTargets)
        {
            if (!listTarget.Contains(additionalCollider.transform))
            {
                listTarget.Add(additionalCollider.transform);
            }
        }

        listTarget.Sort((a, b) => 
        Vector3.Distance(transform.position, a.position).CompareTo(
        Vector3.Distance(transform.position, b.position)));
    }

    public void ChaseTarget()
    {

    }

    public void DoneTarget(Transform oldTarget)
    {
        listTarget.Remove(oldTarget);

        if (listTarget.Count > 0)
        {
            _natureTarget = listTarget[0];
            _navMeshAgent.isStopped = false;
            _animator.Run();
            isBackToHome = true;
            _navMeshAgent.SetDestination(OwnHome.position);

        }
    }

    public void SetOrderPostion(Vector3 target)
    {
        _destination = new Vector3(target.x, 0, target.z);
        _navMeshAgent.SetDestination(_destination);
        _animator.Run();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo,  Mathf.Infinity, _natureMask))
        {
            _natureTarget = hitInfo.transform;
        }
    }

    public void SetSelect(bool isSelect)
    {
        if (!isSelect) 
        {
            _selectCircle.SetActive(isSelect);
            return;
        }
    }

    public void SetOwnHome(Transform home)
    {
        OwnHome = home;
    }
}