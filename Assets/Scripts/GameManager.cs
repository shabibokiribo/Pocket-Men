using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //Singleton for global access
    public static GameManager instance;

    [Header("Player Data")]
    public int playerLevel = 1;
    public int playerRating = 0;
    public List<PocketMan> pocketMenInventory = new List<PocketMan>();

    public void Start()
    {
        if (instance == null)
        {
            GameObject gm = Instantiate(Resources.Load<GameObject>("Prefabs/Managers/GameManager"));
            instance = gm.GetComponent<GameManager>();
        }
    }

    private void Awake()
    {
        //only one instance should exist
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
            
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddPocketMan(PocketMan pm)
    {
        if (!pocketMenInventory.Contains(pm))
        {
            pocketMenInventory.Add(pm);
            Debug.Log(pm.name + " added to inventory!");
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
