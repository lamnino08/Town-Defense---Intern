using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;

public abstract class StructureHealth : MonoBehaviour, IHealth
{
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

        _currentHealth =_maxHealth;
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
                isDead = true;
                GameObject ds = Instantiate(ConstructionData.instance.buildingDestroyEffect, transform.position,Quaternion.identity);
                GameManager.instance.destroyEffect.Add(ds);
                GameManager.instance.RemoveBuilding(gameObject);
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
