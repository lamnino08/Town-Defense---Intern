using System;
using UnityEngine;

public class BuildingAnimator : MonoBehaviour
{
        [SerializeField] ParticleSystem _pacticleEffect;
        private Animator _animator;
        private void Start() 
        {
                _animator = GetComponent<Animator>();
        }
        public void Created()
        {
                _animator.SetTrigger("create");
                _pacticleEffect.Play();
        }

}
