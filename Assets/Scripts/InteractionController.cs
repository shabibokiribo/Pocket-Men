using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactRange = 5f; // how close the player must be
    public LayerMask interactableLayer; // optional: set a layer for interactables

    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
        if (mainCam == null)
            Debug.LogError("[InteractionController] No Main Camera found in the scene!");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // left click (change to 1 if you want right click)
        {
            Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Mouse world position: " + mouseWorldPos);

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity, interactableLayer);
            if (hit.collider != null)
            {
                Debug.Log("Raycast hit: " + hit.collider.name);
            }
            else
            {
                Debug.Log("Raycast hit nothing!");
            }

            HandleInteraction();
        }
    }

    private void HandleInteraction()
    {
        Vector2 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, Mathf.Infinity, interactableLayer);

        if (hit.collider != null)
        {
            // Check for Interactable (NPCs)
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                float distance = Vector2.Distance(transform.position, hit.collider.transform.position);
                if (distance <= interactRange)
                {
                    interactable.Interact();
                    Debug.Log("[InteractionController] Interacted with: " + hit.collider.name);
                    return;
                }
                else
                {
                    Debug.Log("[InteractionController] Too far to interact with: " + hit.collider.name);
                }
            }

            // Check for City (pillaging)
            City city = hit.collider.GetComponent<City>();
            if (city != null)
            {
                float distance = Vector2.Distance(transform.position, hit.collider.transform.position);
                if (distance <= interactRange)
                {
                    city.Pillage();
                    Debug.Log("[InteractionController] Pillaged city: " + hit.collider.name);
                    return;
                }
                else
                {
                    Debug.Log("[InteractionController] Too far to pillage city: " + hit.collider.name);
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
