using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerLocation : MonoBehaviour
{
    [SerializeField]
    private float groundedHeight = 1.5f;
    [SerializeField]
    private float jumpHeight = 5;
    [SerializeField]
    private float slideHeight = .5f;
    [SerializeField]
    private float laneOffset = 2.6f;
    
    public void ChangeLocation(PlayerState currentStates){
        Vector3 newLoc = Vector3.zero;
        float x = 0;
        float y = 0;
        switch(currentStates.currentGroundState){
            case PlayerState.GroundState.Grounded:
                y = groundedHeight;
                break;
            case PlayerState.GroundState.Jumping:
                y = jumpHeight;
                break;
            case PlayerState.GroundState.Sliding:
                y = slideHeight;
                break;
        }
        switch(currentStates.currentLaneState){
            case PlayerState.LaneState.Middle:
                x = 0;
                break;
            case PlayerState.LaneState.Left:
                x = -laneOffset;
                break;
            case PlayerState.LaneState.Right:
                x = laneOffset;
                break;
        }
        transform.position = new Vector3(x, y, 0);
    }
}
