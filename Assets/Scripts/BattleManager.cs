using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    [Header("Player")]
    public PMInst playerPM;
    public Image playerSprite;
    public Slider playerHealthBar;
    public TMP_Text playerNameText;

    [Header("Enemy")]
    public PMInst enemyPM;
    public Image enemySprite;
    public Slider enemyHealthBar;
    public TMP_Text enemyNameText;

    [Header("UI")]
    public TMP_Text dialogueText;
    public GameObject moveButtonsPanel;
    public Button moveButtonPrefab;

    private bool playerTurn = true;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Only runs if StartBattle was not used
    private void Start()
    {
        if (playerPM != null && enemyPM != null)
            return;

        Debug.LogWarning("BattleManager loaded without StartBattle. Attempting fallback setup.");

        playerPM = GameManager.Instance.GetActivePlayerPM();
        GameManager.Instance.EnsureMoves(playerPM);

        if (GameManager.Instance.currentEnemyTeam != null &&
            GameManager.Instance.currentEnemyTeam.Count > 0)
        {
            GameManager.Instance.EnsureMovesForTeam(GameManager.Instance.currentEnemyTeam);
            enemyPM = GameManager.Instance.currentEnemyTeam[0];

            SetupBattle();
        }
        else
        {
            Debug.LogError("BattleManager has no enemy team and StartBattle was not called.");
        }
    }

    // This is the correct way to start a battle
    public void StartBattle(PMInst player, PMInst enemy)
    {
        playerPM = player;
        enemyPM = enemy;

        GameManager.Instance.EnsureMoves(playerPM);
        GameManager.Instance.EnsureMoves(enemyPM);

        SetupBattle();
    }

    private void SetupBattle()
    {
        if (playerPM == null || enemyPM == null)
        {
            Debug.LogError("SetupBattle called without valid PocketMen.");
            return;
        }

        // Player UI setup
        playerSprite.sprite = playerPM.sprite;
        playerNameText.text = playerPM.firstName + " Lvl " + playerPM.level;
        playerPM.maxHealthStat = playerPM.health;
        playerHealthBar.maxValue = playerPM.maxHealthStat;
        playerHealthBar.value = playerPM.health;

        // Enemy UI setup
        enemySprite.sprite = enemyPM.sprite;
        enemyNameText.text = enemyPM.firstName + " Lvl " + enemyPM.level;
        enemyPM.maxHealthStat = enemyPM.health;
        enemyHealthBar.maxValue = enemyPM.maxHealthStat;
        enemyHealthBar.value = enemyPM.health;

        dialogueText.text = "A battle began.";
        playerTurn = true;

        SetupMoveButtons();
    }

    private void SetupMoveButtons()
    {
        // Clear old buttons
        foreach (Transform child in moveButtonsPanel.transform)
            Destroy(child.gameObject);

        if (playerPM.moves == null || playerPM.moves.Length == 0)
        {
            Debug.LogError("Player moves missing during button setup.");
            dialogueText.text = "ERROR: Player has no moves.";
            return;
        }

        foreach (string move in playerPM.moves)
        {
            string capturedMove = move;
            Button btn = Instantiate(moveButtonPrefab, moveButtonsPanel.transform);

            TMP_Text txt = btn.GetComponentInChildren<TMP_Text>();
            if (txt != null)
                txt.text = capturedMove;

            btn.onClick.AddListener(() =>
            {
                if (playerTurn)
                {
                    DisableMoveButtons();
                    OnPlayerMove(capturedMove);
                }
            });
        }

        EnableMoveButtons();
    }

    private void EnableMoveButtons()
    {
        foreach (Button b in moveButtonsPanel.GetComponentsInChildren<Button>())
            b.interactable = true;
    }

    private void DisableMoveButtons()
    {
        foreach (Button b in moveButtonsPanel.GetComponentsInChildren<Button>())
            b.interactable = false;
    }

    public void OnPlayerMove(string move)
    {
        if (!playerTurn) return;
        StartCoroutine(PerformPlayerMove(move));
    }

    IEnumerator PerformPlayerMove(string move)
    {
        playerTurn = false;

        int damage = Mathf.Max(1, playerPM.attack - enemyPM.defense + Random.Range(-2, 3));
        enemyPM.health -= damage;
        enemyHealthBar.value = Mathf.Max(enemyPM.health, 0);

        dialogueText.text = playerPM.firstName + " used " + move + " and dealt " + damage + " damage.";
        yield return new WaitForSeconds(1.2f);

        if (enemyPM.health <= 0)
        {
            dialogueText.text = enemyPM.firstName + " fainted.";
            yield return new WaitForSeconds(1.2f);
            BattleOver(true);
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = "Enemy is choosing a move...";
        DisableMoveButtons();
        yield return new WaitForSeconds(1f);

        string enemyMove = enemyPM.moves[Random.Range(0, enemyPM.moves.Length)];

        int damage = Mathf.Max(1, enemyPM.attack - playerPM.defense + Random.Range(-2, 3));
        playerPM.health -= damage;
        playerHealthBar.value = Mathf.Max(playerPM.health, 0);

        dialogueText.text = "Enemy " + enemyPM.firstName + " used " + enemyMove + " and dealt " + damage + " damage.";
        yield return new WaitForSeconds(1.2f);

        if (playerPM.health <= 0)
        {
            dialogueText.text = playerPM.firstName + " fainted.";
            yield return new WaitForSeconds(1f);
            BattleOver(false);
        }
        else
        {
            playerTurn = true;
            dialogueText.text = "Choose your move.";
            EnableMoveButtons();
        }
    }

    void BattleOver(bool playerWon)
    {
        if (playerWon)
        {
            Debug.Log("Player won the battle.");
        }
        else
        {
            GameManager.Instance.pocketMenInventory.Remove(playerPM);
            Debug.Log(playerPM.firstName + " was removed from your inventory.");
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("OverworldScene");
    }
}
