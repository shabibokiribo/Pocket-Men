using System;
using UnityEngine;

[Serializable]
public class PMInst
{
    public string firstName;
    public string lastName;

    // Reference back to the type/template SO
    public PocketMan baseData;

    // Final rolled stats for this instance
    public int level;
    public int health;
    public int attack;
    public int defense;
    public int maxHealthStat; // Set when generating PM

    // Generated moves for this instance
    public string[] moves;

    // Sprite to show in UI (taken from baseData by default)
    public Sprite sprite => baseData != null ? baseData.sprite : null;

    public string FullName => $"{firstName} {lastName}";
    public string TypeName => baseData != null ? baseData.typeName : "Unknown";
}
