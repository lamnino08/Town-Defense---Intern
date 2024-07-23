using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AArrowMovement : MonoBehaviour
{
    protected Transform _target;
    protected float _damage;
    [SerializeField] protected float speed; 
    [SerializeField] protected GameObject effect;
    [SerializeField] protected LayerMask enemyLayer;
    protected IAudioArmy _audioArmy;

    protected Vector3 _startPos;
    protected Vector3 _endPos;

    protected virtual void Start()
    {
        _startPos = transform.position;
        _audioArmy = GetComponent<IAudioArmy>();
        UpdateTargetInfo();
    }

    protected void Update()
    {
        if (_target != null)
        {
            // Debug.Log(this.gameObject.name +"   "+target.gameObject.name);
            UpdateTargetInfo();
            ArrowFlyCurve();
        }
        else
        {
            // Debug.Log("here");
            Destroy(gameObject);
        }
    }

    // public abstract void Debugs();
    public void UpdateTarget(float dame, Transform target)
    {
        // Debug.Log("Here");
        this._damage = dame;
        this._target = target;
    }

    protected void UpdateTargetInfo()
    {
        _endPos = _target.position + new Vector3(0, 0.25f, 0); 
    }

    protected abstract void ArrowFlyCurve();

    protected void OnTriggerEnter(Collider other)
    {
        if ((enemyLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            // Instantiate(effect,transform.position,Quaternion.identity);
            GameObject particleObject = Instantiate(effect, transform.position, Quaternion.identity);
            ParticleSystem particleSystem = particleObject.GetComponent<ParticleSystem>();
            particleSystem.Play();
            Destroy(particleObject, particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);

            IHealth enemyHealth = other.GetComponent<IHealth>();
            AudioSource audio = other.GetComponent<AudioSource>();
            if (enemyHealth != null )
            {
                enemyHealth.TakeDamage(_damage);
            }
            if (audio)
            {
                audio.PlayOneShot(AudioAssitance.Instance.GetClipByName("hitByAllow"));
          }
            Destroy(gameObject);
        }
    }
}