using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
//    private static ResourceUI _instance;
//     public static ResourceUI instance {get => _instance;}

//     private void Awake() 
//     {
//         if (_instance == null)
//         {
//             // Debug.Log(gameObject);
//             _instance = this;
//         }    else
//         {
//             Destroy(gameObject);
//         }
//     }

    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider ManaBar;

    public void StartDataHealth(float health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void UpdateHealth(float health)
    {
        healthBar.value = health;
    }

    public void StartDataMana(float mana)
    {
        ManaBar.maxValue = mana;
        ManaBar.value = mana;
    }
}
