using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player movement controller.
/// </summary>
public class PlayerMovement : MonoBehaviour
{    
    [SerializeField]
    private PlayerState playerState;

    [SerializeField]
    private PlayerLocation playerLoc;

    [SerializeField]
    private CameraController cam;

    [SerializeField]
    private float jumpTime = .45f;
    [SerializeField]
    private float slideTime = .5f;

    /// <summary>
    /// Changes the lane occupied by the player according to given direction.
    /// </summary>
    /// <param name="direction"> <0 means leftward movement, >=0 means rightward </param>
    public void ChangeLane(int direction){
        bool changed = true;
        switch(playerState.currentLaneState){
            case PlayerState.LaneState.Left:
                if (direction >= 0){
                    playerState.ChangeLaneState(PlayerState.LaneState.Middle);
                }
                else
                {
                    changed = false;
                }
                break;
            case PlayerState.LaneState.Middle:
                if (direction >= 0){
                    playerState.ChangeLaneState(PlayerState.LaneState.Right);
                }
                else{
                    playerState.ChangeLaneState(PlayerState.LaneState.Left);
                }
                break;
            case PlayerState.LaneState.Right:
                if (direction < 0){
                    playerState.ChangeLaneState(PlayerState.LaneState.Middle);
                }
                else
                {
                    changed = false;
                }
                break;
        }
        if (changed)
        {
            playerLoc.ChangeLocation(playerState);
            cam.Move(direction);
        }
    }
    public void Jump(){
        if(playerState.currentGroundState != PlayerState.GroundState.Jumping){
            playerState.ChangeGroundState(PlayerState.GroundState.Jumping);
            StopAllCoroutines();    
            IEnumerator endJumpRoutine = WaitEndJump();
            StartCoroutine(endJumpRoutine);
            playerLoc.ChangeLocation(playerState);
            cam.Jump(jumpTime);
        }
    }
    public void EndJump(){
        if(playerState.currentGroundState == PlayerState.GroundState.Jumping)
            playerState.ChangeGroundState(PlayerState.GroundState.Grounded);
        playerLoc.ChangeLocation(playerState);
        cam.EndJump();
    }
    public IEnumerator WaitEndJump(){
        yield return new WaitForSeconds(jumpTime);
        EndJump();
    }

    public void Slide(){
        cam.Slide();
        if(playerState.currentGroundState != PlayerState.GroundState.Sliding){
            playerState.ChangeGroundState(PlayerState.GroundState.Sliding);
            StopAllCoroutines();
            IEnumerator endSlideRoutine = WaitEndSlide();
            StartCoroutine(endSlideRoutine);
        }
        playerLoc.ChangeLocation(playerState);
    }
    public void EndSlide(){
        if(playerState.currentGroundState == PlayerState.GroundState.Sliding)
            playerState.ChangeGroundState(PlayerState.GroundState.Grounded);
        playerLoc.ChangeLocation(playerState);
        cam.EndSlide();
    }
    public IEnumerator WaitEndSlide(){
        yield return new WaitForSeconds(slideTime);
        EndSlide();
    }
}
