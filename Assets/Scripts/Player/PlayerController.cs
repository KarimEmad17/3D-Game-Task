using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    private Player player;
    private InputSystem_Actions controllers;

    [SerializeField] private CharacterController characterController;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 1.5f;
    

    private float speed;
    private Vector2 movementInput;
    private Vector3 moveDirection;

    [Header("Gravity")]
    [SerializeField] private float jumpHeight = 2f;
    private float velocityY;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        player = GetComponent<Player>();

        speed = walkSpeed;
    }

    private void Start()
    {
        AssignInputEvents();
    }

    private void Update()
    {
        PlayerMovement();
        ApplyGravity();

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void AssignInputEvents()
    {
        controllers = player.controls;

        controllers.Player.Move.performed += OnMove;
        controllers.Player.Move.canceled += OnMove;

        controllers.Player.Jump.performed += OnJump;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        Jump();
    }

    private void PlayerMovement()
    {
        Vector3 horizontalDirection = new Vector3(
            movementInput.x,
            0f,
            movementInput.y
        );

        moveDirection.x = horizontalDirection.x * speed;
        moveDirection.z = horizontalDirection.z * speed;
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded && velocityY < 0f)
        {
            velocityY = -0.5f;
        }

        velocityY += Physics.gravity.y * Time.deltaTime;

        moveDirection.y = velocityY;
    }

    private void Jump()
    {
        if (!characterController.isGrounded)
            return;

        velocityY = Mathf.Sqrt(
            jumpHeight * -2f * Physics.gravity.y
        );
    }

    public void Teleport(Vector3 position)
    {
        characterController.enabled = false;
        position.y = transform.position.y;
        this.transform.position = position;
        
        characterController.enabled = true;
    }

    private void OnDestroy()
    {
        controllers.Player.Move.performed -= OnMove;
        controllers.Player.Move.canceled -= OnMove;
        controllers.Player.Jump.performed -= OnJump;
    }
}