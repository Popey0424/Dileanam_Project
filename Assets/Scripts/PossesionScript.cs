using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class PossesionScript : MonoBehaviour
{

    [Header("Scripts Reference")]
    public BoxCharacterMovement S_boxCharacterMovement;
    public GuardCharacterMovement S_guardCharacterMovement;
    public SwitchPlayer S_switchplayer;
    public CameraFollow S_cameraFollow;

    [Header("Collision Settings")]
    public LayerMask InteractableLayer;
    public KeyCode Interactable;
    
    [SerializeField] private GameObject playerY;
    [SerializeField] private GameObject playerYSkin;
   

    [Header("Classes")]
    public PossesionPossibility.Possession CurrentPossession;

    [Header("BoxSettings")]
    [SerializeField] private GameObject box;
    [SerializeField] private bool canChangeToBox = false;

    [Header("GuardSettigns")]
    [SerializeField] private GameObject guard;
    [SerializeField] private bool canChangeToGuard = false;

    [Header("Debug")]
    [SerializeField] private bool isInRange = false;
    [SerializeField] private bool actualyInPossese = false;
    private int cpt = 1;

    


 
    

    void Start()
    {
        
    }

  
    void Update()
    {
        if (Input.GetKeyDown(Interactable))
        {
            switchEtatPossession();
        }

        //if (Input.GetKeyDown(Interactable) && isInRange == true) //  touche apuyer et dans la range
        //{
        //    Debug.Log("Possesion en cours...");
        //    if(canChangeToBox == true && actualyInPossese == false)
        //    {
        //        ChangeToBox();
        //    }
        //    if (actualyInPossese == true)
        //    {
        //        GoBackToPlayerY();
        //    }
        //}
        
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<BoxCharacterMovement>() != null)
        {
            Debug.Log("Collision avec Box Detecter");
            isInRange = true;
            canChangeToBox = true;
        }
        if (collider.GetComponent<GuardCharacterMovement>() != null)
        {
            Debug.Log("Collision avec Guard Detecter");
            isInRange = true;
            canChangeToGuard = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<BoxCharacterMovement>() != null)
        {
            Debug.Log("Leave la collision Box");
            isInRange = false;
            canChangeToBox = false;
        }
        if (collider.GetComponent<GuardCharacterMovement>() != null)
        {
            Debug.Log("Leave la collision Guard");
            isInRange = false;
            canChangeToGuard = false;
        }
    }

    #region PossesionBox
    private void ChangeToBox()
    {
        Debug.Log("Player Y devient Box");
        playerY.GetComponent<PlayerMovement>().enabled = false;
        playerYSkin.SetActive(false);
        S_switchplayer.CurrentEtat = Player.Etat.InPossesion;
        Debug.Log(S_switchplayer.CurrentEtat);
        CurrentPossession = PossesionPossibility.Possession.Box;
        S_boxCharacterMovement.PosseseBox = true;
        canChangeToBox=false;
    }
    #endregion



    #region possesion Guard

    private void ChangeToGuard()
    {
        Debug.Log("Player Y devient Guard");
        playerY.GetComponent<PlayerMovement>().enabled = false;
        playerYSkin.SetActive(false);
        S_switchplayer.CurrentEtat = Player.Etat.InPossesion;
        Debug.Log(S_switchplayer.CurrentEtat);
        CurrentPossession = PossesionPossibility.Possession.Guard;
        S_guardCharacterMovement.PosseseGuard = true;
        canChangeToGuard = false;
        S_cameraFollow.UpdateTarget(guard.transform);

    }
    #endregion

    private void GoBackToPlayerY()
    {
        if (CurrentPossession == PossesionPossibility.Possession.Box)
        {
            Debug.Log("Box Devient Player Y");
            playerY.GetComponent<PlayerMovement>().enabled = true;
            playerYSkin.SetActive(true);
            playerY.transform.position = box.transform.position + new Vector3(0, -0.5f, 0);
            S_switchplayer.CurrentEtat = Player.Etat.PlayerY;
            CurrentPossession = PossesionPossibility.Possession.PlayerY;
            S_boxCharacterMovement.PosseseBox = false;
            S_cameraFollow.UpdateTarget(playerY.transform);

        }
        if (CurrentPossession == PossesionPossibility.Possession.Guard)
        {
            Debug.Log("Guard Devient Player Y");
            playerY.GetComponent <PlayerMovement>().enabled = true;
            playerYSkin.SetActive(true);
            playerY.transform.position = guard.transform.position + new Vector3(0, 0f, 0);
            S_switchplayer.CurrentEtat = Player.Etat.PlayerY;
            CurrentPossession = PossesionPossibility.Possession.PlayerY;
            S_guardCharacterMovement.PosseseGuard = false;
            S_cameraFollow.UpdateTarget(playerY.transform);
        }
       
    }

    private void switchEtatPossession()
    {
        if (isInRange && CurrentPossession == PossesionPossibility.Possession.PlayerY && canChangeToBox == true)
        {
            ChangeToBox();
        }
        else if (CurrentPossession == PossesionPossibility.Possession.Box)
        {

            Debug.Log("Retransformation");
            GoBackToPlayerY();
        }
        if (isInRange && CurrentPossession == PossesionPossibility.Possession.PlayerY && canChangeToGuard == true)
        {
            ChangeToGuard();
        }
        else if (CurrentPossession == PossesionPossibility.Possession.Guard)
        {
            GoBackToPlayerY();
        }
    }
}




[System.Serializable]
public class PossesionPossibility
{
    public enum Possession { PlayerY, Box, Guard};
}
