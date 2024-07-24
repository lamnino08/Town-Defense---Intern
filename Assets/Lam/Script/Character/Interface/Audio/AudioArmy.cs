using UnityEngine;

public abstract class AudioArmy : AAudio, IAudioArmy
{
    [SerializeField] protected string _attackClipName;
    public virtual void Attack()
    {
        _audioSource.PlayOneShot(AudioAssitance.Instance.GetClipByName(_attackClipName));
    }
}