using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    public Image icon;
    public TMP_Text nameText;

    private PMInst storedPM;
    private InventoryUIManager manager;

    void Start()
    {
        manager = FindObjectOfType<InventoryUIManager>();
    }

    public void SetData(PMInst pm)
    {
        storedPM = pm;
        nameText.text = pm.firstName;
        icon.sprite = pm.sprite;
        icon.color = Color.white;
    }

    public void ClearSlot()
    {
        storedPM = null;
        nameText.text = "";
        icon.sprite = null;
        icon.color = Color.clear;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (storedPM == null) return;

        // Left click = select PM
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            manager.SelectPM(storedPM);
        }

        // Right click = delete PM
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            manager.DeletePM(storedPM);
        }
    }
}
