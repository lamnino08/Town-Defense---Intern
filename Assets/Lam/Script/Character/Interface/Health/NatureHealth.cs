using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public abstract class NatureHealth : MonoBehaviour
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

        healthBarUI.SetActive(false);
    }

    public float TakeDamage(float _damage)
    {
        float timeToManufacture = _maxHealth/_damage;
        StartCoroutine(ProcessManufacture(timeToManufacture));
        return timeToManufacture;
    }

    private IEnumerator ProcessManufacture(float time)
    {
        healthBarUI.SetActive(true);

        float elapsedTime = 0f;
        float startingHealth = _currentHealth;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            _currentHealth = Mathf.Lerp(startingHealth, 0, elapsedTime / time);
            healthBar.value = _currentHealth;
            yield return null;
        }

        if (_currentHealth <= 0)
        {
            // Phá hủy đối tượng sau khi mất hết máu
            Destroy(gameObject);
        }
    }

}
