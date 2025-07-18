using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] Transform visual;

    public float moveSpeed = 5f;

    Animator animator;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
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
}