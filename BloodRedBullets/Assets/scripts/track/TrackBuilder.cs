using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class to handle placing TrackPieces during gameplay
/// </summary>
public class TrackBuilder : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> Pieces;
    [SerializeField]
    public int maxTrackNum = 5;
    private Queue<GameObject> currentPieces = new Queue<GameObject>();
    private System.Random random;
    private GameObject previousPiece = null; //used to track current placement of last trackPiece

    /// <summary>
    /// Creates the initial track, a Queue of blank TrackPiece GameObjects the size of maxTrackNum
    /// </summary>
    public void InitializeTrack(){
        random = new System.Random();
        for(int i = 0; i < maxTrackNum; i++){ //fill the Queue
            AddTrack(true);
        }
    }

    /// <summary>
    /// Adds a new piece to the track from Pieces, removing the oldest TrackPiece if init is false
    /// </summary>
    /// <param name="init">
    /// If true, adds the "blank" TrackPiece GameObject (from Pieces[0]), otherwise randomly chooses one from Pieces
    /// </param>
    public void AddTrack(bool init){
        GameObject pieceToAdd = Pieces[0];
        if(!init){ //if we're not initializing the Queue
            pieceToAdd = Pieces[random.Next(Pieces.Count)]; //choose a random piece
            GameObject pieceToRemove = currentPieces.Dequeue(); //remove last piece from queue
            Destroy(pieceToRemove); //destroy last piece
        }
        //find where to create new TrackPiece
        Vector3 placement = Vector3.zero;
        if(previousPiece != null){
            placement = previousPiece.transform.position + previousPiece.GetComponent<TrackPiece>().EndPoint.localPosition; 
        }
        //create new TrackPiece, parent it to this GameObject
        GameObject newPiece = Instantiate(pieceToAdd, placement, Quaternion.identity, transform);
        previousPiece = newPiece; //set previous TrackPiece to the newly created one
        currentPieces.Enqueue(newPiece); //add new piece to Queue
    }

    /// <summary>
    /// Gets a list of all current TrackPieces.
    /// </summary>
    /// <returns>
    /// A list containing the GameObjects of all current TrackPieces
    /// </returns>
    public List<GameObject> GetCurrentPiecesAsList(){
        return currentPieces.ToList();
    }
}
