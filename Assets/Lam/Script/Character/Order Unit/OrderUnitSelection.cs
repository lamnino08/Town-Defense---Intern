using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;

public class OrderUnitSelection : MonoBehaviour
{
    public List<GameObject> listSelection = new List<GameObject>();

    private static OrderUnitSelection _instance;
    public static OrderUnitSelection Instance {get {return _instance;}}

    private void Awake() 
    {
        if (_instance == null)
        {
            _instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    public void ClickSelection(GameObject objectAdd)
    {
        ICharacterUnit script = objectAdd.GetComponent<ICharacterUnit>();
        if (script != null)
        {
                Debug.Log("here");
            if (listSelection.Contains(objectAdd))
            {
                script.SetSelect(false);
                listSelection.Remove(objectAdd);
            } else
            {
                script.SetSelect(true);
                listSelection.Add(objectAdd);
            }
        }
        
    }

    public void DeSelectAll()
    {
        foreach(GameObject leagueObj in listSelection)
        {
            ArmyLeagueDynamicMovement script = leagueObj.GetComponent<ArmyLeagueDynamicMovement>();
            script.SetSelect(false);
        }
        listSelection.Clear();
    }
        
}
