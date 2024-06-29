using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;

public abstract class StructureHealth : MonoBehaviour, IHealth
{
    [SerializeField] protected float _initialHealth;
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _currentHealth;
    protected bool isDead = false;
    [SerializeField] protected Slider healthBar;
    protected Animator animator;
    protected int _isDeadHash;
    NavMeshObstacle _navMeshObsticle;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        _isDeadHash = Animator.StringToHash("isDead");
        _navMeshObsticle = GetComponent<NavMeshObstacle>();

        _currentHealth =_initialHealth;
        healthBar.maxValue = _maxHealth;
        healthBar.value = _currentHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        if (!isDead)
        {
            _currentHealth -= damage;
            UpdateHealthBar();

            if (_currentHealth <= 0)
            {
                _navMeshObsticle.carving = true;
                isDead = true;
                animator.SetTrigger(_isDeadHash);
            }
        }
    }

    protected virtual void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = _currentHealth;
        }
    }
}
