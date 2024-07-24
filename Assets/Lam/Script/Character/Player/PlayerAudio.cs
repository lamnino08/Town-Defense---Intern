using UnityEngine;

public class PlayerAudio : AudioArmy, IAudioMove
{
    [SerializeField] string _moveClipName;

    protected override void Start() 
    {
        base.Start();
        _moveClipName = "PlayerMove";  
        _attackClipName = "KnightAttackClip";  
    }

    public void Move(bool ismOve)
    {
        if (ismOve)
        {
            _audioSource.loop = true;
            _audioSource.clip = AudioAssitance.Instance.GetClipByName(_moveClipName);
            _audioSource.Play();
        } else
        {
            _audioSource.Stop();
        }
    }
}