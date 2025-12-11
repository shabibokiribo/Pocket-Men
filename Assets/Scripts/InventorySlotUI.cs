using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    public int slotIndex;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            InventoryUIManager.instance.SelectPocketman(slotIndex);
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            InventoryUIManager.instance.DeletePocketman(slotIndex);
        }
    }
}
