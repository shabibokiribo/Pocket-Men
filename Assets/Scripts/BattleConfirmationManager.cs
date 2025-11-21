using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleConfirmationManager : MonoBehaviour
{
    public static BattleConfirmationManager Instance;

    [Header("UI Elements")]
    public GameObject confirmationPanel;
    public Image trainerSpriteImage;
    public TMP_Text trainerNameText;
    public TMP_Text difficultyText;
    public TMP_Text statsText;
    public Button fightButton;
    public Button leaveButton;

    private NPCTrainer currentTrainer;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        confirmationPanel.SetActive(false);

        // Leave button works normally
        leaveButton.onClick.AddListener(ClosePanel);
    }

    public void ShowConfirmation(NPCTrainer npcTrainer)
    {
        currentTrainer = npcTrainer;

        trainerNameText.text = npcTrainer.trainerName;
        trainerSpriteImage.sprite = npcTrainer.GetComponent<SpriteRenderer>().sprite; // assuming sprite is on SpriteRenderer
        difficultyText.text = "Difficulty: " + npcTrainer.difficulty;
        statsText.text = "PocketMen: " + npcTrainer.generatedTeam.Count;

        // Remove previous listeners to prevent duplicates
        fightButton.onClick.RemoveAllListeners();

        // Add lambda to call OnFightConfirmed with the current trainer
        fightButton.onClick.AddListener(() => OnFightConfirmed(currentTrainer));
        // Keep leave functionality
        fightButton.onClick.AddListener(ClosePanel);

        confirmationPanel.SetActive(true);
    }

    private void OnFightConfirmed(NPCTrainer trainer)
    {
        // Assign the enemy team in GameManager
        GameManager.Instance.currentEnemyTeam = trainer.generatedTeam;

        // Automatically assign player's active PocketMan if null
        if (GameManager.Instance.currentPlayerPM == null)
        {
            if (GameManager.Instance.pocketMenInventory.Count > 0)
            {
                GameManager.Instance.currentPlayerPM = GameManager.Instance.pocketMenInventory[0];
                Debug.Log($"{GameManager.Instance.currentPlayerPM.firstName} is now your active PocketMan.");
            }
            else
            {
                Debug.LogError("Player has no PocketMan in inventory!");
                return;
            }
        }

        // Load the battle scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("BattleScene");
    }

    private void ClosePanel()
    {
        confirmationPanel.SetActive(false);
    }
}
