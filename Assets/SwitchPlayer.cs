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


    private void Start()
    {

        playerMovement = GetComponent<PlayerMovement>();
        currentEtat = Player.Etat.PlayerX;
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
            playerY.SetActive(true);
            playerY.transform.position = playerX.transform.position;
            playerX.SetActive(false);
            Debug.Log("Ca marche");
        }
        else if (currentEtat == Player.Etat.PlayerY)
        {
            currentEtat = Player.Etat.PlayerX;
            playerX.SetActive(true);
            playerY.SetActive(false);
            Debug.Log("Ca marche");
        }
    }

}


