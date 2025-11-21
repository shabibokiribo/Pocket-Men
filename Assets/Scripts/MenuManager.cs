using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject creditsPanel;

    private void Start()
    {
        ShowMainMenu();
    }

    // Show main menu
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    // Show credits panel
    public void ShowCredits()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    // Start game (load main scene)
    public void StartGame()
    {
        // Replace "GameScene" with the name of your scene
        SceneManager.LoadScene("Neighborhood");
    }

    // Exit application
    public void ExitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
