using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitchPlayer : MonoBehaviour
{
    //public enum Etat { PlayerX, PlayerY }
    [SerializeField] private KeyCode switchMode;
    private PlayerMovement playerMovement;
    private Player.Etat currentEtat;
    public GameObject playerY;
    public GameObject playerX;

    [Header("PlayersAnimator")]
    private Animator animatorPlayerX;
    private Animator animatorPlayerY;


    private void Start()
    {

        playerMovement = GetComponent<PlayerMovement>();
        currentEtat = Player.Etat.PlayerX;

        animatorPlayerX = playerX.GetComponent<Animator>();
        playerY.SetActive(false);


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
        if (currentEtat == Player.Etat.PlayerX)
        {
            currentEtat = Player.Etat.PlayerY;
            animatorPlayerX.SetBool("Die", true);
            playerY.SetActive(true);
            playerY.transform.position = playerX.transform.position;
            playerX.GetComponent<Rigidbody>().isKinematic = true;
            playerX.GetComponent<PlayerMovement>().enabled = false;
            Debug.Log("Ca marche");
        }
        else if (currentEtat == Player.Etat.PlayerY)
        {
            
            currentEtat = Player.Etat.PlayerX;
            animatorPlayerX.SetBool("Die",false);
            playerX.GetComponent<Rigidbody>().isKinematic = true;
            playerX.GetComponent <PlayerMovement>().enabled = true;
            playerY.SetActive(false);
            Debug.Log("Ca marche");
        }
    }

}


