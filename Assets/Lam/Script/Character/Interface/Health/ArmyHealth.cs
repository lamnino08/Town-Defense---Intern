using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class ArmyHealth : MonoBehaviour, IHealth
{
    [SerializeField] protected float _initialHealth;
    [SerializeField] protected float _maxHealth;
    protected float _currentHealth;
    protected bool isDead = false;
    [SerializeField] protected GameObject healthBarUI;
    [SerializeField] protected Slider healthBar;
    protected ArmyAnimator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<ArmyAnimator>();
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
                isDead = true;
                animator.Dead();
            }
        }
        
    }

    protected virtual void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBarUI.SetActive(true);
            healthBarUI.GetComponent<HealBarLookCamera>().SetTime();
            healthBar.value = _currentHealth;
        }
    }
}
