using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Data")]
    public int playerLevel = 1;
    public int playerRating = 0;
    public List<PMInst> pocketMenInventory = new List<PMInst>();

    // --- CURRENT BATTLE DATA ---
    [HideInInspector] public PMInst currentPlayerPM;       // Active PocketMan for battle
    [HideInInspector] public List<PMInst> currentEnemyTeam; // Enemy team for battle

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

    // Add a PocketMan to the player's inventory
    public void AddPocketMan(PMInst pm)
    {
        if (!pocketMenInventory.Contains(pm))
        {
            pocketMenInventory.Add(pm);
            Debug.Log($"{pm.firstName} {pm.lastName} added to inventory!");
        }
    }

    // Set the active PocketMan for battle
    public void SetActivePocketMan(PMInst pm)
    {
        if (pocketMenInventory.Contains(pm))
        {
            currentPlayerPM = pm;
            Debug.Log($"{pm.firstName} is now your active PocketMan.");

            // Ensure moves are assigned if not already
            EnsureMoves(currentPlayerPM);
        }
        else
        {
            Debug.LogWarning("You do not own this PocketMan!");
        }
    }

    // Returns the current active PocketMan, generating moves if necessary
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

    // Ensure the PocketMan has 1–2 moves assigned at runtime
    public void EnsureMoves(PMInst pm)
    {
        if (pm.moves != null && pm.moves.Length > 0) return;
        if (pm.baseData == null || pm.baseData.possibleMoves == null || pm.baseData.possibleMoves.Length == 0) return;

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

    // Ensure all PMInst in a list have moves
    public void EnsureMovesForTeam(List<PMInst> team)
    {
        if (team == null) return;
        foreach (var pm in team)
        {
            EnsureMoves(pm);
        }
    }

    // Increase player rating
    public void IncreaseRating(int amount)
    {
        playerRating += amount;
        Debug.Log("Player Rating: " + playerRating);
    }

    // Level up the player
    public void LevelUp()
    {
        playerLevel++;
        Debug.Log("Player leveled up to: " + playerLevel);
    }
}
