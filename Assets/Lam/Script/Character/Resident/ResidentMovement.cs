using UnityEngine;
using UnityEngine.AI;

public class ResidentMovement : MonoBehaviour, ICharacterDynamicMovement, ICharacterUnit
{
    protected NavMeshAgent _navMeshAgent;
    protected NavMeshObstacle _navMeshObstacle;
    protected IDynamicAnimator _animatorDynamic;
    [SerializeField] private LayerMask _natureMask;
    [SerializeField] private ResidentWork _Work;
    private Vector3 _destination;
    [SerializeField] private Transform _natureTarget;
    [SerializeField] GameObject _selectCircle;
    private ResidentAnimator _animator;
    private bool isMoving;

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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f, _natureMask);

        foreach (var collider in hitColliders)
        {
            // Kiểm tra nếu collider là _natureTarget.
            if (collider.transform == _natureTarget)
            {
                _navMeshAgent.isStopped = true;
                _animator.Idle();
                _Work.Manufacture(_natureTarget);
            }
        }
    }

    public void ChaseTarget()
    {

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
}