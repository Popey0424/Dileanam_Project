using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    //[Header("Interaction UI")]
    //[SerializeField] private GameObject interactionObject;

    [Header("Animation Door")]
    public Animator DoorAnimator;

    public bool thisDoorIsOpen = false;

    public void StartDoorAnimation()
    {
        thisDoorIsOpen = true;
        Debug.Log("Lancement de l'animation");
        DoorAnimator.SetBool("OpenDoor", true);
    }
}
