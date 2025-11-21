using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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

        fightButton.onClick.AddListener(OnFightConfirmed);
        leaveButton.onClick.AddListener(ClosePanel);
    }

    public void ShowConfirmation(NPCTrainer npcTrainer)
    {
        currentTrainer = npcTrainer;

        trainerNameText.text = npcTrainer.trainerName;
        trainerSpriteImage.sprite = npcTrainer.GetComponent<SpriteRenderer>().sprite; // assuming sprite is on SpriteRenderer
        difficultyText.text = "Difficulty: " + npcTrainer.difficulty;
        statsText.text = "PocketMen: " + npcTrainer.generatedTeam.Count;

        confirmationPanel.SetActive(true);
    }

    public void OnFightConfirmed()
    {
        // Assuming you stored the current NPCTrainer being confirmed:
        BattleManager.Instance.StartBattle(
            GameManager.Instance.pocketMenInventory[0], // or whichever PMInst is active
            currentTrainer.generatedTeam[0]             // first PocketMan of the trainer
        );

        // Close the confirmation menu
        confirmationPanel.SetActive(false);
    }

    private void ClosePanel()
    {
        confirmationPanel.SetActive(false);
    }
}
