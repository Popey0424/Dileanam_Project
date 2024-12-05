using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitchPlayer : MonoBehaviour
{



    [Header("Scripts Reference")]
    private PlayerMovement playerMovement;

    [Header("SwitchPlayer Settings")]
    [SerializeField] private KeyCode switchMode;
    public Player.Etat CurrentEtat;
    public GameObject PlayerY;
    public GameObject PlayerX;

    [Header("PlayersAnimator")]
    private Animator animatorPlayerX;
    private Animator animatorPlayerY;


    private void Start()
    {

        playerMovement = GetComponent<PlayerMovement>();
        CurrentEtat = Player.Etat.PlayerX;

        animatorPlayerX = PlayerX.GetComponent<Animator>();
        PlayerY.SetActive(false);


    }

    private void Update()
    {
        if (Input.GetKeyDown(switchMode))
        {
            SwitchState();
        }
    }

    private void SwitchState()
    {
        if (CurrentEtat == Player.Etat.PlayerX)
        {
            CurrentEtat = Player.Etat.PlayerY;
            animatorPlayerX.SetBool("Die", true);
            PlayerY.SetActive(true);
            PlayerY.transform.position = PlayerX.transform.position;
            //playerX.GetComponent<Collider>().enabled = false;
            PlayerX.GetComponent<Rigidbody>().isKinematic = true;
            PlayerX.GetComponent<PlayerMovement>().enabled = false;
            Debug.Log("Ca marche");
        }
        else if (CurrentEtat == Player.Etat.PlayerY)
        {
            
            CurrentEtat = Player.Etat.PlayerX;
            animatorPlayerX.SetBool("Die",false);
            //playerX.GetComponent<Collider>().enabled = false;
            PlayerX.GetComponent<Rigidbody>().isKinematic = true;
            PlayerX.GetComponent <PlayerMovement>().enabled = true;
            PlayerY.SetActive(false);
            Debug.Log("Ca marche");
        }
        else if(CurrentEtat == Player.Etat.InPossesion)
        {
            //CurrentEtat = Player.Etat.PlayerX;
            Debug.Log("Ne peut pas revenir en PlayerX ");
            //Ajouter le passage au player Y;
        }
    }

}


