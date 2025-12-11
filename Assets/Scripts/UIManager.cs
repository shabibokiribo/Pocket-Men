using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Dialogue UI")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public TMP_Text nameText;

    private string[] currentLines;
    private int currentIndex;
    private Action onDialogueComplete;

    [Header("PocketMan Popup UI")]
    public GameObject pocketManPopup; // Drag your popup panel here
    public Image pmImage;
    public TMP_Text pmNameText;
    public TMP_Text pmStatsText;
    public Button keepButton;
    public Button discardButton;

    private Action<bool, PMInst> onPocketManDecision;
    private PMInst currentPM;

    [Header("Inventory Popup UI")]
    public GameObject inventoryPopUp;

    private void Awake()
    {
        // Setting up Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Hide dialogue panel on start
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        // Hide PocketMan popup on start
        if (pocketManPopup != null)
            pocketManPopup.SetActive(false);

        // Setup buttons if assigned
        if (keepButton != null)
            keepButton.onClick.AddListener(() => MakePocketManDecision(true));
        if (discardButton != null)
            discardButton.onClick.AddListener(() => MakePocketManDecision(false));
    }

    #region Dialogue Methods

    public void ShowDialogue(string[] lines, Action onComplete = null)
    {
        if (lines.Length == 0) return;

        dialoguePanel.SetActive(true);
        currentLines = lines;
        currentIndex = 0;
        onDialogueComplete = onComplete;

        // Display the first line
        dialogueText.text = currentLines[currentIndex];
    }

    public void NextDialogueLine()
    {
        currentIndex++;
        if (currentIndex < currentLines.Length)
        {
            dialogueText.text = currentLines[currentIndex];
        }
        else
        {
            dialoguePanel.SetActive(false);
            onDialogueComplete?.Invoke();
        }
    }

    #endregion

    #region PocketMan Methods

    /// <summary>
    /// Shows the PocketMan popup with Keep/Discard options
    /// </summary>
    public void ShowPocketManPopup(PMInst pm, Action<bool, PMInst> callback)
    {
        currentPM = pm;
        onPocketManDecision = callback;

        if (pmImage != null) pmImage.sprite = pm.sprite;
        if (pmNameText != null) pmNameText.text = $"{pm.firstName} {pm.lastName}";
        if (pmStatsText != null) pmStatsText.text = $"Level: {pm.level}\nHP: {pm.health}\nATK: {pm.attack}\nDEF: {pm.defense}";

        if (pocketManPopup != null) pocketManPopup.SetActive(true);
    }

    private void MakePocketManDecision(bool keep)
    {
        if (pocketManPopup != null) pocketManPopup.SetActive(false);
        onPocketManDecision?.Invoke(keep, currentPM);
    }

    #endregion

    public void OnClickInv()
    {
        inventoryPopUp.SetActive(true );
    }

    public void OnClickInvExit()
    {
        inventoryPopUp.SetActive(false );
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
