using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactRange = 5f;
    public LayerMask interactableLayer;

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
        if (mainCam == null)
            Debug.LogError("[InteractionController] No Main Camera found in the scene!");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // left click
        {
            Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity, interactableLayer);
            if (hit.collider != null)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    float distance = Vector2.Distance(transform.position, hit.collider.transform.position);
                    if (distance <= interactRange)
                    {
                        interactable.Interact();
                        Debug.Log($"[InteractionController] Interacted with {hit.collider.name}");
                    }
                    else
                    {
                        Debug.Log($"[InteractionController] Too far to interact with {hit.collider.name}");
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
