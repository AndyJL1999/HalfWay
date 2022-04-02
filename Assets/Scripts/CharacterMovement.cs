using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float sensitivity = 3.5f;
    [SerializeField] float speed = 12.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] float moveSmoothing = 0.3f;
    [SerializeField] float mouseSmoothing = 0.03f;

    float cameraPitch = 0.0f;
    float fallSpeed = 0.0f;
    CharacterController controller;
    PlayerHealth playerHealth;

    Vector2 currentDirection = Vector2.zero;
    Vector2 currentDirectionVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        playerHealth = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();

        if(playerHealth.currentHealth < 30)
        {
            speed = 6;
        }
        else
        {
            speed = 12;
        }
    }

    void UpdateMouseLook()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, mouseDelta, ref currentMouseDeltaVelocity, mouseSmoothing);

        cameraPitch -= currentMouseDelta.y * sensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * sensitivity);
    }

    void UpdateMovement()
    {
        Vector2 inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDirection.Normalize();

        currentDirection = Vector2.SmoothDamp(currentDirection, inputDirection, ref currentDirectionVelocity, moveSmoothing);

        if (controller.isGrounded)
        {
            fallSpeed = 0.0f;
        }

        fallSpeed += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDirection.y + transform.right * currentDirection.x) * speed + Vector3.up * fallSpeed;
        controller.Move(velocity * Time.deltaTime);
    }
}
