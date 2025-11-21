using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

    void Start()
    {
        playerPM = GameManager.Instance.currentPlayerPM;
        enemyPM = GameManager.Instance.currentEnemyTeam[0]; // first PocketMan in enemy team
        SetupBattle();
    }

    /// <summary>
    /// Call this from outside to start a battle
    /// </summary>
    public void StartBattle(PMInst player, PMInst enemy)
    {
        playerPM = player;
        enemyPM = enemy;
        SetupBattle();
    }

    private void SetupBattle()
    {
        // Player UI
        playerSprite.sprite = playerPM.sprite;
        playerNameText.text = $"{playerPM.firstName} Lvl {playerPM.level}";
        playerHealthBar.maxValue = playerPM.maxHealthStat;
        playerHealthBar.value = playerPM.health;

        // Enemy UI
        enemySprite.sprite = enemyPM.sprite;
        enemyNameText.text = $"{enemyPM.firstName} Lvl {enemyPM.level}";
        enemyHealthBar.maxValue = enemyPM.maxHealthStat;
        enemyHealthBar.value = enemyPM.health;

        dialogueText.text = "A wild PocketMan appeared!";

        playerTurn = true;
        SetupMoveButtons();
    }

    private void SetupMoveButtons()
    {
        // Clear any existing buttons
        foreach (Transform child in moveButtonsPanel.transform)
            Destroy(child.gameObject);

        if (playerPM.moves == null || playerPM.moves.Length == 0)
        {
            Debug.LogWarning("Player has no moves!");
            return;
        }

        // Create a button for each move
        foreach (string move in playerPM.moves)
        {
            string capturedMove = move; // capture for lambda
            Button btn = Instantiate(moveButtonPrefab, moveButtonsPanel.transform);
            TMP_Text btnText = btn.GetComponentInChildren<TMP_Text>();
            if (btnText != null)
                btnText.text = capturedMove;

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
        foreach (Button btn in moveButtonsPanel.GetComponentsInChildren<Button>())
            btn.interactable = true;
    }

    private void DisableMoveButtons()
    {
        foreach (Button btn in moveButtonsPanel.GetComponentsInChildren<Button>())
            btn.interactable = false;
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
        dialogueText.text = $"{playerPM.firstName} used {move} and dealt {damage} damage!";
        yield return new WaitForSeconds(1.5f);

        if (enemyPM.health <= 0)
        {
            dialogueText.text = $"{enemyPM.firstName} fainted!";
            yield return new WaitForSeconds(1.5f);
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
        dialogueText.text = $"Enemy {enemyPM.firstName} used {enemyMove} and dealt {damage} damage!";
        yield return new WaitForSeconds(1.5f);

        if (playerPM.health <= 0)
        {
            dialogueText.text = $"{playerPM.firstName} fainted!";
            yield return new WaitForSeconds(1f);
            BattleOver(false);
        }
        else
        {
            playerTurn = true;
            dialogueText.text = "Choose your move!";
            EnableMoveButtons();
        }
    }

    void BattleOver(bool playerWon)
    {
        if (playerWon)
        {
            Debug.Log("Player won!");
        }
        else
        {
            GameManager.Instance.pocketMenInventory.Remove(playerPM);
            Debug.Log($"{playerPM.firstName} was removed from inventory!");
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("OverworldScene");
    }
}
