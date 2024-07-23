using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class AAudio : MonoBehaviour
{
    protected AudioSource _audioSource;

    protected virtual void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
}