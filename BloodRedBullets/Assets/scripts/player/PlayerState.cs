using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Contains current Player state Enums and their current state in-game.
/// </summary>
public class PlayerState : MonoBehaviour
{
    /// <summary>
    /// Enum to describe all states of player interaction with ground.
    /// Has Grounded(default), Jumping, and Sliding.
    /// </summary>
    public enum GroundState {Grounded, Jumping, Sliding};
    /// <summary>
    /// Enum to describe all states of player interaction with gun.
    /// Has Readied(default), Reloading, and Firing.
    /// </summary>
    public enum GunState {Readied, Reloading, Firing};
    /// <summary>
    /// Enum to describe current lane player occupies.
    /// Has Left, Middle, Right
    /// </summary>
    public enum LaneState {Left, Middle, Right};
    /// <summary>
    /// Current state of player interaction with ground.
    /// </summary>
    public GroundState currentGroundState {get; private set;}
    /// <summary>
    /// Current state of player interaction with gun.
    /// </summary>
    public GunState currentGunState {get; private set;}
    /// <summary>
    /// Current lane player occupies.
    /// </summary>
    public LaneState currentLaneState {get; private set;}

    void Start(){
        ResetAllStates();
    }
    /// <summary>
    /// Resets all player states to default.
    /// </summary>
    public void ResetAllStates(){
        currentGroundState = GroundState.Grounded;
        currentGunState = GunState.Readied;
        currentLaneState = LaneState.Middle;
    }
    /// <summary>
    /// Changes the current GroundState held by this object
    /// </summary>
    /// <param name="state"></param>
    public void ChangeGroundState(GroundState state) {
        currentGroundState = state;
    }
    /// <summary>
    /// Changes the current GunState held by this object
    /// </summary>
    /// <param name="state"></param>
    public void ChangeGunState(GunState state) {
        currentGunState = state;
    }
    /// <summary>
    /// Changes the current LaneState held by this object
    /// </summary>
    /// <param name="state"></param>
    public void ChangeLaneState(LaneState state) {
        currentLaneState = state;
    }
}
