using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target; //this is the player
    private float smoothSpeed = 0.05f;
    public Vector3 offset;

    private void LateUpdate()
    {
        if (target == null) return; //if theres no player don't do anything

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothed;

        transform.rotation = Quaternion.identity; //keeps rotation fixed



    }
}
