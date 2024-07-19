using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Buildingpanel : MonoBehaviour
{
    public GameObject buildingPanels;
    [SerializeField] RectTransform BuildingpanelRect;
    [SerializeField] float LeftPosX, middlePosX;
    [SerializeField] float tweenDuration;
    void Update()
    {
        // You can add code here for updating the pause menu during gameplay if needed
    }
    public void Open()
    {
        buildingIntro();
        buildingPanels.SetActive(true);
    }

    public async void Exit()
    {
       await buildingOutro();
       buildingPanels.SetActive(false);
        

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
