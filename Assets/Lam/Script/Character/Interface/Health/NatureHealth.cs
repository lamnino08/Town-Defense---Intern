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
    protected Coroutine isAttackProcess;
    protected int _isDeadHash;
    protected Nature _nature;
    // NavMeshObstacle _navMeshObsticle;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        _isDeadHash = Animator.StringToHash("isDead");
        _currentHealth = _maxHealth;
        _nature = GetComponent<Nature>();
        
        healthBar.maxValue = _maxHealth;
        healthBar.value = _currentHealth;
        healthBarUI.SetActive(false);

    }

    public float TakeDamage(float _damage)
    {
        float timeToManufacture = _maxHealth/_damage;
        isAttackProcess = StartCoroutine(ProcessManufacture(timeToManufacture));
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
            _nature.Manufacture();
            TilemapManager.instance.RemoveTileNatureInPos(Vector3Int.RoundToInt(transform.position));
            Destroy(gameObject);
        }
    }

    public void StopTakeDamge()
    {
        if (isAttackProcess != null)
        {
            healthBarUI.SetActive(false);
            StopCoroutine(isAttackProcess);
        }
    }

}
