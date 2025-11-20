using System;
using UnityEngine;

[Serializable]
public class SaveData
{
    // Player info
    public float playerX;
    public float playerY;
    public float playerZ;

    public int playerLevel;
    public int playerXP;

    // Which trainers are defeated
    public bool[] defeatedTrainers;

    // Pocket Men team (expand later)
    public PocketManData[] pocketMenTeam;

    public SaveData()
    {
        playerLevel = 1;
        playerXP = 0;

        defeatedTrainers = new bool[50]; // example max trainer count
        pocketMenTeam = new PocketManData[6]; // party of 6
    }
}

[Serializable]
public class PocketManData
{
    public string name;
    public int level;
    public int health;
}
