using UnityEngine;

public class First_Person_Movement : MonoBehaviour
{
    private Vector3 Velocity;
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRotation;
    
    private float originalHeight;
    private Vector3 originalCenter;

    // Slaat de magnitude van de bewegingsinput op voor de voetstapcheck
    private float movementMagnitude; 

    [Header("Components Needed")]
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private CharacterController Controller;
    
    [Space]
    [Header("Movement")]
    [SerializeField] private float Speed;
    [SerializeField] private float SprintSpeedIncrease;
    [SerializeField] private float JumpForce; 
    [SerializeField] private float Sensitivity;
    [SerializeField] private float Gravity = -9.81f; 

    [Space]
    [Header("Sneaking")]
    [SerializeField] private bool CanSneak = true; 
    [SerializeField] private bool IsSneaking = false;
    [SerializeField] private float SneakSpeed;
    [SerializeField] private float CrouchHeight; 

    [Space]
    [Header("Footsteps")]
    // Sleep hier de AudioSource component op de Player in
    [SerializeField] private AudioSource FootstepSource; 
    [Tooltip("De lange MP3-file die alle stappen bevat")]
    [SerializeField] private AudioClip MultiStepClip; 
    [Tooltip("De exacte duur van één enkele stap in seconden (bijv. 0.4 seconden)")]
    [SerializeField] private float SingleStepDuration = 0.4f; 
    [Tooltip("Hoeveel stappen zitten er in de MultiStepClip?")]
    [SerializeField] private int TotalStepsInClip = 5; 

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        originalHeight = Controller.height;
        originalCenter = Controller.center;
    }
    
    // *** EVENT ABONNEMENT ***
    void OnEnable()
    {
        // Luister naar het event van de HeadBobber
        HeadBobber.OnFootstep += OnStepTaken;
    }

    void OnDisable()
    {
        // Stop met luisteren wanneer het script gedeactiveerd wordt
        HeadBobber.OnFootstep -= OnStepTaken;
    }

    void Update()
    {
        // Input ophalen
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        // Sla de magnitude op voor de voetstapcheck
        movementMagnitude = PlayerMovementInput.magnitude; 

        MovePlayer();
        MoveCamera();
        HandleCrouch(); 
        HandleSprint(); 
    }

    private void HandleCrouch()
    {
        // ... (Logica blijft hetzelfde)
        if (Input.GetKeyDown(KeyCode.LeftControl) && CanSneak)
        {
            IsSneaking = true;
            Controller.height = CrouchHeight;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            IsSneaking = false;
            Controller.height = originalHeight;
            Controller.center = originalCenter;
        }
    }

    private void HandleSprint()
    {
        // ... (Logica blijft hetzelfde)
        if (Input.GetKeyDown(KeyCode.LeftShift) && !IsSneaking)
        {
            Speed += SprintSpeedIncrease;
        }
        
        if (Input.GetKeyUp(KeyCode.LeftShift) || (IsSneaking && Input.GetKey(KeyCode.LeftShift)))
        {
            Speed -= SprintSpeedIncrease; 
        }
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput);

        // Zwaartekracht logic
        if (Controller.isGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f; 
        }

        // Springen
        if (Input.GetKeyDown(KeyCode.Space) && Controller.isGrounded && !IsSneaking)
        {
            Velocity.y = Mathf.Sqrt(JumpForce);  
        }

        Velocity.y += Gravity * Time.deltaTime;

        // BEWEGEN
        float currentSpeed = IsSneaking ? SneakSpeed : Speed;
        Controller.Move(MoveVector * currentSpeed * Time.deltaTime);
        Controller.Move(Velocity * Time.deltaTime);
    }

    private void MoveCamera()
    {
        xRotation -= PlayerMouseInput.y * Sensitivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    // *** DE EVENT ONTVANGER ***
    private void OnStepTaken()
    {
        // 1. Controleer of we daadwerkelijk lopen (input > 0) en op de grond staan
        if (Controller.isGrounded && movementMagnitude > 0.1f)
        {
            // Speel alleen de stap als we niet al aan het afspelen zijn om overlap te voorkomen
            if (!FootstepSource.isPlaying)
            {
                 PlayFootstepSound();
            }
        }
    }

    private void PlayFootstepSound()
    {
        if (FootstepSource == null || MultiStepClip == null) return;
        
        // 1. Bepaal een willekeurige stap-index om af te spelen
        int randomStepIndex = UnityEngine.Random.Range(0, TotalStepsInClip);

        // 2. Bereken de Starttijd in seconden
        float startTime = randomStepIndex * SingleStepDuration;

        // 3. Pas de tijd van de AudioSource aan
        FootstepSource.clip = MultiStepClip;
        FootstepSource.time = startTime;

        // 4. Speel af met aanpassingen voor Sneaking
        FootstepSource.pitch = IsSneaking ? 
            UnityEngine.Random.Range(0.8f, 1.0f) : 
            UnityEngine.Random.Range(0.9f, 1.1f);
            
        FootstepSource.volume = IsSneaking ? 0.4f : 1.0f;
        
        FootstepSource.Play();
        
        // 5. Stop na de duur van één stap
        Invoke("StopPlayingStep", SingleStepDuration);
    }

    private void StopPlayingStep()
    {
        if (FootstepSource != null && FootstepSource.isPlaying)
        {
            FootstepSource.Stop();
        }
    }
}