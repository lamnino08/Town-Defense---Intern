using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioHit : MonoBehaviour
{
    protected AudioSource _audioSource;

    protected virtual void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void HitSound()
    {

    }
}