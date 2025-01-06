using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed;     
    [SerializeField] private float rotationSpeed;
    public Player.Etat PlayerEtat;

    [Header("Animator Player X")]
    public Animator AnimatorPlayerX;

    [Header("Animator Player Y")]
    public Animator AnimatorPlayerY;

    private Vector3 inputDirection;



    void Update()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        inputDirection = new Vector3(horizontal, 0, vertical).normalized;
        Movement();
       
    }
    private void Movement()
    {

        if (inputDirection.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            if (PlayerEtat == Player.Etat.PlayerX)
            {
                AnimatorPlayerX.SetBool("Walk", true);
            }
            else if (PlayerEtat == Player.Etat.PlayerY)
            {
                AnimatorPlayerY.SetBool("Walk", true);
            }


        }


        if(inputDirection.magnitude <= 0f)
        {
            if (PlayerEtat == Player.Etat.PlayerX)
            {
                AnimatorPlayerX.SetBool("Walk", false);
            }
            else if (PlayerEtat == Player.Etat.PlayerY)
            {
                AnimatorPlayerY.SetBool("Walk", false);
            }
        }
        
    }
}

[System.Serializable]
public class Player
{
    public enum Etat { PlayerX, PlayerY, InPossesion, PlayerYReturn, PlayerArrived }
}
