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
    [SerializeField] protected GameObject healthBarUI;
    [SerializeField] protected Slider healthBar;
    protected Animator animator;
    protected int _isDeadHash;
    // NavMeshObstacle _navMeshObsticle;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        _isDeadHash = Animator.StringToHash("isDead");
        // _navMeshObsticle = GetComponent<NavMeshObstacle>(); 

        _currentHealth =_initialHealth;
        healthBar.maxValue = _maxHealth;
        healthBar.value = _currentHealth;

        healthBarUI.SetActive(false);
    }

    public virtual void TakeDamage(float damage)
    {
        if (!isDead)
        {
            _currentHealth -= damage;
            UpdateHealthBar();

            if (_currentHealth <= 0)
            {
                // _navMeshObsticle.carving = true;
                isDead = true;
                Instantiate(ConstructionData.instance.buildingDestroyEffect, transform.position,Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public virtual void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBarUI.SetActive(true);
            healthBarUI.GetComponent<HealthBar>().SetTime();
            healthBar.value = _currentHealth;
        }
    }
}
