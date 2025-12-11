using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIManager : MonoBehaviour
{
    public InventorySlotUI[] slots; // Assign all 6 in inspector
    public TMP_Text activePMText;

    void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        var inv = GameManager.Instance.pocketMenInventory;

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inv.Count)
            {
                slots[i].SetData(inv[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }

        UpdateActivePMText();
    }

    public void UpdateActivePMText()
    {
        if (GameManager.Instance.currentPlayerPM != null)
            activePMText.text = "Active PM: " + GameManager.Instance.currentPlayerPM.firstName;
        else
            activePMText.text = "Active PM: None";
    }

    public void SelectPM(PMInst pm)
    {
        GameManager.Instance.SetActivePocketMan(pm);
        UpdateActivePMText();
    }

    public void DeletePM(PMInst pm)
    {
        GameManager.Instance.pocketMenInventory.Remove(pm);

        if (GameManager.Instance.currentPlayerPM == pm)
            GameManager.Instance.currentPlayerPM = null; // Removed active one

        RefreshUI();
    }
}

