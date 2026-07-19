using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform cameraPivot;
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference lookAction;
    [SerializeField] InputActionReference interactAction;

    [SerializeField] float walkSpeed;
    [SerializeField] float mouseSensitivity;
    private Vector2 moveDirection;
    private Vector2 lookDirection;
    private float pitch = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    void Update()
    {
        moveDirection = moveAction.action.ReadValue<Vector2>();
        lookDirection = lookAction.action.ReadValue<Vector2>();

        pitch -= lookDirection.y * (Mathf.Clamp(mouseSensitivity - 0.2f, 0.1f, 1f));
        pitch = Mathf.Clamp(pitch, -90f, 90f);

        transform.Rotate(0, lookDirection.x * mouseSensitivity, 0);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0, 0);
    }

    void FixedUpdate()
    {
        Vector3 movement = transform.forward * moveDirection.y + transform.right * moveDirection.x;
        rigidBody.linearVelocity = new Vector3(movement.x * walkSpeed, rigidBody.linearVelocity.y, movement.z * walkSpeed);
    }
}
