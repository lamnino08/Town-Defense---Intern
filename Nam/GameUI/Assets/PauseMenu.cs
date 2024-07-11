using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;

    void Update()
    {
        // You can add code here for updating the pause menu during gameplay if needed
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game by setting timeScale to 0
    }

    public void PlayContinue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game by setting timeScale back to 1
    }
    public void Exit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
