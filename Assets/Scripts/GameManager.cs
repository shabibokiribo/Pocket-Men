using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Data")]
    public int playerLevel = 1;
    public int playerRating = 0;
    public List<PMInst> pocketMenInventory = new List<PMInst>();

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // destroy duplicate
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // persist across scenes
    }

    // --- Player Methods ---
    //public List<PMInst> pocketMenInventory = new List<PMInst>();

    public void AddPocketMan(PMInst pm)
    {
        if (!pocketMenInventory.Contains(pm))
        {
            pocketMenInventory.Add(pm);
            Debug.Log(pm.firstName + " " + pm.lastName + " added to inventory!");
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
