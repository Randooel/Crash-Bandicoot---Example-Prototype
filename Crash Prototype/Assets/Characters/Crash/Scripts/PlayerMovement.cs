using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class PlayerMovement : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    // public / private / protected

    [Header("Movement")]
    public float moveSpeed = 5f;
    private CharacterController controller;

    [Header("Rotation")]
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    [SerializeField] Transform visual;

    [Header("Jump")]
    public float gravity = -9.81f;
    [Range(1, 10)] public float jumpHeight = 2;
    Vector3 velocity;

    [Space(10)]
    public LayerMask groundMask;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    bool isGrounded;

    private Vector3 startPos;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        startPos = transform.position;
    }

    void Update()
    {
        Move();
        Jump();

        if(Input.GetKeyDown(KeyCode.R))
        {
            transform.position = startPos;
        }
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

    protected abstract void Attack();
    protected virtual void EspecialAttack()
    {
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("EspecialAttack");
        }
    }
}