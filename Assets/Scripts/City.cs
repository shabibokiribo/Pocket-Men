using UnityEngine;

public class City : MonoBehaviour
{
    [Header("City Settings")]
    public string cityName = "Unnamed City";
    public int minLevel = 1;
    public int maxLevel = 5;

    public string[] firstNamesOverride; // optional override for this city
    public string[] lastNamesOverride;  // optional override for this city

    // Called when the player clicks this city
    public void Pillage()
    {
        // Generate a random PocketMan
        PMInst newPM = GenerateRandomPocketManForCity();

        // Show the popup
        UIManager.Instance.ShowPocketManPopup(newPM, OnPocketManDecision);
    }

    private PMInst GenerateRandomPocketManForCity()
    {
        // Pick a random type from PocketManGenerator
        PocketMan type = PocketManGenerator.Instance.pocketManTypes[Random.Range(0, PocketManGenerator.Instance.pocketManTypes.Length)];

        PMInst pm = new PMInst();
        pm.baseData = type;
        

        // Choose name using overrides if available, otherwise generator defaults
        string[] firsts = (firstNamesOverride != null && firstNamesOverride.Length > 0) ? firstNamesOverride : PocketManGenerator.Instance.firstNames;
        string[] lasts = (lastNamesOverride != null && lastNamesOverride.Length > 0) ? lastNamesOverride : PocketManGenerator.Instance.lastNames;

        pm.firstName = firsts[Random.Range(0, firsts.Length)];
        pm.lastName = lasts[Random.Range(0, lasts.Length)];

        // Level within city range
        pm.level = Random.Range(minLevel, maxLevel + 1);

        // Stats based on type + random within min/max
        pm.health = Random.Range(type.minHealth, type.maxHealth + 1);
        pm.attack = Random.Range(type.minAttack, type.maxAttack + 1);
        pm.defense = Random.Range(type.minDefense, type.maxDefense + 1);

        // Random moves (1–2 moves)
        int moveCount = Mathf.Min(2, type.possibleMoves.Length);
        pm.moves = new string[moveCount];
        for (int i = 0; i < moveCount; i++)
        {
            pm.moves[i] = type.possibleMoves[Random.Range(0, type.possibleMoves.Length)];
        }

        return pm;
    }

    // Callback when the player decides to keep or discard
    private void OnPocketManDecision(bool keep, PMInst pm)
    {
        if (keep)
        {
            GameManager.Instance.AddPocketMan(pm);
            Debug.Log($"{pm.firstName} {pm.lastName} added to inventory!");
        }
        else
        {
            Debug.Log($"{pm.firstName} {pm.lastName} was discarded.");
        }
    }

    // Optional: visualize city in editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
