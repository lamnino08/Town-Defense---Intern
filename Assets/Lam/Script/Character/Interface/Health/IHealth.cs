using UnityEngine;
using UnityEngine.UI;

public interface IHealth
{
    void TakeDamage(float damage);
    void UpdateHealthBar();
}
