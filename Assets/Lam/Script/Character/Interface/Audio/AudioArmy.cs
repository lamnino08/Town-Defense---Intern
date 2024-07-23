using UnityEngine;

public abstract class AudioArmy : AAudio, IAudioArmy
{
    [SerializeField] protected string _attackClipName;
    public virtual void Attack()
    {
        Debug.Log("heree");
        _audioSource.PlayOneShot(AudioAssitance.Instance.GetClipByName(_attackClipName));
    }
}