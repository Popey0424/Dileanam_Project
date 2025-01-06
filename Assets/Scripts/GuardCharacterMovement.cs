using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCharacterMovement : MonoBehaviour
{
    [Header("ScriptsReference")]
    public OpenDoor S_openDoor;


    [Header("MovementSettings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector3 inputDirection;

    [Header("Possesion")]
    [SerializeField] public bool PosseseGuard = false;

    [Header("DoorSettings")]
    [SerializeField] private bool isInRange = false;
    [SerializeField] private bool canOpenDoor = false;


    [Header("KeyInteracte")]
    public KeyCode OpenDoor;
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(OpenDoor) && canOpenDoor && S_openDoor.thisDoorIsOpen == false)
        {
            OpenLockedDoor();
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (PosseseGuard == true)
        {
            Movement();
        }
    }


    private void Movement()
    {
      
        if (inputDirection.magnitude >= 0.1f)
        {
                float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
                float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);

                transform.rotation = Quaternion.Euler(0f, angle, 0f);


                transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.GetComponent<OpenDoor>()!= null)
        {
            Debug.Log("Collision avec Porte");
            isInRange = true;
            canOpenDoor = true;
          
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<OpenDoor>() != null)
        {
            Debug.Log("Leave de la collision avec Porte");
            isInRange = false;
            canOpenDoor = false;
        }
    }

    private void OpenLockedDoor()
    {
      
        Debug.Log("Porte en cours d'ouverture...");
        S_openDoor.StartDoorAnimation();
    }
}
