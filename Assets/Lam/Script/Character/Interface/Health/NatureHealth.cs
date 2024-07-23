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
    protected Coroutine isAttackProcessSound;
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

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        healthBar.value = _currentHealth;
        healthBarUI.SetActive(true);


        AudioAssitance.Instance.PlaySFX("Sound cut tree");
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
            StopCoroutine(isAttackProcessSound);
        }
    }

}
