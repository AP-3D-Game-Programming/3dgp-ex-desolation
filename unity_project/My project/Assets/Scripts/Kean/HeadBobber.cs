using UnityEngine;

public class HeadBobber : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float bobSpeed = 14f;
    [SerializeField] private float bobAmount = 0.05f;
    
    private float defaultPosY = 0;
    private float timer = 0;

    void Start()
    {
        defaultPosY = transform.localPosition.y;
    }

    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        if (Mathf.Abs(inputX) > 0.1f || Mathf.Abs(inputZ) > 0.1f)
        {

            timer += Time.deltaTime * bobSpeed;


            float newY = defaultPosY + Mathf.Sin(timer) * bobAmount;

            transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);
        }
        else
        {

            timer = 0;
            
            Vector3 targetPos = new Vector3(transform.localPosition.x, defaultPosY, transform.localPosition.z);
            
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * 10f);
        }
    }
}