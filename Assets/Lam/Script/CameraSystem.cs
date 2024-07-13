using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    public InputManagement inputManager;
    public float fixedHeight = 19f; 
    public float moveSpeed = 6f;
    public float moveSpeedIncrement = 0.1f;
    public float moveSpeedMin = 0.5f;
    public float moveSpeedMax = 5f;
    private float targetFieldOfView;
    bool _isShift = false;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private void Start()
    {
        Vector3 initialPosition = transform.position;
        initialPosition.y = fixedHeight;
        transform.position = initialPosition;

        targetFieldOfView = 50f; 
        cinemachineVirtualCamera.m_Lens.FieldOfView = targetFieldOfView;
    }

    private void Update()
    {
        inputManager.HandleAllInput();
        if (inputManager != null)
        {
            MoveCamera();
            // HandleCameraZoom_FieldOfView();
        }
    }
     private void MoveCamera()
    {
        float verticalInput = inputManager.verticalInput;
        float horizontalInput = inputManager.horizontalInput;
        // Vector2 inputVector = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(-verticalInput, 0, horizontalInput).normalized;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!_isShift)
            {
                moveSpeed = moveSpeed * 2.5f;
                _isShift = true;
            }
        } else
        {
            if (_isShift)
            {
                _isShift = false;
                moveSpeed = moveSpeed / 2.5f;
            }
        }
        Vector3 moveOffset = moveDirection * moveSpeed * Time.deltaTime;

        transform.position += moveOffset;
        transform.position = new Vector3(transform.position.x, fixedHeight, transform.position.z);
    }
}