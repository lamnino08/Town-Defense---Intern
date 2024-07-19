using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    [SerializeField] RectTransform pausePanelRect;
    [SerializeField] float TopPosY, middlePosY;
    [SerializeField] float tweenDuration;
    void Update()
    {
        // You can add code here for updating the pause menu during gameplay if needed
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game by setting timeScale to 0
        PausepanelIntro();
    }

    public async void PlayContinue()
    {
        await PausepanelOutro();
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game by setting timeScale back to 1
       
    }
    public void Exit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    void PausepanelIntro()
    {
        pausePanelRect.DOAnchorPosY(middlePosY, tweenDuration).SetUpdate(true);

    }
    async Task PausepanelOutro()
    {
        await pausePanelRect.DOAnchorPosY(TopPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }
}
