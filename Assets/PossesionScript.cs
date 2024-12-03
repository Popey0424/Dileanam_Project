using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossesionScript : MonoBehaviour
{

    [Header("Scripts Reference")]
    public BoxCharacterMovement S_boxCharacterMovement;
    public SwitchPlayer S_switchplayer;

    [Header("Collision Settings")]
    public LayerMask InteractableLayer;
    public KeyCode Interactable;
    [SerializeField] private bool canChangeToBox = false;
    [SerializeField] private GameObject playerY;

    [Header("Classes")]
    public PossesionPossibility.Possession CurrentPossession;



    [Header("Debug")]
    [SerializeField] private bool isInRange = false;


 
    

    void Start()
    {
        
    }

  
    void Update()
    {
        
        if (Input.GetKeyDown(Interactable) && isInRange == true) //  touche apuyer et dans la range
        {
            Debug.Log("Possesion en cours...");
            if(canChangeToBox == true)
            {
                ChangeToBox();
            }
        }
        
    }


    #region Possesion Statue

    #endregion


    #region possesion Box
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Box"))
        {
            Debug.Log("Collision avec Box Detecter");
            isInRange = true;
            canChangeToBox = true;
        }
    }

    private void ChangeToBox()
    {
        if(CurrentPossession == PossesionPossibility.Possession.PlayerY)
        {
            Debug.Log("Player Y devient Box");
            playerY.SetActive(false);
            S_switchplayer.CurrentEtat = Player.Etat.InPossesion;
            Debug.Log(S_switchplayer.CurrentEtat);
            CurrentPossession = PossesionPossibility.Possession.Box;
            S_boxCharacterMovement.PosseseBox = true;

        }
    }
    #endregion
}




[System.Serializable]
public class PossesionPossibility
{
    public enum Possession { PlayerY, Box};
}
