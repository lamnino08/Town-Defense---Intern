using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerl : MonoBehaviour
{
    private static AudioManagerl _instance;
    public static AudioManagerl instance => _instance;

    private void Start() {
        _instance = this;
    }

    public AudioClip bowAttack;
}
