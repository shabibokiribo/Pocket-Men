using UnityEngine;

[CreateAssetMenu(fileName = "PocketMan", menuName = "Scriptable Objects/PocketMan")]
public class PocketMan : ScriptableObject
{
    public string manName;
    public int level;
    public int maxHealth;
    public int attack;
    public int defense;
    public Sprite sprite;
}
