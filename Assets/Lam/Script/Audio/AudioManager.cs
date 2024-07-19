using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager instance => _instance;

    private void Start() {
        _instance = this;
    }

    public AudioClip bowAttack;
}
