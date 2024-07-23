using UnityEngine;

public class AllowAudio : AudioArmy
{
    protected override void Start()
    {
        base.Start();
        _attackClipName = "AllowAudioClip";
    }
}