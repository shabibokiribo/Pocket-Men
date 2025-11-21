using UnityEngine;

public class City : Interactable
{
    [Header("City Settings")]
    public string cityName = "Unnamed City";
    public int minLevel = 1;
    public int maxLevel = 5;

    public string[] firstNamesOverride; // optional override for this city
    public string[] lastNamesOverride;  // optional override for this city

    public override void Interact()
    {
        Pillage();
    }

    public void Pillage()
    {
        // Generate a random PocketMan using your generator
        PMInst newPM = PocketManGenerator.Instance.GenerateRandomPocketMan(
            minLevel,
            maxLevel,
            firstNamesOverride,
            lastNamesOverride
        );

        if (newPM == null)
        {
            Debug.LogWarning("Failed to generate PocketMan!");
            return;
        }

        // Show the popup
        UIManager.Instance.ShowPocketManPopup(newPM, OnPocketManDecision);
        Debug.Log($"Pillaged city '{cityName}' and got {newPM.firstName} {newPM.lastName}");
    }

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
