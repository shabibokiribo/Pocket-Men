using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public float interactRange = 2f; //how close player needs to be

    private void Update()
    {
        if (Input.GetMouseButtonDown(1)) //right click
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //checks for interactables in range
            Collider2D[] hits = Physics2D.OverlapCircleAll(mouseWorldPos, interactRange);

            foreach (Collider2D hit in hits)
            {
                Interactable interactable = hit.GetComponent<Interactable>();
                if (interactable != null)
                {
                    interactable.Interact();
                    break;
                }
            }
        }
    }



}
