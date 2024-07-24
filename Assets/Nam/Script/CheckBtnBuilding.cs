using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBtnBuilding : MonoBehaviour
{
   [SerializeField] private int id;
   private Button btn;

   private void Start() 
   {
        btn = GetComponent<Button>();
   }

   public void CheckRecoureToInteract()
   {
        ObjectData data = ConstructionData.instance.GetObjectDataById(id);
        btn.interactable = data.costToBuild.Check();
   }

   public void Place()
   {
        PlacementSystem.instance.StartPlaceBuilding(id);
   }
}
