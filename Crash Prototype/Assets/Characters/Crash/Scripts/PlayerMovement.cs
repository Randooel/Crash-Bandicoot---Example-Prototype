using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    Animator animator;

    [Header("Movement")]
    public float moveSpeed = 5f;
    private CharacterController controller;

    [Header("Rotation")]
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    [SerializeField] Transform visual;

    [Header("Jump")]
    public float gravity = -9.81f;
    public float jumpHeight = 2;
    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Calculates how much the player needs to rotate
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            // Smoothes the rotation
            float angle = Mathf.SmoothDampAngle(visual.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            // Rotate
            visual.rotation = Quaternion.Euler(0f, angle, 0f);

            // Move
            controller.Move(direction * moveSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}