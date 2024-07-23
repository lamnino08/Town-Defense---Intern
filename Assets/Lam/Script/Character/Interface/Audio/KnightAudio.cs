using UnityEngine;

public class KnightAudio : AudioArmy
{
    protected override void Start()
    {
        base.Start();
        _attackClipName = "KnightAttackClip";
    }
}