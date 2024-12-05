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
            //playerX.GetComponent<Collider>().enabled = false;
            PlayerX.GetComponent<Rigidbody>().isKinematic = true;
            PlayerX.GetComponent<PlayerMovement>().enabled = false;
            Debug.Log("Ca marche, ccc");
        }
        else if (CurrentEtat == Player.Etat.PlayerY)
        {
            //StartCoroutine(PlayerYReturn());
            CurrentEtat = Player.Etat.PlayerYReturn;
            Debug.Log("Le joueur return");
            //animatorPlayerX.SetBool("Die",false);
            ////playerX.GetComponent<Collider>().enabled = false;
            //PlayerX.GetComponent<Rigidbody>().isKinematic = true;
            //PlayerX.GetComponent <PlayerMovement>().enabled = true;
            //PlayerY.SetActive(false);
            //Debug.Log("Ca marche");
        }
        if(CurrentEtat == Player.Etat.PlayerYReturn)
        {
            isComingBack = true;
            if(isArrived == true)
            {
                CurrentEtat = Player.Etat.PlayerX;
                animatorPlayerX.SetBool("Die",false);
                //playerX.GetComponent<Collider>().enabled = false;
                PlayerX.GetComponent<Rigidbody>().isKinematic = true;
                PlayerX.GetComponent <PlayerMovement>().enabled = true;
                PlayerY.SetActive(false);
                Debug.Log("Ca marche");
                isArrived = false;
            }

           
        }

        
        else if(CurrentEtat == Player.Etat.InPossesion)
        {
            //CurrentEtat = Player.Etat.PlayerX;
            Debug.Log("Ne peut pas revenir en PlayerX ");
            //Ajouter le passage au player Y;
        }
    }

    //IEnumerator PlayerYReturn()
    //{
    //    rb.AddForce(0,10, 0, ForceMode.Impulse );
    //    Debug.Log("Est ce que ca amrche ");
    //    yield return new WaitUntil (() => PlayerY.transform.position != PlayerX.transform.position);
    //    Debug.Log("Apparement il est la");
    //}

}


