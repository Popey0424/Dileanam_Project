using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossesionScript : MonoBehaviour
{
    private BoxCharacterMovement boxCharacterMovement;

    float detectionRange = 1f;
    public LayerMask InteractableLayer;
    public KeyCode Interactable;

    [Header("Debug")]
    [SerializeField] private bool IsInRange = false;
    [SerializeField] private bool ChangeTo = false;

    void Start()
    {
        
    }

  
    void Update()
    {
        
        if (Input.GetKeyDown(Interactable) && IsInRange == true)
        {
            Debug.Log("Possesion en cours...");
        }
        
    }

      
    
    #region possesion Box
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Box"))
        {
            Debug.Log("Collision avec Box Detecter");
            IsInRange = true;
        }
    }
    #endregion
}
