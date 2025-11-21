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

    private void Start()
    {
        // Fallback if StartBattle was not called
        if (playerPM == null || enemyPM == null)
        {
            playerPM = GameManager.Instance.GetActivePlayerPM();
            EnsureMoves(playerPM);

            if (GameManager.Instance.currentEnemyTeam != null && GameManager.Instance.currentEnemyTeam.Count > 0)
            {
                foreach (var enemy in GameManager.Instance.currentEnemyTeam)
                    EnsureMoves(enemy);

                enemyPM = GameManager.Instance.currentEnemyTeam[0];
            }
            else
            {
                Debug.LogError("No enemy team found. Cannot start battle.");
                return;
            }
        }

        SetupBattle();
    }

    public void StartBattle(PMInst player, PMInst enemy)
    {
        playerPM = player;
        enemyPM = enemy;

        EnsureMoves(playerPM);
        EnsureMoves(enemyPM);

        SetupBattle();
    }

    private void EnsureMoves(PMInst pm)
    {
        if (pm.moves == null || pm.moves.Length == 0)
            GameManager.Instance.EnsureMoves(pm);
    }

    private void SetupBattle()
    {
        if (playerPM == null || enemyPM == null)
        {
            Debug.LogError("Cannot setup battle. Missing PocketMen.");
            return;
        }

        // Ensure maxHealthStat is set
        if (playerPM.maxHealthStat == 0) playerPM.maxHealthStat = playerPM.health;
        if (enemyPM.maxHealthStat == 0) enemyPM.maxHealthStat = enemyPM.health;

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

        dialogueText.text = "A battle began!";
        playerTurn = true;

        SetupMoveButtons();
    }

    private void SetupMoveButtons()
    {
        // Clear existing buttons
        foreach (Transform child in moveButtonsPanel.transform)
            Destroy(child.gameObject);

        if (playerPM.moves == null || playerPM.moves.Length == 0)
        {
            Debug.LogError("Player has no moves!");
            dialogueText.text = "ERROR: Player has no moves.";
            return;
        }

        foreach (string move in playerPM.moves)
        {
            string capturedMove = move;

            Button btn = Instantiate(moveButtonPrefab, moveButtonsPanel.transform);
            TMP_Text btnText = btn.GetComponentInChildren<TMP_Text>();
            if (btnText == null)
            {
                Debug.LogError("Move button prefab is missing TMP_Text child!");
                continue;
            }
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

    private IEnumerator PerformPlayerMove(string move)
    {
        playerTurn = false;

        int damage = Mathf.Max(1, playerPM.attack - enemyPM.defense + Random.Range(-2, 3));
        enemyPM.health = Mathf.Clamp(enemyPM.health - damage, 0, enemyPM.maxHealthStat);
        enemyHealthBar.value = enemyPM.health;

        dialogueText.text = $"{playerPM.firstName} used {move} and dealt {damage} damage!";
        yield return new WaitForSeconds(1.2f);

        if (enemyPM.health <= 0)
        {
            dialogueText.text = $"{enemyPM.firstName} fainted!";
            yield return new WaitForSeconds(1.2f);
            BattleOver(true);
        }
        else
        {
            StartCoroutine(EnemyTurn());
        }
    }

    private IEnumerator EnemyTurn()
    {
        dialogueText.text = "Enemy is choosing a move...";
        DisableMoveButtons();
        yield return new WaitForSeconds(1f);

        string enemyMove = enemyPM.moves[Random.Range(0, enemyPM.moves.Length)];
        int damage = Mathf.Max(1, enemyPM.attack - playerPM.defense + Random.Range(-2, 3));
        playerPM.health = Mathf.Clamp(playerPM.health - damage, 0, playerPM.maxHealthStat);
        playerHealthBar.value = playerPM.health;

        dialogueText.text = $"Enemy {enemyPM.firstName} used {enemyMove} and dealt {damage} damage!";
        yield return new WaitForSeconds(1.2f);

        if (playerPM.health <= 0)
        {
            dialogueText.text = $"{playerPM.firstName} fainted!";
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

    private void BattleOver(bool playerWon)
    {
        if (playerWon)
        {
            Debug.Log("Player won the battle.");
        }
        else
        {
            GameManager.Instance.pocketMenInventory.Remove(playerPM);
            Debug.Log($"{playerPM.firstName} was removed from your inventory.");
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("Neighborhood");
    }
}
