using UnityEngine;

public class First_Person_Movement : MonoBehaviour
{
    private Vector3 Velocity;
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRotation;

    // Variabelen om de originele grootte te onthouden
    private float originalHeight;
    private Vector3 originalCenter;

    [Header("Components Needed")]
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private CharacterController Controller;
    // [SerializeField] private Transform Player; // <-- NIET NODIG als het script op de speler staat

    [Space]
    [Header("Movement")]
    [SerializeField] private float Speed;
    [SerializeField] private float SprintSpeedIncrease;
    [SerializeField] private float JumpForce; // Iets verhoogd voor test
    [SerializeField] private float Sensitivity;
    [SerializeField] private float Gravity = -9.81f; // Let op: Gravity is meestal negatief in berekeningen

    [Space]
    [Header("Sneaking")]
    [SerializeField] private bool CanSneak = true; // Hernoemd voor duidelijkheid
    [SerializeField] private bool IsSneaking = false;
    [SerializeField] private float SneakSpeed;
    [SerializeField] private float CrouchHeight; // Hoe klein word je?

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // Sla de standaard waarden op bij het starten
        originalHeight = Controller.height;
        originalCenter = Controller.center;
    }

    void Update()
    {
        // Input ophalen
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        MovePlayer();
        MoveCamera();
        HandleCrouch(); // Bukken apart gezet voor overzicht
        HandleSprint(); // Sprinten apart gezet
    }

    private void HandleCrouch()
    {
        // Alleen bukken als we dat mogen (CanSneak)
        if (Input.GetKeyDown(KeyCode.LeftControl) && CanSneak)
        {
            IsSneaking = true;
            
            // FIX 1: Pas de Controller aan in plaats van Scale
            Controller.height = CrouchHeight;
            // Verplaats het middenpunt zodat de voeten op de grond blijven
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            IsSneaking = false;

            // Zet alles terug naar normaal
            Controller.height = originalHeight;
            Controller.center = originalCenter;
        }
    }

    private void HandleSprint()
    {
        // Je mag alleen sprinten als je NIET bukt
        if (Input.GetKeyDown(KeyCode.LeftShift) && !IsSneaking)
        {
            Speed += SprintSpeedIncrease;
        }
        
        // Als je shift loslaat OF als je begint met bukken terwijl je sprint
        if (Input.GetKeyUp(KeyCode.LeftShift) || (IsSneaking && Input.GetKey(KeyCode.LeftShift)))
        {
            // Zorg dat we niet oneindig vertragen als we per ongeluk dubbel checken
            // (Een simpele reset naar basis snelheid is vaak veiliger)
             // Maar voor jouw logica:
             Speed -= SprintSpeedIncrease; 
        }
    }

    private void MovePlayer()
    {
        // 1. Richting bepalen (Lokaal naar Wereld)
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput);

        // 2. Zwaartekracht logic
        if (Controller.isGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f; // Zorgt dat je stevig op de grond blijft
        }

        // Springen
        if (Input.GetKeyDown(KeyCode.Space) && Controller.isGrounded && !IsSneaking)
        {
            Velocity.y = Mathf.Sqrt(JumpForce * -2f * Gravity);  
        }

        // Zwaartekracht opbouwen
        
        Velocity.y += Gravity * Time.deltaTime;

        // 3. BEWEGEN (Dit is de fix!)
        
        float currentSpeed = IsSneaking ? SneakSpeed : Speed;
        
        // A. Horizontale beweging
        Controller.Move(MoveVector * currentSpeed * Time.deltaTime);
        
        // B. Verticale beweging (FIX 2: Deze miste je!)
        Controller.Move(Velocity * Time.deltaTime);
    }

    private void MoveCamera()
    {
        xRotation -= PlayerMouseInput.y * Sensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}