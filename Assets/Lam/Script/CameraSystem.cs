using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float fixedHeight = 19f;
    [SerializeField] private float followDistance = 10f;
    [SerializeField] private float lenNormal = 5f;
    [SerializeField] private float lenBattle = 10f;
    [SerializeField] private float transitionDuration = 1f;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

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

    public void BattleCamera()
    {
        StartCoroutine(SmoothTransition(cinemachineVirtualCamera.m_Lens.OrthographicSize, lenBattle));
    }

    public void NormalCamera()
    {
        StartCoroutine(SmoothTransition(cinemachineVirtualCamera.m_Lens.OrthographicSize, lenNormal));
    }

    private IEnumerator SmoothTransition(float startSize, float endSize)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, endSize, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cinemachineVirtualCamera.m_Lens.OrthographicSize = endSize;
    }
}
