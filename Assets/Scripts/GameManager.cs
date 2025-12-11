using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Data")]
    public int playerLevel = 1;
    public int playerRating = 0;
    public List<PMInst> pocketMenInventory = new List<PMInst>();

    [HideInInspector] public PMInst currentPlayerPM;
    [HideInInspector] public List<PMInst> currentEnemyTeam;

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

    public void AddPocketMan(PMInst pm)
    {
        if (pocketMenInventory.Count >= 6)
        {
            Debug.Log("Inventory full! Cannot add more PocketMen.");
            return;
        }

        if (!pocketMenInventory.Contains(pm))
        {
            pocketMenInventory.Add(pm);
            Debug.Log($"{pm.firstName} {pm.lastName} added to inventory!");
        }
    }

    public void SetActivePocketMan(PMInst pm)
    {
        if (pocketMenInventory.Contains(pm))
        {
            currentPlayerPM = pm;
            Debug.Log($"{pm.firstName} is now your active PocketMan.");
            EnsureMoves(currentPlayerPM);
        }
        else
        {
            Debug.LogWarning("You do not own this PocketMan!");
        }
    }

    public PMInst GetActivePlayerPM()
    {
        if (currentPlayerPM == null)
        {
            if (pocketMenInventory.Count > 0)
            {
                currentPlayerPM = pocketMenInventory[0];
                EnsureMoves(currentPlayerPM);
                Debug.Log($"{currentPlayerPM.firstName} set as active PocketMan with moves: " +
                          string.Join(", ", currentPlayerPM.moves));
            }
            else
            {
                Debug.LogWarning("No PocketMen in inventory!");
            }
        }
        return currentPlayerPM;
    }

    public void EnsureMoves(PMInst pm)
    {
        if (pm == null) return;

        // Set maxHealthStat if missing
        if (pm.maxHealthStat == 0) pm.maxHealthStat = pm.health;

        if (pm.moves != null && pm.moves.Length > 0) return;

        if (pm.baseData == null || pm.baseData.possibleMoves == null || pm.baseData.possibleMoves.Length == 0)
        {
            // Assign fallback move
            pm.moves = new string[] { "Struggle" };
            Debug.LogWarning($"{pm.firstName} has no moves in baseData. Assigned fallback move.");
            return;
        }

        int moveCount = Mathf.Min(2, pm.baseData.possibleMoves.Length);
        List<string> chosenMoves = new List<string>();
        for (int i = 0; i < moveCount; i++)
        {
            string pick;
            int safety = 0;
            do
            {
                pick = pm.baseData.possibleMoves[Random.Range(0, pm.baseData.possibleMoves.Length)];
                safety++;
            } while (chosenMoves.Contains(pick) && safety < 10);

            chosenMoves.Add(pick);
        }

        pm.moves = chosenMoves.ToArray();
    }

    public void EnsureMovesForTeam(List<PMInst> team)
    {
        if (team == null) return;
        foreach (var pm in team)
        {
            EnsureMoves(pm);
        }
    }

    public void IncreaseRating(int amount)
    {
        playerRating += amount;
        Debug.Log("Player Rating: " + playerRating);
    }

    public void LevelUp()
    {
        playerLevel++;
        Debug.Log("Player leveled up to: " + playerLevel);
    }

    public PMInst CreateInstance(PocketMan baseData)
    {
        PMInst pm = new PMInst();
        pm.baseData = baseData;

        pm.firstName = baseData.manName;
        pm.lastName = ""; // Optional

        // Roll stats from ranges
        pm.health = Random.Range(baseData.minHealth, baseData.maxHealth + 1);
        pm.attack = Random.Range(baseData.minAttack, baseData.maxAttack + 1);
        pm.defense = Random.Range(baseData.minDefense, baseData.maxDefense + 1);

        pm.maxHealthStat = pm.health;

        // Assign moves
        if (baseData.possibleMoves != null && baseData.possibleMoves.Length > 0)
        {
            int moveCount = Mathf.Min(2, baseData.possibleMoves.Length);
            pm.moves = new string[moveCount];

            for (int i = 0; i < moveCount; i++)
            {
                pm.moves[i] = baseData.possibleMoves[Random.Range(0, baseData.possibleMoves.Length)];
            }
        }
        else
        {
            pm.moves = new string[0];
        }

        return pm;
    }

}
