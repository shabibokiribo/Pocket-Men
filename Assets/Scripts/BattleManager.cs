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

        // Setup move buttons
        foreach (Transform child in moveButtonsPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (string move in playerPM.moves)
        {
            Button btn = Instantiate(moveButtonPrefab, moveButtonsPanel.transform);
            btn.GetComponentInChildren<TMP_Text>().text = move;
            btn.onClick.AddListener(() => OnPlayerMove(move));
        }

        dialogueText.text = "A wild PocketMan appeared!";
        playerTurn = true;
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
