using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    // The player's actual Pocket Men
    public List<PocketManInstance> pocketMen = new List<PocketManInstance>();

    public int maxPocketMen = 6;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public bool AddPocketMan(PocketManInstance p)
    {
        if (pocketMen.Count >= maxPocketMen)
        {
            Debug.Log("Inventory full! Need to discard one first.");
            return false;
        }

        pocketMen.Add(p);
        Debug.Log("Added PocketMan: " + p.firstName + " " + p.lastName);
        return true;
    }

    public void RemovePocketMan(int index)
    {
        if (index >= 0 && index < pocketMen.Count)
            pocketMen.RemoveAt(index);
    }
}

