using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;


    [SerializeField] private float thrustForce = 100f;
    [SerializeField] private float rotationForce = 100f;

    AudioSource audioSource;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    /* private void OnDisable()
     {
         thrust.Disable();
         rotation.Disable();
     } */

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);

            if (!audioSource.isPlaying)  // so it doesn't layer 
            {
                audioSource.Play();
            }
        }

        else
        {
            audioSource.Stop();
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0f) // A (left) is pressed 
        {
            ApplyRotation(rotationForce); // positive rotation around z-axis (z is positive when rotating left)
        }

        else if (rotationInput > 0f) // D (right) is pressed

        {
            ApplyRotation(-rotationForce); // negative rotation around z-axis (z is negative when rotating right)
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {  
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame  * Time.fixedDeltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
