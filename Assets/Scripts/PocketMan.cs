using UnityEngine;

[CreateAssetMenu(fileName = "PocketMan", menuName = "Scriptable Objects/PocketMan")]
public class PocketMan : ScriptableObject
{
    public string manName;
    public int level;
    public int attack;
    public int defense;
    public int health;
    public Sprite sprite;

    public string typeName;

    


    [Header("Stat Ranges")]
    public int minHealth;
    public int maxHealth;

    public int minAttack;
    public int maxAttack;

    public int minDefense;
    public int maxDefense;

    [Header("Possible Moves")]
    public string[] possibleMoves;
}
