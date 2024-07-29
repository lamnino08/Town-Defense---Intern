using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
   private static ResourceUI _instance;
    public static ResourceUI instance {get => _instance;}

    private void Awake() 
    {
        if (_instance == null)
        {
            // Debug.Log(gameObject);
            _instance = this;
        }    else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private TMP_Text goldNumber;
    [SerializeField] private TMP_Text RockNumber;
    [SerializeField] private TMP_Text WoodNumber;

    public void UpdateResource(int wood, int rock, int gold)
    {
        // Debug.Log($"UpdateUI {gold}  {rock}  {wood}");
        goldNumber.text = gold.ToString();
        RockNumber.text = rock.ToString();
        WoodNumber.text = wood.ToString();
    }
}
