using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float xzLimit = 25f;
    public float maxY = 15f;
    public float mouseSensitivity = 100f;
    public float pitchMin = -80f;
    public float pitchMax = 80f;

    private float yaw = 0f;
    private float pitch = 0f;

    void Start()
    {
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void Update()
    {
        HandleMovement();
        if(Input.GetKey(KeyCode.Mouse1))
        {
            HandleRotation();
        }
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        forward.y = 0f;
        right.y = 0f;

        Vector3 move = (forward.normalized * vertical + right.normalized * horizontal) * moveSpeed * Time.deltaTime;
        transform.position += move;

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -xzLimit, xzLimit);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, -xzLimit, xzLimit);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, 0f, maxY);

        transform.position = clampedPosition;
    }

    void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}
