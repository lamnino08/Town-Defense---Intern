using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Startguide: MonoBehaviour
{
    public void Playgame()
    {
        AudioAssitance.Instance.PlaySFX("Clickbutton ");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quit()
    {
        AudioAssitance.Instance.PlaySFX("Clickbutton ");
        UnityEngine.Debug.Log("quit!");
        UnityEngine.Application.Quit();
    }
}
