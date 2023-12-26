using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the movement of the track and calls TrackBuilder to create new TrackPieces when necessary
/// </summary>
public class TrackMover : MonoBehaviour
{
    public TrackBuilder trackBuilder;
    public float moveSpeed;
    void Start(){
        trackBuilder.InitializeTrack();
    }

    /// <summary>
    /// Moves each TrackPiece, calling TrackBuilder.AddTrack when oldest TrackPiece is out of view.
    /// </summary>
    void Update(){
        float moveAmount = moveSpeed*Time.deltaTime;
        foreach (GameObject piece in trackBuilder.GetCurrentPiecesAsList()){
            Vector3 pos = piece.transform.position;
            piece.transform.position = new Vector3(pos.x, pos.y, pos.z -= moveAmount);
            if (piece.GetComponent<TrackPiece>().EndPoint.position.z < -5){
                trackBuilder.AddTrack(false);
            }
        }
    }
}
