using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Interactionbuilding : MonoBehaviour
{
    public GameObject InteractionPanel;
    [SerializeField] RectTransform ITTRect;
    [SerializeField] float BottomPosY, middlePosY;
    [SerializeField] float tweenDuration;
    void Update()
    {
        // You can add code here for updating the pause menu during gameplay if needed
    }
    public void Open()
    {
        buildingIntro();
        InteractionPanel.SetActive(true);
    }

    public async void Exit()
    {
        await buildingOutro();
        InteractionPanel.SetActive(false);


    }

    void buildingIntro()
    {
        ITTRect.DOAnchorPosY(middlePosY, tweenDuration).SetUpdate(true);

    }
    async Task buildingOutro()
    {
        await ITTRect.DOAnchorPosY(BottomPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }
}
