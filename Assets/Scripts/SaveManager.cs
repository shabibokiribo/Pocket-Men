using System;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private string savePath;

    public SaveData currentSaveData;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Path.Combine(Application.persistentDataPath, "save.json");

        LoadGame();
    }

    // -----------------------------
    // PUBLIC SAVE / LOAD FUNCTIONS
    // -----------------------------

    public void SaveGame()
    {
        try
        {
            string json = JsonUtility.ToJson(currentSaveData, true);
            File.WriteAllText(savePath, json);
            Debug.Log("Game Saved at: " + savePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Save failed: " + e.Message);
        }
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("No save found, creating new one.");
            currentSaveData = new SaveData();  // Start with empty save
            return;
        }

        try
        {
            string json = File.ReadAllText(savePath);
            currentSaveData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Loaded save file.");
        }
        catch (Exception e)
        {
            Debug.LogError("Load failed: " + e.Message);
            currentSaveData = new SaveData();
        }
    }

    public void DeleteSave()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Save deleted.");
        }

        currentSaveData = new SaveData();
    }
}

