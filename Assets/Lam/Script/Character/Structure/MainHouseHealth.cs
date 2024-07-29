using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHouseHealth : StructureHealth
{
    public override void TakeDamage(float damage)
    {
            _currentHealth -= damage;
            UpdateHealthBar();

            if (_currentHealth <= 0)
            {
                GameManager.instance.Lose();
                _currentHealth = _maxHealth;
                UpdateHealthBar();
            }
    }

    public void Reset()
    {
        _currentHealth = _maxHealth;
                UpdateHealthBar();
    }
   
}
