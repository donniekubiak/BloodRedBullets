using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerState state;

    void OnTriggerEnter(Collider other)
    {
        string triggerType = other.gameObject.tag;
        switch (triggerType)
        {
            case "Low":
                if(state.currentGroundState != PlayerState.GroundState.Jumping)
                {
                    state.TakeDamage();
                }
                break;
            case "High":
                if (state.currentGroundState != PlayerState.GroundState.Sliding)
                {
                    state.TakeDamage();
                }
                break;
        }
    }
}
