using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PocketManInstance
{
    public string firstName;
    public string lastName;

    public PocketMan baseData; // The ScriptableObject type

    public int health;
    public int attack;
    public int defense;

    public string[] moves;

    public Sprite sprite; // inherited from baseData
}

public class PMObj : MonoBehaviour
{
    


    
        [Header("UI References")]
        public TMP_Text nameText;
        public TMP_Text typeText;
        public TMP_Text statsText;
        public Image spriteImage;

        private PocketManInstance currentInstance;

        public void Init(PocketManInstance instance)
        {
            currentInstance = instance;

            // Name
            nameText.text = $"{instance.firstName} {instance.lastName}";

            // Type / Job (from ScriptableObject)
            typeText.text = instance.baseData.typeName;

            // Stats
            statsText.text =
                $"HP: {instance.health}\n" +
                $"ATK: {instance.attack}\n" +
                $"DEF: {instance.defense}";

            // Sprite
            spriteImage.sprite = instance.sprite;
        }

        // Called when user presses "Keep"
        public void OnKeepButton()
        {
            InventoryManager.Instance.AddPocketMan(currentInstance);
            Destroy(gameObject); // close UI
        }

        // Called when user presses "Discard"
        public void OnDiscardButton()
        {
            Destroy(gameObject); // close UI
        }
    


}
