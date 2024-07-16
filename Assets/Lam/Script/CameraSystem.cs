using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    public Transform target;  // Transform mà bạn muốn camera theo dõi
    public float fixedHeight = 19f;
    public float followDistance = 10f;  // Khoảng cách giữa camera và mục tiêu
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    // private void Start()
    // {
    //     if (target != null)
    //     {
    //         SetUpCinemachine();
    //     }
    // }

    private void Update()
    {
        if (target != null)
        {
            FollowTarget();
        }
    }

    private void FollowTarget()
    {
        // Chỉ cập nhật vị trí của camera theo vị trí của mục tiêu
        Vector3 targetPosition = target.position;
        targetPosition.y = fixedHeight;
        transform.position = targetPosition + new Vector3(followDistance, 0, -followDistance);
    }
}
