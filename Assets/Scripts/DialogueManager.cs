using UnityEngine;
using System;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public TMP_Text trainerNameText;

    private string[] lines;
    private int currentIndex;
    private Action onDialogueComplete;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    //Starting a dialogue sequence
    public void StartDialogue(string[] dialogueLines, Action onComplete = null, string trainerName = "")
    {
        if (dialogueLines.Length == 0) return;

        lines = dialogueLines;
        currentIndex = 0;
        onDialogueComplete = onComplete;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        if (trainerNameText != null)
            trainerNameText.text = trainerName;

        dialogueText.text = lines[currentIndex];
    }

    //Call to show the next line of dialogue
    public void NextLine()
    {
        currentIndex++;
        if (currentIndex < lines.Length)
        {
            dialogueText.text = lines[currentIndex];
        }
        else
        {
            // Dialogue finished
            dialoguePanel.SetActive(false);
            onDialogueComplete?.Invoke();
        }
    }

    private void Update()
    {
        if (dialoguePanel.activeSelf && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1)))
        {
            NextLine();
        }
    }

}
