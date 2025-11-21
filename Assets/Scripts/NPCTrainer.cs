using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class NPCTrainer : Interactable
{
    [Header("Trainer Info")]
    public string trainerName = "Trainer";
    [Range(1, 10)]
    public int difficulty = 1; // 1–10

    [Header("Team Templates (ScriptableObject templates)")]
    [Tooltip("Drag the PocketMan ScriptableObjects that this trainer should use as templates.")]
    public PocketMan[] teamTemplates;

    [Header("Dialogue")]
    [TextArea(2, 6)]
    public string[] dialogueLines;

    [Header("Difficulty Scaling (tweakable)")]
    public int levelMultiplier = 3; // level = difficulty * levelMultiplier
    public int healthBoostPerDifficulty = 2; // added to min/max
    public int attackBoostPerDifficulty = 1;
    public int defenseBoostPerDifficulty = 1;

    [Header("Naming (optional) - if left empty, generator defaults will be used)")]
    public string[] firstNames;
    public string[] lastNames;

    // The generated runtime team (instances) - accessible by BattleManager
    [HideInInspector]
    public List<PMInst> generatedTeam = new List<PMInst>();

    // Tracking beaten state
    public bool hasBeenDefeated = false;
    public int trainerID = -1; // optional: use this to save/load beaten status

    // --- Unity callbacks ---
    private void Start()
    {
        // If teamTemplates are defined, generate the runtime team immediately
        GenerateTeamFromTemplates();

        // Optional: you could check SaveManager here for defeated state and disable accordingly
        // Example:
        // if (trainerID >= 0 && SaveManager.Instance.currentSaveData.defeatedTrainers[trainerID]) { hasBeenDefeated = true; gameObject.SetActive(false); }
    }



    // Player right-click calls this (Interactable override)
    public override void Interact()
    {
        //DialogueManager.Instance.StartDialogue(dialogue);

        Debug.Log($"Clicked NPC: {trainerName}");

        if (hasBeenDefeated)
        {
            DialogueManager.Instance.StartDialogue(
                new string[] { trainerName + ": You already beat me." }
            );
            return;
        }

        // Show dialogue and then start battle in callback
        DialogueManager.Instance.StartDialogue(dialogueLines, OnDialogueComplete, trainerName);
    }

    private void OnDialogueComplete()
    {
        if (hasBeenDefeated)
            return;

        BattleConfirmationManager.Instance.ShowConfirmation(this);
    }

    /// <summary>
    /// Generates a runtime team of PocketManInstance objects from the assigned templates.
    /// Call this whenever you want to (e.g., on Start or right before a battle).
    /// </summary>
    public void GenerateTeamFromTemplates()
    {
        generatedTeam.Clear();

        if (teamTemplates == null || teamTemplates.Length == 0)
            return;

        // Ensure names arrays have fallbacks if empty
        EnsureNameFallbacks();

        foreach (var template in teamTemplates)
        {
            if (template == null) continue;

            PMInst instance = GenerateInstanceFromTemplate(template, difficulty);
            generatedTeam.Add(instance);
        }
    }

    private void EnsureNameFallbacks()
    {
        // If the NPC doesn't provide name lists, fallback to a small built-in set
        if (firstNames == null || firstNames.Length == 0)
        {
            firstNames = new string[] { "Sam", "Alex", "Casey", "Taylor", "Riley", "Jordan" };
        }
        if (lastNames == null || lastNames.Length == 0)
        {
            lastNames = new string[] { "Smith", "Johnson", "Lee", "Garcia", "Brown" };
        }
    }

    private PMInst GenerateInstanceFromTemplate(PocketMan template, int difficultyLevel)
    {
        var p = new PMInst();
        p.baseData = template;

        // Name
        p.firstName = firstNames[Random.Range(0, firstNames.Length)];
        p.lastName = lastNames[Random.Range(0, lastNames.Length)];

        // Level (linear)
        p.level = Mathf.Max(1, difficultyLevel * levelMultiplier);

        // Compute stat boost from difficulty
        int healthBoost = difficultyLevel * healthBoostPerDifficulty;
        int atkBoost = difficultyLevel * attackBoostPerDifficulty;
        int defBoost = difficultyLevel * defenseBoostPerDifficulty;

        // Roll stats within template range + boost
        int minH = template.minHealth + healthBoost;
        int maxH = template.maxHealth + healthBoost;
        p.health = Random.Range(minH, maxH + 1);

        int minA = template.minAttack + atkBoost;
        int maxA = template.maxAttack + atkBoost;
        p.attack = Random.Range(minA, maxA + 1);

        int minD = template.minDefense + defBoost;
        int maxD = template.maxDefense + defBoost;
        p.defense = Random.Range(minD, maxD + 1);

        // Moves: pick up to 2 unique moves from template.possibleMoves
        int moveCount = Mathf.Min(2, template.possibleMoves != null ? template.possibleMoves.Length : 0);
        List<string> chosen = new List<string>();
        if (template.possibleMoves != null && template.possibleMoves.Length > 0)
        {
            for (int i = 0; i < moveCount; i++)
            {
                string pick;
                int safety = 0;
                do
                {
                    pick = template.possibleMoves[Random.Range(0, template.possibleMoves.Length)];
                    safety++;
                } while (chosen.Contains(pick) && safety < 10);

                chosen.Add(pick);
            }
        }
        p.moves = chosen.ToArray();

        return p;
    }

    /// <summary>
    /// Call this (for example from your BattleManager) when the trainer has truly been defeated.
    /// </summary>
    public void MarkDefeated()
    {
        hasBeenDefeated = true;

        // Optional: save state via SaveManager if you use trainerID
        if (trainerID >= 0 && SaveManager.Instance != null)
        {
            if (SaveManager.Instance.currentSaveData != null &&
                trainerID < SaveManager.Instance.currentSaveData.defeatedTrainers.Length)
            {
                SaveManager.Instance.currentSaveData.defeatedTrainers[trainerID] = true;
                SaveManager.Instance.SaveGame();
            }
        }

        // Optional: disable the NPC visually
        // gameObject.SetActive(false);
    }

    // Editor gizmo to show trainer origin
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
