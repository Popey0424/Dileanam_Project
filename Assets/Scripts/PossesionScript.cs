using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
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
    [SerializeField] private GameObject playerYSkin;
    [SerializeField] private GameObject box;

    [Header("Classes")]
    public PossesionPossibility.Possession CurrentPossession;



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


    #region Possesion Statue

    #endregion




    #region possesion Box
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<BoxCharacterMovement>() != null)
        {
            Debug.Log("Collision avec Box Detecter");
            isInRange = true;
            canChangeToBox = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<BoxCharacterMovement>() != null)
        {
            Debug.Log("Leave collision");
            isInRange = false;
            canChangeToBox = false;
        }
    }

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

    private void GoBackToPlayerY()
    {

        Debug.Log("Box Devient Player Y");
        playerY.GetComponent<PlayerMovement>().enabled = true;
        playerYSkin.SetActive(true);
        playerY.transform.position = box.transform.position + new Vector3(0,-0.5f,0);
        S_switchplayer.CurrentEtat = Player.Etat.PlayerY;
        CurrentPossession = PossesionPossibility.Possession.PlayerY;
        S_boxCharacterMovement.PosseseBox = false;
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
    }
}




[System.Serializable]
public class PossesionPossibility
{
    public enum Possession { PlayerY, Box};
}
