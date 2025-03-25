using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public enum CameraMoveMode
    {
        FollowPlayer,
        LockedCenter
    }

    public float camMoveSpeed;

    public float offset;

    public CameraMoveMode currMoveMode;

    public PlayerMovement playerMovement;

    Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerTransform = playerMovement.transform;   
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (currMoveMode == CameraMoveMode.FollowPlayer)
        {
            Vector3 target = playerTransform.position - playerMovement.moveDir * offset;

            transform.position = Vector3.MoveTowards(transform.position, target, camMoveSpeed * Time.deltaTime);
        }
    }
}
