using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : ArmyHealth
{
    [SerializeField] PlayerUI _playerUI;
    protected override void Awake() 
    {
        base.Awake();
        _playerUI.StartDataHealth(_maxHealth);
    }

    protected override void Die()
    {
        GameManager.instance.Lose();
    }

    public override void UpdateHealthBar()
    {
        _playerUI.UpdateHealth(_currentHealth);
    }

    public void SetHealth()
    {
        _currentHealth = _maxHealth;
        UpdateHealthBar();
    }
    
}
