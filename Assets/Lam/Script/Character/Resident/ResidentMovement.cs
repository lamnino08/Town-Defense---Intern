using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class ResidentMovement : MonoBehaviour, ICharacterDynamicMovement, ICharacterUnit
{
    protected NavMeshAgent _navMeshAgent;
    protected NavMeshObstacle _navMeshObstacle;
    protected IDynamicAnimator _animatorDynamic;
    [SerializeField] protected LayerMask _natureMask;
    [SerializeField] protected LayerMask _constructionMask;
    [SerializeField] protected ResidentWork _Work;
    protected Vector3 _destination;
    [SerializeField] protected Transform _natureTarget;
    [SerializeField] protected Transform _previousNatureTarget;
    [SerializeField] GameObject _selectCircle;
    protected ResidentAnimator _animator;
    public List<Transform> listTarget = new List<Transform>();
    protected bool isMoving;
    protected Transform OwnHome;
    protected bool isBackToHome = false;


    protected void Start() 
    {
        _animator = GetComponent<ResidentAnimator>();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();

        _Work = GetComponent<ResidentWork>();

        _selectCircle.SetActive(false);
    }

    protected void Update() 
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
                    if (_natureTarget == null)
                    {
                        listTarget.RemoveAt(0);
                        _natureTarget = listTarget[0];
                    } 
                    _navMeshAgent.SetDestination(_natureTarget.position);
                }
            }
        } else 
        {
            if (_natureTarget != null)
            {
                CheckDetectTarget();
            } else
            {
                _Work.StopManufacture();
            }
            

        }
    }

    protected abstract void CheckDetectTarget();

    protected abstract void DefineNatureArea();

    public void ChaseTarget()
    {

    }

    public void DoneTarget()
    {
        listTarget.Remove(_previousNatureTarget);
        _previousNatureTarget = null;

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
        _Work.StopManufacture();
        _navMeshAgent.SetDestination(_destination);
        _animator.Run();
        isBackToHome = false;

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