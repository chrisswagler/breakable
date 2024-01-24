using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float airControl = 5f;

    private const float gravity = 9.81f;

    private CharacterController controller;
    private Vector3 input;
    private Vector3 moveDirection;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        moveDirection.y = 0f;
    }

    private void Update()
    {
        // Get wasd input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Get input vector with speed
        input = transform.right * moveHorizontal
            + transform.forward * moveVertical;
        input *= moveSpeed;

        if (controller.isGrounded)
        {
            moveDirection = input;
            if (Input.GetButton("Jump"))
            {
                // Apply jump force if grounded and intend to jump
                moveDirection.y = Mathf.Sqrt(2 * gravity * jumpHeight);
            }
            else
            {
                moveDirection.y = 0f;
            }
        }
        else
        {
            input.y = moveDirection.y;
            // If in the air, bring moveDirection slowly towards input
            moveDirection = Vector3.Lerp(moveDirection, input, Time.deltaTime * airControl);
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }
}
