using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float sprintSpeed = 6f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 5f;
    public float gravity = 9.81f;
    public int health = 100;
    private CharacterController controller;
    private Transform playerCamera;
    private float playerSpeed;
    private float rotationX = 0f;
    private Vector3 velocity;
    private bool isGrounded;
    private int hitCount = 0;
    private bool isWounded = false;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        playerSpeed = walkSpeed;
    }
    void Update()
    {
        Move();
        RotateView();
    }
    void Move()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * playerSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
        }
        if (!isWounded)
        {
            playerSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;
        }
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    void RotateView()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -75f, 75f);
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        hitCount++;
        if (hitCount >= 10)
        {
            isWounded = true;
            playerSpeed = walkSpeed / 2;
        }
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Игрок умер!");
    }
}