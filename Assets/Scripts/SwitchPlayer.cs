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
    private float Speed = 10f;
    public GameObject PlayerX;

    [Header("PlayersAnimator")]
    private Animator animatorPlayerX;
    private Animator animatorPlayerY;

    [Header("Camera Reference")]
    [SerializeField] private CameraFollow S_cameraFollow;
    
    private Rigidbody rb;
    private Vector3 ReturnPlayer;
    private bool isComingBack= false;
    private bool isArrived= false;

    private void Start()
    {

        playerMovement = GetComponent<PlayerMovement>();
        CurrentEtat = Player.Etat.PlayerX;

        animatorPlayerX = PlayerX.GetComponent<Animator>();
        PlayerY.SetActive(false);

        rb = PlayerY.GetComponent<Rigidbody>();



    }

    private void Update()
    {
        if (Input.GetKeyDown(switchMode))
        {
            SwitchState();
        }
        if (isComingBack == true)
        {
            ReturnPlayer = new Vector3(PlayerX.transform.position.x, PlayerX.transform.position.y, PlayerX.transform.position.z);
            PlayerY.transform.position = Vector3.MoveTowards(PlayerY.transform.position, ReturnPlayer, Speed * Time.deltaTime);
            if (PlayerY.transform.position == PlayerX.transform.position)
            {
                isComingBack = false;
                isArrived = true;
            }
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
            PlayerX.GetComponent<Rigidbody>().isKinematic = true;
            PlayerX.GetComponent<PlayerMovement>().enabled = false;
            Debug.Log("Ca marche, ccc");
            S_cameraFollow.UpdateTarget(PlayerY.transform);
        }
        else if (CurrentEtat == Player.Etat.PlayerY)
        {
          
            CurrentEtat = Player.Etat.PlayerYReturn;
            Debug.Log("Le joueur return");
        }
        if(CurrentEtat == Player.Etat.PlayerYReturn)
        {
            isComingBack = true;
            if(isArrived == true)
            {
                CurrentEtat = Player.Etat.PlayerX;
                animatorPlayerX.SetBool("Die",false);
                PlayerX.GetComponent<Rigidbody>().isKinematic = true;
                PlayerX.GetComponent <PlayerMovement>().enabled = true;
                PlayerY.SetActive(false);
                Debug.Log("Ca marche");
                isArrived = false;
                S_cameraFollow.UpdateTarget(PlayerX.transform);
            }

           
        }

        
        else if(CurrentEtat == Player.Etat.InPossesion)
        {
            
            Debug.Log("Ne peut pas revenir en PlayerX ");
            //Ajouter le passage au player Y;
        }
    }

    

}


