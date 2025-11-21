using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Data")]
    public int playerLevel = 1;
    public int playerRating = 0;
    public List<PMInst> pocketMenInventory = new List<PMInst>();

    // CURRENT BATTLE DATA
    [HideInInspector] public PMInst currentPlayerPM;      // Active PocketMan for battle
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

    public void AddPocketMan(PMInst pm)
    {
        if (!pocketMenInventory.Contains(pm))
        {
            pocketMenInventory.Add(pm);
            Debug.Log(pm.firstName + " " + pm.lastName + " added to inventory!");
        }
    }

    public void SetActivePocketMan(PMInst pm)
    {
        if (pocketMenInventory.Contains(pm))
        {
            currentPlayerPM = pm;
            Debug.Log($"{pm.firstName} is now your active PocketMan.");
        }
        else
        {
            Debug.LogWarning("You do not own this PocketMan!");
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
}
