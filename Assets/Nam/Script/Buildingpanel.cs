using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections.Generic;

public class Buildingpanel : MonoBehaviour
{
    public GameObject buildingPanels;
    [SerializeField] RectTransform BuildingpanelRect;
    [SerializeField] float LeftPosX, middlePosX;
    [SerializeField] float tweenDuration;

    [SerializeField] private List<CheckBtnBuilding> btns = new List<CheckBtnBuilding>();
    void Update()
    {
        // You can add code here for updating the pause menu during gameplay if needed
    }
    public void Open()
    {
        buildingIntro();
        buildingPanels.SetActive(true);
        CheckInteracte();
        AudioAssitance.Instance.PlaySFX("Sound click mouse");
    }

    private void CheckInteracte()
    {
        foreach(CheckBtnBuilding e in btns)
        {
            e.CheckRecoureToInteract();
        }
    }

    public async void Exit()
    {
       await buildingOutro();
       buildingPanels.SetActive(false);
       AudioAssitance.Instance.PlaySFX("Sound click mouse");
    }
    
    void buildingIntro()
    {
        BuildingpanelRect.DOAnchorPosX(middlePosX, tweenDuration).SetUpdate(true);

    }
    async Task buildingOutro()
    {
      await  BuildingpanelRect.DOAnchorPosX(LeftPosX, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }
}
