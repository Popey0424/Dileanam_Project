using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianCharacterController : MonoBehaviour
{
    [Header("MovementSettings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector3 inputDirection;

    [Header("Possession")]
    [SerializeField] public bool PosseseMagician = false;

    [Header("Interaction Settings")]
    [SerializeField] private KeyCode interactKey = KeyCode.E;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private Color wallInteractColor = Color.blue;
    [SerializeField] private float interactRange = 2.0f;

    [Header("Interaction Text")]
    [SerializeField] private GameObject textSpawn;

    [Header("Player Y Settings")]
    [SerializeField] private GameObject playerY;

    private GameObject lastInteractedWall;
 

    private void Start()
    {
        textSpawn.SetActive(false); 
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector3(horizontal, 0, vertical).normalized;

        if (PosseseMagician == true)
        {
            Movement();
            HandleInteraction();
            
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

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactRange, interactableLayer))
            {
                GameObject wall = hit.collider.gameObject;
                Renderer renderer = wall.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = wallInteractColor;
                }
                AllowPlayerYToPassThrough(wall);
            }
        }
    }

   

    private void AllowPlayerYToPassThrough(GameObject wall)
    {
        Collider wallCollider = wall.GetComponent<Collider>();

        if (wallCollider != null)
        {
            Physics.IgnoreCollision(playerY.GetComponent<Collider>(), wallCollider, true);
            lastInteractedWall = wall;
            Invoke(nameof(ResetWallCollision), 5f);
        }
    }

    private void ResetWallCollision()
    {
        if (lastInteractedWall != null)
        {
            Collider wallCollider = lastInteractedWall.GetComponent<Collider>();
            if (wallCollider != null)
            {
                Physics.IgnoreCollision(playerY.GetComponent<Collider>(), wallCollider, false);
            }
            lastInteractedWall = null;
        }
    }
}