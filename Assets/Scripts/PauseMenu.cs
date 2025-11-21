using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject pauseMenuPanel;

    private bool isPaused = false;

    private void Update()
    {
        // Toggle pause with Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }


    public void TogglePause()
    {
        if (isPaused) ResumeGame();
        else PauseGame();
    }
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // freezes the game
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f; // resumes the game
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // make sure timeScale is reset
        SceneManager.LoadScene("MainMenu"); // replace with your menu scene name
    }

    public void ExitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
