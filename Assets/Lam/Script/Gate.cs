using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private LayerMask _leagueMask;
    [SerializeField] private float _rangeDetect;
    [SerializeField] private Animator _animaton;
    private bool _isOpen;
    private int _openHash;
    private int _closeHash;

    private void Start() 
    {
        _animaton = transform.GetChild(0).GetComponent<Animator>();
        _openHash = Animator.StringToHash("open");
        _closeHash = Animator.StringToHash("close");
    }

    private void Update() 
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _rangeDetect, _leagueMask);
        if (colliders.Length > 0)
        {
            if (!_isOpen)
            {
                _animaton.SetTrigger(_openHash);
                _isOpen = true;
            }
        } 
        else
        {
            if (_isOpen)
            {
                _animaton.SetTrigger(_closeHash);
                _isOpen = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Vẽ hình cầu để biểu thị vùng phát hiện
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _rangeDetect);
    }
}
