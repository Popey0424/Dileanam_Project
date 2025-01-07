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

    [Header("Camera Reference")]
    [SerializeField] private CameraFollow S_cameraFollow;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        CurrentEtat = Player.Etat.PlayerX;

        animatorPlayerX = PlayerX.GetComponent<Animator>();
        animatorPlayerY = PlayerY.GetComponent<Animator>();

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
            PlayerX.GetComponent<Collider>().enabled = false;
            PlayerY.transform.position = PlayerX.transform.position + new Vector3(0.5f,0,0);           
            PlayerX.GetComponent<Rigidbody>().isKinematic = true;
            PlayerX.GetComponent<PlayerMovement>().enabled = false;            
            PlayerY.GetComponent<Rigidbody>().isKinematic = false;
            PlayerY.GetComponent<PlayerMovement>().enabled = true;
            PlayerY.GetComponent<Collider>().enabled = true;

            S_cameraFollow.UpdateTarget(PlayerY.transform);
        }
        else if (CurrentEtat == Player.Etat.PlayerY)
        {
            CurrentEtat = Player.Etat.PlayerX;
            animatorPlayerX.SetBool("Die", false);
            PlayerY.SetActive(false);
            PlayerX.GetComponent<Rigidbody>().isKinematic = false;
            PlayerX.GetComponent<PlayerMovement>().enabled = true;
            PlayerX.GetComponent<Collider>().enabled = true;
            PlayerY.GetComponent<Rigidbody>().isKinematic = true;
            PlayerY.GetComponent<PlayerMovement>().enabled = false;
            PlayerY.GetComponent<Collider>().enabled = false;

            S_cameraFollow.UpdateTarget(PlayerX.transform);
        }
    }
}