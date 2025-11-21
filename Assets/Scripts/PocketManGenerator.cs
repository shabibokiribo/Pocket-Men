using UnityEngine;

public class PocketManGenerator : MonoBehaviour
{
    public static PocketManGenerator Instance;

    public string[] firstNames;
    public string[] lastNames;

    public PocketManData[] allTypes; // Assign via inspector

    private void Awake()
    {
        Instance = this;
    }

    public PMObj GenerateRandomPocketMan()
    {
        PocketMan newMan = new PocketMan();

        // Random names
        newMan.firstName = firstNames[Random.Range(0, firstNames.Length)];
        newMan.lastName = lastNames[Random.Range(0, lastNames.Length)];

        // Random type
        newMan.type = allTypes[Random.Range(0, allTypes.Length)];

        // Stats from ranges
        newMan.health = Random.Range(newMan.type.minHealth, newMan.type.maxHealth + 1);
        newMan.attack = Random.Range(newMan.type.minAttack, newMan.type.maxAttack + 1);
        newMan.defense = Random.Range(newMan.type.minDefense, newMan.type.maxDefense + 1);

        // Moves — pick 2 from possible list
        int moveCount = Mathf.Min(2, newMan.type.possibleMoves.Length);
        newMan.moves = new string[moveCount];
        for (int i = 0; i < moveCount; i++)
            newMan.moves[i] = newMan.type.possibleMoves[i];

        return newMan;
    }
}
