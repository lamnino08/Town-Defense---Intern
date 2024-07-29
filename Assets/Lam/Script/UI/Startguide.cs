using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;
using UnityEngine;


public class Startguide: MonoBehaviour
{
    [SerializeField] private TransitionEffect _fadeEffect;
    public void Playgame()
    {
        AudioAssitance.Instance.PlaySFX("Clickbutton ");
        _fadeEffect.FadeIn();
    }
    public void Quit()
    {
        AudioAssitance.Instance.PlaySFX("Clickbutton ");
        UnityEngine.Debug.Log("quit!");
        UnityEngine.Application.Quit();
    }
}
