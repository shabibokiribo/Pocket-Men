using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class NPCTrainer : Interactable
{
    [Header("Trainer Info")]
    public string trainerName = "Trainer";
    [Range(1, 10)]
    public int difficulty = 1;

    [Header("Team Templates (ScriptableObject templates)")]
    public PocketMan[] teamTemplates;

    [Header("Dialogue")]
    [TextArea(2, 6)]
    public string[] dialogueLines;

    [Header("Difficulty Scaling")]
    public int levelMultiplier = 3;
    public int healthBoostPerDifficulty = 2;
    public int attackBoostPerDifficulty = 1;
    public int defenseBoostPerDifficulty = 1;

    [Header("Naming (optional)")]
    public string[] firstNames;
    public string[] lastNames;

    [HideInInspector] public List<PMInst> generatedTeam = new List<PMInst>();

    public bool hasBeenDefeated = false;
    public int trainerID = -1;

    private void Start()
    {
        GenerateTeamFromTemplates();
    }

    public override void Interact()
    {
        if (hasBeenDefeated)
        {
            DialogueManager.Instance.StartDialogue(
                new string[] { trainerName + ": You already beat me." }
            );
            return;
        }

        DialogueManager.Instance.StartDialogue(dialogueLines, OnDialogueComplete, trainerName);
    }

    private void OnDialogueComplete()
    {
        if (!hasBeenDefeated)
            BattleConfirmationManager.Instance.ShowConfirmation(this);
    }

    public void GenerateTeamFromTemplates()
    {
        generatedTeam.Clear();
        EnsureNameFallbacks();

        if (teamTemplates == null || teamTemplates.Length == 0)
        {
            Debug.LogError($"{trainerName} has no teamTemplates assigned!");
            return;
        }

        foreach (var template in teamTemplates)
        {
            if (template != null)
                generatedTeam.Add(GenerateInstanceFromTemplate(template, difficulty));
        }
    }

    private void EnsureNameFallbacks()
    {
        if (firstNames == null || firstNames.Length == 0)
            firstNames = new string[] { "Sam", "Alex", "Casey", "Taylor", "Riley", "Jordan" };

        if (lastNames == null || lastNames.Length == 0)
            lastNames = new string[] { "Smith", "Johnson", "Lee", "Garcia", "Brown" };
    }

    private PMInst GenerateInstanceFromTemplate(PocketMan template, int difficultyLevel)
    {
        PMInst p = new PMInst();
        p.baseData = template;

        // Name
        p.firstName = firstNames[Random.Range(0, firstNames.Length)];
        p.lastName = lastNames[Random.Range(0, lastNames.Length)];

        // Level
        p.level = Mathf.Max(1, difficultyLevel * levelMultiplier);

        // Stat boosts
        int healthBoost = difficultyLevel * healthBoostPerDifficulty;
        int atkBoost = difficultyLevel * attackBoostPerDifficulty;
        int defBoost = difficultyLevel * defenseBoostPerDifficulty;

        // Stats
        p.maxHealthStat = Random.Range(template.minHealth + healthBoost, template.maxHealth + healthBoost + 1);
        p.health = p.maxHealthStat;

        p.attack = Random.Range(template.minAttack + atkBoost, template.maxAttack + atkBoost + 1);
        p.defense = Random.Range(template.minDefense + defBoost, template.maxDefense + defBoost + 1);

        // Sprite — YOU FORGOT THIS
        p.sprite = template.sprite;

        // MOVES
        AssignMovesFromTemplate(p, template);

        Debug.Log($"Generated {p.firstName} with moves: {string.Join(", ", p.moves)}");

        return p;
    }

    private void AssignMovesFromTemplate(PMInst p, PocketMan template)
    {
        if (template.possibleMoves == null || template.possibleMoves.Length == 0)
        {
            Debug.LogWarning($"{template.name} has NO possibleMoves — assigning fallback move.");
            p.moves = new string[] { "Struggle" };
            return;
        }

        int moveCount = Mathf.Min(2, template.possibleMoves.Length);
        List<string> chosen = new List<string>();

        for (int i = 0; i < moveCount; i++)
        {
            string pick = template.possibleMoves[Random.Range(0, template.possibleMoves.Length)];

            int safety = 0;
            while (chosen.Contains(pick) && safety < 10)
            {
                pick = template.possibleMoves[Random.Range(0, template.possibleMoves.Length)];
                safety++;
            }

            chosen.Add(pick);
        }

        p.moves = chosen.ToArray();
    }
}
