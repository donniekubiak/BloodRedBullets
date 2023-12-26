using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement;

    void Update()
    {
        if(Input.GetButtonDown("Left")){
            playerMovement.ChangeLane(-1);
        }
        if(Input.GetButtonDown("Right")){
            playerMovement.ChangeLane(1);
        }
        if(Input.GetButtonDown("Up")){
            playerMovement.Jump();
        }
        if(Input.GetButtonDown("Down")){
            playerMovement.Slide();
        }
    }
}
