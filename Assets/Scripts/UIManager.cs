using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Dialogue UI")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public TMP_Text nameText;

    private string[] currentLines;
    private int currentIndex;
    private System.Action onDialogueComplete;

    private void Awake()
    {
        //setting up Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //Hide dialogue panel on start
        if(dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    #region Dialogue Methods

    public void ShowDialogue(string[] lines, System.Action onComplete = null)
    {
        if (lines.Length == 0) return;

        dialoguePanel.SetActive(true);
        currentLines = lines;
        currentIndex = 0;
        onDialogueComplete = onComplete;

        //display the first line
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
            dialoguePanel.SetActive(false) ;
            onDialogueComplete?.Invoke();
        }
    }

    #endregion

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
