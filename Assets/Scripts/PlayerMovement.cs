using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;
    public Vector2 input;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); //gets rigidbody from player object and assigns in script
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); //gets the players input (horiuzontal is AD and the left and right arrows, etc.)
        rb.linearVelocity = input * speed;
      
    }

    private void Start()
    {
            var data = SaveManager.Instance.currentSaveData;

            transform.position = new Vector3(
                data.playerX,
                data.playerY,
                data.playerZ
            );
    }
}
