using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;


    [SerializeField] private float thrustForce = 100f;
    [SerializeField] private float rotationForce = 100f;


    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();

        if (rotationInput < 0f)
        {
            ApplyRotation(rotationForce);
        }

        else if (rotationInput > 0f)

        {
            ApplyRotation(-rotationForce);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.AddRelativeTorque(Vector3.forward * rotationThisFrame  * Time.fixedDeltaTime);
    }
}
