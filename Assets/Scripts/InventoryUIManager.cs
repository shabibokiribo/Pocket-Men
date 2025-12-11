using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager instance;

    [System.Serializable]
    public class SlotUI
    {
        public Image portraitImage;
        public TMP_Text nameText;
    }

    public SlotUI[] slots; // 6 slots

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        RefreshInventoryUI();
    }

    public void RefreshInventoryUI()
    {
        var inventory = GameManager.Instance.pocketMenInventory;

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.Count)
            {
                PMInst pm = inventory[i];

                // Set portrait
                slots[i].portraitImage.sprite = pm.baseData.sprite;
                slots[i].portraitImage.color = Color.white;

                // Set name
                slots[i].nameText.text = pm.FullName;
            }
            else
            {
                // Clear empty slot
                slots[i].portraitImage.sprite = null;
                slots[i].portraitImage.color = new Color(1, 1, 1, 0);
                slots[i].nameText.text = "";
            }
        }
    }

    public void SelectPocketman(int index)
    {
        var inventory = GameManager.Instance.pocketMenInventory;
        if (index >= inventory.Count) return;

        GameManager.Instance.SetActivePocketMan(inventory[index]);
        Debug.Log("Selected PM in slot: " + index);
    }

    public void DeletePocketman(int index)
    {
        var inventory = GameManager.Instance.pocketMenInventory;
        if (index >= inventory.Count) return;

        inventory.RemoveAt(index);
        RefreshInventoryUI();
        Debug.Log("Deleted PM in slot: " + index);
    }
}
