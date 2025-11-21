using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public float interactRange = 5f;
    public LayerMask interactableLayer;
    private Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
        if (mainCam == null)
            Debug.LogError("No Main Camera found!");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, interactableLayer);

            if (hit.collider != null)
            {
                Debug.Log("Raycast hit: " + hit.collider.name);

                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    if (Vector2.Distance(transform.position, hit.collider.transform.position) <= interactRange)
                    {
                        interactable.Interact();
                    }
                    else Debug.Log("Too far to interact with: " + hit.collider.name);
                }
            }
            else
            {
                Debug.Log("Raycast hit nothing!");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}
