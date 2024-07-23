using UnityEngine;

public class ArcherAudio : AudioArmy
{
    protected override void Start()
    {
        base.Start();
        _attackClipName = "ArcherAttackClip";
    }
}