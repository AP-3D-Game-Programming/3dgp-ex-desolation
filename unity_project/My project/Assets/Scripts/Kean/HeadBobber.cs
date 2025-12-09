using UnityEngine;
using System; // Nodig voor het Action event

public class HeadBobber : MonoBehaviour
{
    // *** FOOTSTEP EVENT ***
    // Dit is de publieke 'zender' waar andere scripts naar luisteren.
    public static event Action OnFootstep; 
    
    [Header("Settings")]
    [SerializeField] private float bobSpeed = 14f;
    [SerializeField] private float bobAmount = 0.05f;
    
    private float defaultPosY = 0;
    private float timer = 0;
    
    // Controlemechanisme om te voorkomen dat het event constant wordt getriggerd
    private bool stepTriggered = false; 

    void Start()
    {
        defaultPosY = transform.localPosition.y;
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // Alleen bobben als de speler beweegt
        if (Mathf.Abs(inputX) > 0.1f || Mathf.Abs(inputZ) > 0.1f)
        {
            timer += Time.deltaTime * bobSpeed;

            float newY = defaultPosY + Mathf.Sin(timer) * bobAmount;
            
            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);

            // *** STAP TRIGGER LOGICA ***
            // Trigger een stap wanneer de sinusgolf de top of de bodem nadert.
            if (Mathf.Abs(Mathf.Sin(timer)) > 0.9f && !stepTriggered)
            {
                // Roep het event op zodat de bewegingscontroller het weet.
                OnFootstep?.Invoke(); 
                stepTriggered = true;
            }
            // Reset de trigger wanneer de sinus de nul-lijn passeert (tussen de stappen door).
            else if (Mathf.Abs(Mathf.Sin(timer)) < 0.1f)
            {
                stepTriggered = false;
            }
        }
        else
        {
            // Reset timer en keer terug naar de standaardpositie
            timer = 0;
            stepTriggered = false; 
            
            Vector3 targetPos = new Vector3(transform.localPosition.x, defaultPosY, transform.localPosition.z);
            
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * 10f);
        }
    }
}