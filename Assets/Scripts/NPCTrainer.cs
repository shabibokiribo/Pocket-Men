using System;
using UnityEngine;

public class NPCTrainer : Interactable
{
    public SpriteRenderer spriteRenderer;
    public Sprite characterSprite;

    [Header("Trainer Info")]
    public string trainerName = "Trainer";
    public int difficulty = 1; // 1–10 scale
    public int trainerID;

    [Header("Pocket Men Team")]
    public PocketMan[] pocketMenTeam; // You’ll define PocketMan later

    [Header("Dialogue")]
    public string[] dialogue;

    private bool hasBeenDefeated = false;

    public override void Interact()
    {
        if (hasBeenDefeated)
        {
            DialogueManager.Instance.StartDialogue(
                new string[] { trainerName + ": You already beat me…" }
            );
            return;
        }

        DialogueManager.Instance.StartDialogue(
            dialogue,
            OnDialogueComplete,
            trainerName
        );
    }

    private void OnDialogueComplete()
    {
        StartBattle();

        SaveManager.Instance.currentSaveData.defeatedTrainers[trainerID] = true;
        SaveManager.Instance.SaveGame();
    }

    private void StartBattle()
    {
        Debug.Log("Starting battle with " + trainerName);

        // Replace this later with:
        // BattleManager.Instance.StartBattle(this);

        hasBeenDefeated = true;
    }

    private void Update()
    {
        // Only advance if dialogue panel is open
        if (DialogueManager.Instance.dialoguePanel.activeSelf &&
           (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1)))
        {
            DialogueManager.Instance.NextLine();
        }
    }

    void Start()
    {
        if (SaveManager.Instance.currentSaveData.defeatedTrainers[trainerID])
        {
            // disable this trainer since they're already beaten
            gameObject.SetActive(false);
        }

        if (spriteRenderer != null && characterSprite != null)
            spriteRenderer.sprite = characterSprite;
    }
}

