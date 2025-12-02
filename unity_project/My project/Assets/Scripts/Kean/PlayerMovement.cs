using UnityEngine;

public class First_Person_Movement : MonoBehaviour
{
    private Vector3 Velocity;
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private bool Sneaking = false;
    private float xRotation;

    [Header("Components Needed")]
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private CharacterController Controller;
    [SerializeField] private Transform Player;
    [Space]
    [Header("Movement")]
    [SerializeField] private float Speed;
    [SerializeField] private float SprintSpeedIncrease;
    [SerializeField] private float JumpForce;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float Gravity = 9.81f;
    [Space]
    [Header("Sneaking")]
    [SerializeField] private bool Sneak = false;
    [SerializeField] private float SneakSpeed;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MoveCamera();

        if (Input.GetKey(KeyCode.LeftControl) && Sneak)
        {
            Player.localScale = new Vector3(1f, 0.5f, 1f);
            Sneaking = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            Player.localScale = new Vector3(1f, 1f, 1f);
            Sneaking = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Speed += SprintSpeedIncrease;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed -= SprintSpeedIncrease;
        }

    }
    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput);


        if (Controller.isGrounded)
        {
            Velocity.y = -1f;

            if (Input.GetKeyDown(KeyCode.Space) && Sneaking == false)
            {
                Velocity.y = JumpForce;
            }
        }
        else
        {
            Velocity.y += Gravity * -2f * Time.deltaTime;
        }
        if (Sneaking)
        {
            Controller.Move(MoveVector * SneakSpeed * Time.deltaTime);
        }
        else
        {
            Controller.Move(MoveVector * Speed * Time.deltaTime);

        }
        Controller.Move(Velocity * Time.deltaTime);

    }
    private void MoveCamera()
    {
        xRotation -= PlayerMouseInput.y * Sensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void WobbleCamera()
    {
        // Camera wobble logic would go here

    }
}