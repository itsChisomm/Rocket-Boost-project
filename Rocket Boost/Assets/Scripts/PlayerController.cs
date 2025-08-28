using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;

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
            StartThrusting();
        }

        else
        {
            audioSource.Stop();
            mainBooster.Stop(); // stop particle system
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);

        if (!audioSource.isPlaying)  // so it doesn't layer 
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0f) // A (left) is pressed 
        {
            ApplyRotation(rotationForce); // positive rotation around z-axis (z is positive when rotating left)

            if (!rightBooster.isPlaying)
            {
                leftBooster.Stop();
                rightBooster.Play();
            }
        }

        else if (rotationInput > 0f) // D (right) is pressed

        {
            ApplyRotation(-rotationForce); // negative rotation around z-axis (z is negative when rotating right)

            if (!leftBooster.isPlaying) 
            {
                rightBooster.Stop();    
                leftBooster.Play();
            }
            
        }
        else
        {
            leftBooster.Stop();
            rightBooster.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {  
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame  * Time.fixedDeltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
