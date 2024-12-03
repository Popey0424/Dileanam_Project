using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCharacterMovement : MonoBehaviour
{

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector3 inputDirection;


    [Header("Possesion")]
    [SerializeField] public bool PosseseBox = false;

   
    void Start()
    {
        
    }

    void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (PosseseBox == true)
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
}
