using UnityEngine;

public class City : Interactable
{
    [Header("City Settings")]
    public string cityName = "Unnamed City";
    public int minLevel = 1;
    public int maxLevel = 5;

    public string[] firstNamesOverride;
    public string[] lastNamesOverride;

    public override void Interact()
    {
        Pillage();
    }

    public void Pillage()
    {
        Debug.Log("Pillage called on city: " + cityName);

        // Generate PocketMan
        PMInst newPM = PocketManGenerator.Instance.GenerateRandomPocketMan(
            minLevel,
            maxLevel,
            firstNamesOverride,
            lastNamesOverride
        );

        if (newPM == null)
        {
            Debug.LogWarning("PocketMan generation failed!");
            return;
        }

        Debug.Log($"Generated PocketMan: {newPM.firstName} {newPM.lastName} (Level {newPM.level})");

        // Show UI popup
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowPocketManPopup(newPM, OnPocketManDecision);
        }
        else
        {
            Debug.LogWarning("UIManager instance not found!");
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.5f);
    }
}
