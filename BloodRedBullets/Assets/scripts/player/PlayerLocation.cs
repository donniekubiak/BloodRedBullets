using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerLocation : MonoBehaviour
{
    [SerializeField]
    private float laneOffset = 2.6f;
    
    public void ChangeLocation(PlayerState currentStates){
        Vector3 newLoc = Vector3.zero;
        float x = 0;
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
        transform.position = new Vector3(x, 0, 0);
    }
}
