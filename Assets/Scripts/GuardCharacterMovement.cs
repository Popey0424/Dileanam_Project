using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCharacterMovement : MonoBehaviour
{


    [Header("MovementSettings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector3 inputDirection;

    [Header("Possesion")]
    [SerializeField] public bool PosseseGuard = false;

    [Header("KeyInteracte")]
    public KeyCode OpenDoor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()


    {
        
    }
}
