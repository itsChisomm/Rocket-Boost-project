using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    bool isThrusting = false;

    private void OnEnable()
    {
        thrust.Enable();
    }   
   
    private void OnDisable()
    {
        thrust.Disable();
    }

    void Update()
    {
       if (thrust.IsPressed())
       {
           isThrusting = true;
           Debug.Log("Thrusting");
       }
       else
       {
           isThrusting = false;
        }
    }
}
