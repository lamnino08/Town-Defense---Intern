namespace Trung.Scene
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CameraShop : MonoBehaviour
    {
        private static CameraShop _instance = null; public static CameraShop instance { get { return _instance; } }

        private Controls _inputs = null;

        [SerializeField] private Camera _camera = null;

        [SerializeField] private float _moveSpeed = 100;
        [SerializeField] private float _moveSmooth = 5;
        private bool _moving = false;


        [SerializeField] private float _zoomSpeed = 100;
        [SerializeField] private float _zoomSmooth = 5;
        public float _zoom = 5;
        private float _zoomMax = 25;
        private float _zoomMin = 1;

        private Transform _root = null;
        private Transform _pivot = null;
        private Transform _target = null;

        private Vector3 _center = Vector3.zero;

        private float _angle = 30;
        private float _right = 60;
        private float _left = 27;
        private float _up = 47;
        private float _down = 22;

        private bool _building = false; public bool isPlacingBuilding { get { return _building; } set { _building = value; } }
        public bool _movingBuilding = false;

        private void Awake()
        {
            _instance = this;
            _inputs = new Controls();
            _root = new GameObject("CameraHelper").transform;
            _pivot = new GameObject("CameraPivot").transform;
            _target = new GameObject("CameraTarget").transform;
            _camera.orthographic = true;
            _camera.nearClipPlane = 0;
            
        }



        // Start is called before the first frame update
        void Start()
        {
            Initialize(Vector3.zero, 60, 35, 47, 22, 90, 5, 1, 25);
        }

        public void Initialize(Vector3 center, float right, float left, float up, float down, float angle, float zoom, float zoomMin, float zoomMax)
        {
            _center = center;
            _right = right;
            _left = left;
            _up = up;
            _down = down;
            _angle = angle;
            _zoom = zoom;
            _zoomMin = zoomMin;
            _zoomMax = zoomMax;

            _camera.orthographicSize = _zoom;

            _moving = false;
            _pivot.SetParent(_root);
            _target.SetParent(_pivot);

            _root.position = _center;
            _root.localEulerAngles = Vector3.zero;

            _pivot.localPosition = Vector3.zero;
            _pivot.localEulerAngles = new Vector3(_angle, 0, 0);

            _target.localPosition = new Vector3(0, 0, -100);
            _target.localEulerAngles = Vector3.zero;
        }

        private void OnEnable()
        {
            _inputs.Enable();
            _inputs.Main.Move.started += _ => MoveStarted();
            _inputs.Main.Move.canceled += _ => MoveCanceled();
        }

        private void OnDisable()
        {
            _inputs.Main.Move.started -= _ => MoveStarted();
            _inputs.Main.Move.canceled -= _ => MoveCanceled();
            _inputs.Disable();
        }

        private void MoveStarted()
        {
            _moving = true;
        }

        private void MoveCanceled()
        {
            _moving = false;
        }


        // Update is called once per frame
        void Update()
        {
            float mouseScroll = _inputs.Main.MouseScroll.ReadValue<float>();
            if (mouseScroll > 0)
            {
                _zoom -= _zoomSpeed * Time.deltaTime;
            }
            else if (mouseScroll < 0)
            {
                _zoom += _zoomSpeed * Time.deltaTime;
            }

            CalculateMoveSpeed();

            if (_moving && !_movingBuilding)
            {
                Vector2 move = _inputs.Main.MoveDelta.ReadValue<Vector2>();
                if (move != Vector2.zero)
                {
                    move.x /= Screen.width;
                    move.y /= Screen.height;
                    _root.position -= _root.right.normalized * move.x * _moveSpeed;
                    _root.position -= _root.forward.normalized * move.y * _moveSpeed;
                }
            }

            AdjustBounds();

            if (_camera.orthographicSize != _zoom)
            {
                _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _zoom, _zoomSmooth * Time.deltaTime);
            }
            if (_camera.transform.position != _target.position)
            {
                _camera.transform.position = Vector3.Lerp(_camera.transform.position, _target.position, _moveSmooth * Time.deltaTime);
            }
            if (_camera.transform.rotation != _target.rotation)
            {
                _camera.transform.rotation = _target.rotation;
            }
        }

        private void AdjustBounds()
        {
            if (_zoom < _zoomMin)
            {
                _zoom = _zoomMin;
            }
            if (_zoom > _zoomMax)
            {
                _zoom = _zoomMax;
            }

            float h = PlaneOrthographicSize();
            float w = h * _camera.aspect;

            if (h > (_up + _down) / 2f)
            {
                float n = (_up + _down) / 2f;
                _zoom = n * Mathf.Sin(_angle * Mathf.Deg2Rad);
            }

            if (w > (_right + _left) / 2f)
            {
                float n = (_right + _left) / 2f;
                _zoom = n * Mathf.Sin(_angle * Mathf.Deg2Rad) / _camera.aspect;
            }

            h = PlaneOrthographicSize();
            w = h * _camera.aspect;

            Vector3 tr = _root.position + _root.right.normalized * w + _root.forward.normalized * h;
            Vector3 tl = _root.position - _root.right.normalized * w + _root.forward.normalized * h;
            Vector3 dr = _root.position + _root.right.normalized * w - _root.forward.normalized * h;
            Vector3 dl = _root.position - _root.right.normalized * w - _root.forward.normalized * h;

            if (tr.x > _center.x + _right)
            {
                _root.position += Vector3.left * Mathf.Abs(tr.x - (_center.x + _right));
            }
            if (tl.x < _center.x - _left)
            {
                _root.position += Vector3.right * Mathf.Abs((_center.x - _left) - tl.x);
            }

            if (tr.z > _center.z + _up)
            {
                _root.position += Vector3.back * Mathf.Abs(tr.z - (_center.z + _up));
            }
            if (dl.z < _center.z - _down)
            {
                _root.position += Vector3.forward * Mathf.Abs((_center.z - _down) - dl.z);
            }
        }

        private float PlaneOrthographicSize()
        {
            float h = _zoom * 2f;
            return h / Mathf.Sin(_angle * Mathf.Deg2Rad) / 2f;
        }


        private void CalculateMoveSpeed()
        {
            if (_zoom > 4)
            {
                _moveSpeed = 50;
            }
            else if (_zoom < 4 && _zoom > 1.5f)
            {
                _moveSpeed = 30;
            }
            else
            {
                _moveSpeed = 20;
            }
        }
    }
}