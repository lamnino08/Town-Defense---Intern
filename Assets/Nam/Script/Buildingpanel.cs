using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections.Generic;

public class Buildingpanel : MonoBehaviour
{
    private static Buildingpanel _instance;
    public static Buildingpanel instance {get => _instance;}

    
    public GameObject buildingPanels;
    [SerializeField] RectTransform BuildingpanelRect;
    [SerializeField] float LeftPosX, middlePosX;
    [SerializeField] float tweenDuration;
    private bool isOpen = false;

    [SerializeField] private List<CheckBtnBuilding> btns = new List<CheckBtnBuilding>();

    private void Awake() 
    {
        if (_instance == null)
        {
            _instance = this;
        }    else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        // You can add code here for updating the pause menu during gameplay if needed
    }
    public async void Open()
    {
        AudioAssitance.Instance.PlaySFX("Sound click mouse");
        if (isOpen == false)
        {
            buildingIntro();
            buildingPanels.SetActive(true);
            CheckInteracte();
        } else
        {
            await buildingOutro();
            buildingPanels.SetActive(false);
        }
        isOpen = !isOpen;

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
